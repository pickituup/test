using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace tpm.Views.CompoundedViews.Menu {
    public abstract class MenuBase : ContentView {

        private bool _isMenuVisible;

        /// <summary>
        /// 
        /// </summary>
        public bool IsMenuVisible {
            get => _isMenuVisible;
            protected set => _isMenuVisible = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void ShowMenu();

        /// <summary>
        /// 
        /// </summary>
        public abstract void HideMenu();
    }
}
