using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.ToolKit.ValueConverters {
    public sealed class BoolToColorConverter : IValueConverter {
        private Color _trueColor;
        private Color _falseColor;

        /// <summary>
        /// 
        /// </summary>
        public BoolToColorConverter() {
            _trueColor = Color.Aqua;
            _falseColor = Color.DarkBlue;
        }

        /// <summary>
        /// 
        /// </summary>
        public Color TrueColor {
            get => _trueColor;
            set => _trueColor = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public Color FalseColor {
            get => _falseColor;
            set => _falseColor = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((bool)value) ? TrueColor : FalseColor;
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
            return ((Color)value) == TrueColor ? true : false;
        }
    }
}
