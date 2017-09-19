using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.ToolKit.ValueConverters {
    public class BoolToStringConverter : IValueConverter {
        /// <summary>
        /// 
        /// </summary>
        public BoolToStringConverter() {
            TrueText = "True text";
            FalseText = "False text";
        }

        /// <summary>
        /// 
        /// </summary>
        public string TrueText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FalseText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((bool)value) ? TrueText : FalseText;
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
            return string.Equals(TrueText, value.ToString()) ? true : false;
        }
    }
}
