using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class QuestionsListView : ContentView, IAuditView {

        private QuestionsListViewViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public QuestionsListView() {
            InitializeComponent();

            BindingContext = _viewModel = new QuestionsListViewViewModel();
        }
    }
}
