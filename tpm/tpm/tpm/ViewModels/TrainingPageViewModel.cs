using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.Models.DataContainers;
using tpm.Models.DataContainers.DataItems;
using tpm.NavigationFramework;

namespace tpm.ViewModels {
    public class TrainingPageViewModel : BasePageViewModel, IViewModel {

        private TrainingToolsContainer _trainingToolsContainer = new TrainingToolsContainer();
        private List<TrainingToolsMaterialsBlockViewViewModel> _trainingToolsMaterials = new List<TrainingToolsMaterialsBlockViewViewModel>();
        private string _actionBarHeader;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public TrainingPageViewModel() {
            TrainingToolsMaterials.AddRange(_trainingToolsContainer.BuildWebinarItems()
                .Select<TrainingToolsItem, TrainingToolsMaterialsBlockViewViewModel>(t => new TrainingToolsMaterialsBlockViewViewModel(t)));

            MenuItems.First(i => i.PageType == PageTypes.TrainingPage).IsSelected = true;
            ActionBarHeader = _actionBarHeader = "Training Tools & Materials";
        }

        /// <summary>
        /// 
        /// </summary>
        public List<TrainingToolsMaterialsBlockViewViewModel> TrainingToolsMaterials {
            get => _trainingToolsMaterials;
            private set => SetProperty<List<TrainingToolsMaterialsBlockViewViewModel>>(ref _trainingToolsMaterials, value);
        }

        /// <summary>
        /// BasePageViewModel implementation
        /// </summary>
        public override string ActionBarHeader {
            get => _actionBarHeader;
            protected set => SetProperty<string>(ref _actionBarHeader, value);
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="isNoVisible"></param>
        public void WEB_VIEW_BUG(bool isNoVisible) {
            foreach (TrainingToolsMaterialsBlockViewViewModel item in TrainingToolsMaterials) {
                item.WebViewIsNotVisible = isNoVisible;
            }
        }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }
    }
}