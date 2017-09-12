using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Views.CompoundedViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tpm.Views.CompoundedViews.Menu;

namespace tpm.Views.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial class BasePage : ContentPage {

        public static readonly BindableProperty ActionBarProperty;
        public static readonly BindableProperty MainContentProperty;
        public static readonly BindableProperty MenuProperty;
        public static readonly BindableProperty IsAwaitingProperty;

        /// <summary>
        /// Static ctor.
        /// </summary>
        static BasePage() {
            ActionBarProperty =
                BindableProperty.Create("ActionBar",
                typeof(ActionBarBase),
                typeof(BasePage),
                propertyChanged: OnActionBarPropertyChanged);

            MainContentProperty =
                BindableProperty.Create("MainContent",
                typeof(View),
                typeof(BasePage),
                propertyChanged: OnMainContentPropertyChanged);

            MenuProperty =
                BindableProperty.Create("Menu",
                typeof(MenuBase),
                typeof(BasePage),
                propertyChanged: OnMenuPropertyChanged);

            IsAwaitingProperty =
                BindableProperty.Create("IsAwaiting",
                typeof(bool),
                typeof(BasePage),
                defaultValue: false,
                propertyChanged: OnIsAwaitingChanged);
        }

        /// <summary>
        /// Public ctor.
        /// </summary>
        public BasePage() {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            ToogleAwaiting();
        }

        /// <summary>
        /// 
        /// </summary>
        public ActionBarBase ActionBar {
            get => (ActionBarBase)GetValue(ActionBarProperty);
            set => SetValue(ActionBarProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public View MainContent {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public MenuBase Menu {
            get => (MenuBase)GetValue(MenuProperty);
            set => SetValue(MenuProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAwaiting {
            get => (bool)GetValue(IsAwaitingProperty);
            set => SetValue(IsAwaitingProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AttachActionBar() {
            _actionBarSpot_ContentView.Content = ActionBar;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AttachMainContent() {
            _mainContentSpot_ContentView.Content = MainContent;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AttachMenuContent() {
            _menuSpot_ContentView.Content = Menu;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ToogleAwaiting() {
            _indicator_ExtendedSimpleIndicator.IsVisible = IsAwaiting;

            if (IsAwaiting) {
                Grid.SetRow(_indicator_ExtendedSimpleIndicator, 0);
            }
            else {
                Grid.SetRow(_indicator_ExtendedSimpleIndicator, 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnActionBarPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            BasePage basePage = (BasePage)bindable;

            if (basePage != null) {
                basePage.AttachActionBar();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnMainContentPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            BasePage basePage = (BasePage)bindable;

            if (basePage != null) {
                basePage.AttachMainContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnMenuPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            BasePage basePage = (BasePage)bindable;

            if (basePage != null) {
                basePage.AttachMenuContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnIsAwaitingChanged(BindableObject bindable, object oldValue, object newValue) {
            BasePage basePage = (BasePage)bindable;

            if (basePage != null) {
                basePage.ToogleAwaiting();
            }
        }
    }
}