using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class AssesmentView : ContentView, IAuditView {

        private AssesmentViewViewModel _viewModel;

        /// <summary>
        /// Publicctor.
        /// </summary>
        public AssesmentView() {
            InitializeComponent();

            BindingContext = _viewModel = new AssesmentViewViewModel();
        }
    }
}
