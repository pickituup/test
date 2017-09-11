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
    public sealed class MenuViewModel : ViewModelBase, IViewModel {

        private readonly MenuContainer _menuContainer;
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenuItem;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public MenuViewModel() {
            _menuContainer = new MenuContainer();

            MenuItems = _menuContainer.BuildMenuItems();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<MenuItem> MenuItems {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public MenuItem SelectedMenuItem {
            get { return _selectedMenuItem; }
            set {
                if (SetProperty(ref _selectedMenuItem, value) && value != null) {
                    //BaseSingleton<ViewSwitchingLogic>.Instance.NavigateTo(value.PageType);
                    PageTypes pT = value.PageType;
                }
            }
        }

    }
}