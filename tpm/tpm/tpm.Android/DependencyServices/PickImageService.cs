using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tpm.DependencyServices;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.Droid.DependencyServices.PickImageService))]
namespace tpm.Droid.DependencyServices {
    public sealed class PickImageService : IPickImageService {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<string> CatchPhoto() {
            ((MainActivity)Xamarin.Forms.Forms.Context).CatchPhoto();

            return MainActivity.CatchImageTaskCompletion.Task;
        }
    }
}