using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Views.Controls;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews.Menu {
    public partial class MenuItemView : StackListItemBase {

        /// <summary>
        /// 
        /// </summary>
        public MenuItemView() {
            InitializeComponent();
            
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Selected() {
            //_outputText_Label.FontAttributes = FontAttributes.Bold;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Deselected() {
            //_outputText_Label.FontAttributes = FontAttributes.None;
        }
    }
}