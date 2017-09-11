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
    public sealed class MainStartupPageViewModel : ViewModelBase, IViewModel {

        private readonly MenuContainer _menuContainer;
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenuItem;
        private bool _isBusy;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public MainStartupPageViewModel() {
            _menuContainer = new MenuContainer();
            MenuItems = _menuContainer.BuildMenuItems();
        }

        /// <summary>
        /// List of menu items (potential pages for navigation).
        /// </summary>
        public List<MenuItem> MenuItems {
            get { return _menuItems; }
            private set { SetProperty(ref _menuItems, value); }
        }

        /// <summary>
        /// TODO: define in moe abstract maner...
        /// </summary>
        public bool IsBusy {
            get => _isBusy;
            private set => SetProperty<bool>(ref _isBusy, value);
        }

        /// <summary>
        /// Selected menu item. Also navigates to appropriate page.
        /// </summary>
        public MenuItem SelectedMenuItem {
            get { return _selectedMenuItem; }
            set {
                if (SetProperty(ref _selectedMenuItem, value) && value != null) {
                    //BaseSingleton<ViewSwitchingLogic>.Instance.NavigateTo(value.PageType);
                    NavigateToThePage(value.PageType);

                    SelectedMenuItem = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetPage"></param>
        private async void NavigateToThePage(PageTypes targetPage) {
            IsBusy = true;

            await BaseSingleton<PageSwitchingLogic>.Instance.NavigateToAsync(targetPage);

            IsBusy = false;
        }
    }
}