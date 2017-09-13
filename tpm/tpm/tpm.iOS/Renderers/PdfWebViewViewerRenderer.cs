using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using tpm.iOS.Renderers;
using tpm.Views.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PdfWebViewViewer), typeof(PdfWebViewViewerRenderer))]
namespace tpm.iOS.Renderers {
    public sealed class PdfWebViewViewerRenderer: ViewRenderer<PdfWebViewViewer, UIWebView> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<PdfWebViewViewer> e) {
            base.OnElementChanged(e);

            if (Control == null) {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null) {
                // Cleanup
            }
            if (e.NewElement != null) {
                var customWebView = Element as PdfWebViewViewer;
                string fileName = WebUtility.UrlEncode(customWebView.Uri);
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}