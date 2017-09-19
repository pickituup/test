using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using tpm.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.iOS.Services.PackageInfoService))]
namespace tpm.iOS.Services {
    public class PackageInfoService : IPackageInfo {

        private string _versionNumber;
        private string _versionName;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PackageInfoService() {
            VersionNumber = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
            VersionName = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
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
