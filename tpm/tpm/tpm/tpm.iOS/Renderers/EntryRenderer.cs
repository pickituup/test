using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(tpm.iOS.Renderers.EntryRenderer))]
namespace tpm.iOS.Renderers {
    public sealed class EntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                DisableNativeBorder();
            }
        }

        /// <summary>
        /// Simply disables border of native entry
        /// </summary>
        private void DisableNativeBorder() {
            Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}