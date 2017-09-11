using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.ViewModels;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews.Menu {
    public partial class MenuView : MenuBase {

        /// <summary>
        /// 
        /// </summary>
        public MenuView() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAnimated"></param>
        /// <returns></returns>
        public async override void ShowMenu() {
            this.IsMenuVisible = true;
            this.IsVisible = true;
            await this.FadeTo(1, 130);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAnimated"></param>
        public async override void HideMenu() {
            this.IsMenuVisible = false;
            await this.FadeTo(0, 130);
            this.IsVisible = false;
        }
    }
}