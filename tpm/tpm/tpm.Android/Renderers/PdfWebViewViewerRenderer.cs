using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using tpm.Views.Controls;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics.Pdf;

[assembly: ExportRenderer(typeof(PdfWebViewViewer), typeof(tpm.Droid.Renderers.PdfWebViewViewerRenderer))]
namespace tpm.Droid.Renderers {
    public sealed class PdfWebViewViewerRenderer : WebViewRenderer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                PdfWebViewViewer customWebView = Element as PdfWebViewViewer;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.Settings.AllowFileAccessFromFileURLs = true;
                Control.Settings.JavaScriptEnabled = true;
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = false;

                //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", System.Net.WebUtility.UrlEncode(customWebView.Uri)));
                //Control.LoadUrl(string.Format("http://docs.google.com/gview?embedded=true&url={0}", System.Net.WebUtility.UrlEncode(customWebView.Uri)));
                Control.LoadUrl(string.Format("file:///android_asset/pdfviewer/index.html?file={0}", System.Net.WebUtility.UrlEncode(customWebView.Uri)));
            }
            
        }
    }
}