using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tpm.Views.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demo : ContentPage {
        public Demo() {
            InitializeComponent();

            BindingContext = new DemoVM();
        }
    }

    public class DemoVM : tpm.ViewModels.ViewModelBase {
        public DemoVM() {
            Ic = new Command(async () => {
                string base64photo = await DependencyService.Get<IPickImageService>().CatchPhoto();

                I = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64photo)));

                //using (Stream stream = new MemoryStream(Convert.FromBase64String(base64photo))) {
                //    //I = ImageSource.FromStream(() => stream);
                //    ImageSource iss = ImageSource.FromStream(() => stream);
                //}
            });
        }
        private ImageSource _i;
        public ImageSource I {
            get => _i;
            set => SetProperty<ImageSource>(ref _i, value);
        }

        public ICommand Ic { get; set; }
    }
}
