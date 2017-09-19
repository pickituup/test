using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using tpm.DependencyServices;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.Droid.DependencyServices.ScreenService))]
namespace tpm.Droid.DependencyServices {

    /// <summary>
    /// 
    /// </summary>
    public class ScreenService : IScreenService {

        /// <summary>
        /// 
        /// </summary>
        public void LandscapeFullScreen() {
            ((MainActivity)Xamarin.Forms.Forms.Context).RequestedOrientation =
                Android.Content.PM.ScreenOrientation.Landscape;

            ((MainActivity)Xamarin.Forms.Forms.Context).Window.AddFlags(WindowManagerFlags.Fullscreen);
        }

        /// <summary>
        /// 
        /// </summary>
        public void DefaultOrientation() {
            ((MainActivity)Xamarin.Forms.Forms.Context).RequestedOrientation =
                Android.Content.PM.ScreenOrientation.Portrait;

            ((MainActivity)Xamarin.Forms.Forms.Context).Window.ClearFlags(WindowManagerFlags.Fullscreen);
        }
    }
}