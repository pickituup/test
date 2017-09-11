using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class ReviewView : ContentView , IAuditView {

        private ReviewViewViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public ReviewView() {
            InitializeComponent();

            BindingContext = _viewModel = new ReviewViewViewModel();
        }
    }
}
