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
using Xamarin.Forms.Platform.Android;
using tpm.Droid.Renderers;
using Xamarin.Forms;
using tpm.Views.Controls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ExtendedWebView), typeof(ExtendedWebViewRenderer))]
namespace tpm.Droid.Renderers {
    public class ExtendedWebViewRenderer : WebViewRenderer {

        private WebViewSource _lastWebViewSource;

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetachedFromWindow() {
            base.OnDetachedFromWindow();

            if (Control != null) {
                Control.StopLoading();
                Control.LoadUrl("");
                Control.Reload();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "IsNotVisible") {
                if (((ExtendedWebView)Element).IsNotVisible) {
                    _lastWebViewSource = Element.Source;
                    Control.StopLoading();
                    Control.LoadUrl("");
                    Control.Reload();
                }
                else {
                    Element.Source = _lastWebViewSource;
                }
            }
        }
    }
}