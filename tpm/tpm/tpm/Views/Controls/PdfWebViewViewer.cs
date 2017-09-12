using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.Views.Controls {
    public sealed class PdfWebViewViewer : WebView {

        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(PdfWebViewViewer),
                defaultValue: default(string));

        /// <summary>
        /// 
        /// </summary>
        public string Uri {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
    }
}
