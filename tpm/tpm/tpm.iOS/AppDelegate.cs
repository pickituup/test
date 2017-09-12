
using Foundation;
using ObjCRuntime;
using UIKit;

namespace tpm.iOS {
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        public static UIInterfaceOrientationMask OrientationLock = UIInterfaceOrientationMask.Portrait;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="forWindow"></param>
        /// <returns></returns>
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow) {
            return OrientationLock;
        }
    }
}