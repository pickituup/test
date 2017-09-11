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
using Xamarin.Forms;
using NativeGraphics = Android.Graphics;
using Android.Graphics.Drawables;

namespace tpm.Droid.Renderers {
    public abstract class EditorRendererBase : EditorRenderer {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e) {
            base.OnElementChanged(e);

            if (Control != null && Element != null) {
                RemoveUnderscore();
            }
        }

        /// <summary>
        /// Parse Xamarin Color to native Android Color.
        /// 
        /// TODO: DRY, set this method in service, static type.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static NativeGraphics.Color CreateNativeColor(Color color) {
            byte red = (byte)(color.R * 255);
            byte green = (byte)(color.G * 255);
            byte blue = (byte)(color.B * 255);
            byte alpha = (byte)(color.A * 255);

            return new NativeGraphics.Color(red, green, blue, alpha);
        }

        /// <summary>
        /// Will hide the undefined underline through all native-element. Under the hood change the background of native
        /// control to the appropriate Forms Element bakground color.
        /// </summary>
        private void RemoveUnderscore() {
            if (Control != null && Element != null) {
                Color backgroundColor = Element.BackgroundColor;

                Control.Background = new ColorDrawable(CreateNativeColor(backgroundColor));
            }
        }
    }
}