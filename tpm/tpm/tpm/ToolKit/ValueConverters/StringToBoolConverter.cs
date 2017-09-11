using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.ToolKit.ValueConverters {
    public sealed class StringToBoolConverter : IValueConverter {

        private static readonly string _ERROR_TO_SOURCE_CONVERTING =
            "StringToBoolConverter works only with OneWay binding";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(string.IsNullOrEmpty(value as string));
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
            throw new InvalidOperationException(string.Format("StringToBoolConverter.ConvertBack - {0}",
                _ERROR_TO_SOURCE_CONVERTING));
        }
    }
}
