using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Models.DataContainers;
using tpm.NavigationFramework;
using tpm.Views.CompoundedViews.Menu;
using XF = Xamarin.Forms;
using tpm.Models.DataContainers.DataItems;
using tpm.Helpers;

namespace tpm.ViewModels {
    public abstract class BasePageViewModel : ViewModelBase {

        /// <summary>
        /// On iOS it's not enough to set opacity 0 for "menu and loading indicator", they still will block 
        /// interaction with underlying elements. So "menu and loading indicator" will use TranslationX with _xTranslation value.
        /// </summary>
        private static readonly int _xTranslation = -3000;

        private readonly MenuContainer _menuContainer;
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenuItem;
        private MenuBase _menu;
        private bool _isAwaiting;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public BasePageViewModel() {
            _menuContainer = new MenuContainer();
            MenuItems = _menuContainer.BuildMenuItems();
            Menu = new MenuView() {
                IsVisible = false,
                Opacity = 0,
                TranslationX = _xTranslation
            };

            ShowMenuCommand = new XF.Command(() => {
                //
                // On iOS it's not enough to set opacity 0 for "menu and loading indicator", they still will block 
                // interaction with underlying elements.
                //
                Menu.TranslationX = 0;
                Menu.ShowMenu();
            });

            HideMenuCommand = new XF.Command(() => {
                Menu.HideMenu();
                //
                // On iOS it's not enough to set opacity 0 for "menu and loading indicator", they still will block 
                // interaction with underlying elements.
                //
                Menu.TranslationX = _xTranslation;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAwaiting {
            get { return _isAwaiting; }
            protected set { SetProperty(ref _isAwaiting, value); }
        }

        /// <summary>
        /// List of menu items (potential pages for navigation).
        /// </summary>
        public List<MenuItem> MenuItems {
            get { return _menuItems; }
            private set { SetProperty(ref _menuItems, value); }
        }

        /// <summary>
        /// Selected menu item. Also navigates to appropriate page.
        /// </summary>
        public MenuItem SelectedMenuItem {
            get { return _selectedMenuItem; }
            set {
                if (SetProperty(ref _selectedMenuItem, value) && value != null) {
                    BaseSingleton<PageSwitchingLogic>.Instance.NavigateTo(value.PageType);

                    SelectedMenuItem = null;
                }
            }
        }

        /// <summary>
        /// Menu 'visual' instance. Allow's to 'animate' through VM.
        /// </summary>
        public MenuBase Menu {
            get => _menu;
            private set => SetProperty<MenuBase>(ref _menu, value);
        }

        /// <summary>
        /// Make one step back navigation through ViewSwitchingLogic.
        /// </summary>
        public void NavigateOneStepBack() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }

        /// <summary>
        /// Header for action bar. Each 'page view model' will define its own header and set/get behavior.
        /// </summary>
        public abstract string ActionBarHeader { get; protected set; }

        /// <summary>
        /// Show menu
        /// </summary>
        public ICommand ShowMenuCommand { get; private set; }

        /// <summary>
        /// Hide menu
        /// </summary>
        public ICommand HideMenuCommand { get; private set; }
    }
}