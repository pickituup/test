using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers.Builders.ViewBuilders;
using Xamarin.Forms;

namespace tpm.Views.CompoundedViews {
    public abstract class ActionBarBase : ContentView, IView {

        public static readonly BindableProperty HeaderTitleProperty;

        /// <summary>
        /// Static ctor.
        /// </summary>
        static ActionBarBase() {
            HeaderTitleProperty = 
                BindableProperty.Create("HeaderTitle", 
                typeof(string), 
                typeof(ActionBarBase));
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract string HeaderTitle {
            get;
            set;
        }
    }
}
