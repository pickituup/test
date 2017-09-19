using System;
using System.Collections.Generic;
using System.Text;
using tpm.iOS.Renderers;
using tpm.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace tpm.iOS.Renderers {
    public sealed class ExtendedWebViewRenderer : WebViewRenderer { }
}
