using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.Views.Controls {
    public sealed class ExtendedLabel : Label {
        public static readonly BindableProperty LineHeightProperty =
            BindableProperty.Create("LineHeight", typeof(float), typeof(ExtendedLabel), defaultValue: 1F);

        /// <summary>
        /// 
        /// </summary>
        public float LineHeight {
            get => (float)GetValue(LineHeightProperty);
            set => SetValue(LineHeightProperty, value);
        }
    }
}
