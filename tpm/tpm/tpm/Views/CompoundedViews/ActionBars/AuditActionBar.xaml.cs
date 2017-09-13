using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class AuditActionBar : ActionBarBase {

        public static readonly BindableProperty IsOneStepBackAvailableProperty =
            BindableProperty.Create("IsOneStepBackAvailable",
                typeof(bool),
                typeof(AuditActionBar),
                defaultValue: false,
                propertyChanged: OnIsOneStepBackAvailableChanged);

        /// <summary>
        /// Public ctor.
        /// </summary>
        public AuditActionBar() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOneStepBackAvailable {
            get => (bool)GetValue(IsOneStepBackAvailableProperty);
            set => SetValue(IsOneStepBackAvailableProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public override string HeaderTitle {
            get {
                return (string)GetValue(ActionBarBase.HeaderTitleProperty);
            }
            set {
                SetValue(ActionBarBase.HeaderTitleProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SwapMenuOneStapBackButtons() {
            if (IsOneStepBackAvailable) {
                //
                // Hide menu button
                //
                Grid.SetColumn(_menuButton_Image, 3);

                //
                // Display one step back button
                //
                Grid.SetColumn(_backButton_Image, 0);
            }
            else {
                //
                // Display menu button
                //
                Grid.SetColumn(_menuButton_Image, 0);

                //
                // Hide one step back button
                //
                Grid.SetColumn(_backButton_Image, 3);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void OnIsOneStepBackAvailableChanged(BindableObject bindable, object oldValue, object newValue) {
            AuditActionBar auditActionBar = (AuditActionBar)bindable;

            if (auditActionBar != null) {
                auditActionBar.SwapMenuOneStapBackButtons();
            }
        }
    }
}