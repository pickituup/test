using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tpm.Helpers;
using tpm.NavigationFramework;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace tpm {
    public partial class App : Application {

        /// <summary>
        /// 
        /// </summary>
        public App() {
            InitializeComponent();

            BaseSingleton<PageSwitchingLogic>.Instance.BuildNavigationStack(PageTypes.MainStartupPage);
        }
    }
}