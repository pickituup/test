using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using tpm.Views.CompoundedViews;

namespace tpm.Views.Pages {
    public abstract class ActionBarPageBase : ContentPage {

        public static readonly BindableProperty ActionBarProperty;

        /// <summary>
        /// Static ctor.
        /// </summary>
        static ActionBarPageBase() {
            ActionBarProperty =
                BindableProperty.Create("ActionBar",
                typeof(ActionBarBase),
                typeof(ActionBarPageBase),
                propertyChanged: OnPropertyChanged);
        }

        /// <summary>
        /// Public ctor.
        /// </summary>
        public ActionBarPageBase() {
            NavigationPage.SetHasNavigationBar(this, false);
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
        protected abstract void DisplayActionBar();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue) {
            ActionBarPageBase actionBarPageBase = (ActionBarPageBase)bindable;

            if (actionBarPageBase != null) {
                actionBarPageBase.DisplayActionBar();
            }
        }
    }
}