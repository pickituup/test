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
using Xamarin.Forms.Platform.Android;
using tpm.Droid.Renderers;
using System.ComponentModel;
using tpm.Views.Controls;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace tpm.Droid.Renderers {
    public sealed class ExtendedLabelRenderer : LabelRenderer {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e) {
            base.OnElementChanged(e);

            UseLineHeightMultiplier();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "LineHeight") {
                UseLineHeightMultiplier();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UseLineHeightMultiplier() {
            Control.SetLineSpacing(0, ((ExtendedLabel)Element).LineHeight);
        }
    }
}