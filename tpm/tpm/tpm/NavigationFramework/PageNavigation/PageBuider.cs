using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using tpm.Helpers.Builders.ViewBuilders;

namespace tpm.NavigationFramework {
    /// <typeparam name="TView"></typeparam>
    public sealed class PageBuider<TView> : ViewBuilderBase<TView> 
        where TView : INavigatedPage, new() {

        /// <summary>
        /// Define new instance of Xamarin.Forms.NavigationPage with target page inside.
        /// </summary>
        /// <returns></returns>
        public NavigationPage GetPageInNavigationFrame() {
            return new NavigationPage(View as Page);
        }

        /// <summary>
        /// Return target page instance.
        /// </summary>
        /// <returns></returns>
        public INavigatedPage GetPage() {
            return View;
        }
    }
}