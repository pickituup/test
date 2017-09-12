using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class EnterInfoView : ContentView, IAuditView {

        private EnterInfoViewViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public EnterInfoView() {
            InitializeComponent();

            BindingContext = _viewModel = new EnterInfoViewViewModel();
        }
    }
}
