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

namespace tpm.Droid.Controlers.Activities {
    [Activity(Label = "WebViewActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class WebViewActivity : Activity {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);
        }
    }
}