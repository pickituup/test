using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class PublishView : ContentView, IAuditView {

        private PublishViewViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PublishView() {
            InitializeComponent();

            BindingContext = _viewModel = new PublishViewViewModel();
        }
    }
}