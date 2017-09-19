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
using tpm.DependencyServices;
using Android.Content.PM;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.Droid.DependencyServices.PackageInfoService))]
namespace tpm.Droid.DependencyServices {
    public class PackageInfoService : IPackageInfo {

        private string _versionNumber;
        private string _versionName;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PackageInfoService() {
            PackageInfo packageInfo = Forms.Context.PackageManager.GetPackageInfo(Forms.Context.PackageName, 0);

            VersionNumber = string.Format("{0}", packageInfo.VersionCode);
            VersionName = packageInfo.VersionName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string VersionNumber {
            get => _versionNumber;
            private set {
                _versionNumber = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string VersionName {
            get => _versionName;
            private set {
                _versionName = value;
            }
        }
    }
}