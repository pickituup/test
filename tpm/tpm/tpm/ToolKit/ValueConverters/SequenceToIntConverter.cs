using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.ToolKit.ValueConverters {
    public sealed class SequenceToIntConverter : IValueConverter {

        private static readonly string _ERROR_VALUE_CONVERTING = "The source value must implement IList. Check validity of incoming source type.";
        private static readonly string _ERROR_CONVERT_BACK = "Convert back is not supported";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                return ((IList)value).Count;
            }
            catch (Exception exc) {
                throw new InvalidOperationException(string.Format("SequenceToIntConverter.Convert - {0}. Exception details - {1}", 
                    _ERROR_VALUE_CONVERTING, 
                    exc.Message));
            }
        }

        /// <summary>
        /// Convert back is not supported
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new InvalidOperationException(string.Format("SequenceToIntConverter.Convert - {0}.",
                    _ERROR_CONVERT_BACK));
        }
    }
}
