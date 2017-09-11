using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ToolKit.ValueConverters {
    public sealed class CommentToFormatedStringConverter : IValueConverter {

        private static readonly string _ERROR_TO_SOURCE_CONVERTING = "CommentMessageAndDateToFormatedStringConverter works only with OneWay binding";
        private static readonly string _ERROR_CONVERTING_CONVERTING = "CommentMessageAndDateToFormatedStringConverter works only AttachedComments";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return null;
            }

            try {
                AttachedComment comment = (AttachedComment)value;

                return new FormattedString() {
                    Spans = {
                        new Span {
                            Text = comment.CommentMessage,
                            ForegroundColor = Color.FromHex("#202125")
                        },
                        new Span {
                            Text = string.Format(" {0:dd MMM H:mm}", comment.PublishDate),
                            ForegroundColor = Color.FromHex("#d7d7e0")
                        }
                    }
                };
            }
            catch (Exception exc) {
                throw new InvalidOperationException(string.Format("CommentToFormatedStringConverter.Convert - {0}. Exception details - {1}",
                    _ERROR_CONVERTING_CONVERTING, exc.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new InvalidOperationException(string.Format("CommentMessageAndDateToFormatedStringConverter.ConvertBack - {0}",
                _ERROR_TO_SOURCE_CONVERTING));
        }
    }
}
