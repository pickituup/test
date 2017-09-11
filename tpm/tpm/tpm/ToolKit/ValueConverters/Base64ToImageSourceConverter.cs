using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace tpm.ToolKit.ValueConverters {
    public sealed class Base64ToImageSourceConverter : IValueConverter {

        private static readonly string _ERROR_TO_SOURCE_CONVERTING = "Base64ToImageSourceConverter works only with OneWay binding";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(value.ToString())));
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
            throw new InvalidOperationException(string.Format("Base64ToImageSourceConverter.ConvertBack - {0}", 
                _ERROR_TO_SOURCE_CONVERTING));
        }
    }
}
