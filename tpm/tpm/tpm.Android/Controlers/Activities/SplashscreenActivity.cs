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
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/SplashScreenTheme")]
    public class SplashscreenActivity : Android.Support.V7.App.AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            DisplayMainActivity();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayMainActivity() {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}