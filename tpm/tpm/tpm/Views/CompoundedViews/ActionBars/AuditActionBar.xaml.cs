using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public partial class AuditActionBar : ActionBarBase {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public AuditActionBar() {
            InitializeComponent();
            
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
    }
}
