using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.Views.Controls {
    public class ExtendedWebView : WebView {
        public static readonly BindableProperty IsNotVisibleProperty =
            BindableProperty.Create("IsNotVisible", typeof(bool), typeof(ExtendedWebView), defaultValue: false);

        /// <summary>
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible
        /// </summary>
        public bool IsNotVisible {
            get => (bool)GetValue(IsNotVisibleProperty);
            set => SetValue(IsNotVisibleProperty, value);
        }
    }
}
