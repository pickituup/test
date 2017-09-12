﻿using System;
using System.Collections.Generic;
using System.Text;
using tpm.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.iOS.Services.ScreenService))]
namespace tpm.iOS.Services {
    public sealed class ScreenService : IScreenService {

        /// <summary>
        /// 
        /// </summary>
        public void DefaultOrientation() {
            UIDevice.CurrentDevice.SetValueForKey(new Foundation.NSNumber((int)UIInterfaceOrientation.Portrait), new Foundation.NSString("orientation"));
        }

        /// <summary>
        /// 
        /// </summary>
        public void LandscapeFullScreen() {
            AppDelegate.OrientationLock = UIInterfaceOrientationMask.Landscape;
            UIDevice.CurrentDevice.SetValueForKey(new Foundation.NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new Foundation.NSString("orientation"));
        }
    }
}