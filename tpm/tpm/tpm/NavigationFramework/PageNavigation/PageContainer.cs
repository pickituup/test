using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers.Builders.ViewBuilders;
using Xamarin.Forms;
using tpm.Views.Pages;

namespace tpm.NavigationFramework {
    public sealed class PageContainer {

        private static readonly string _ERROR_CANT_GET_PAGE_IN_NAVIGATION_FRAME_BY_TYPE = "Can't get page in navigation frame by it's type",
            _ERROR_CANT_GET_PAGE_BY_TYPE = "Can't get page by it's type";

        /// <summary>
        /// Container for pages wrapped by Xamarin.Forms.NavigationPage
        /// </summary>
        private readonly Dictionary<PageTypes, Func<NavigationPage>> _pagesInNavigationFrame;

        /// <summary>
        /// Container for views
        /// </summary>
        private readonly Dictionary<PageTypes, Func<INavigatedPage>> _pages;

        /// <summary>
        /// Public ctor
        /// </summary>
        public PageContainer() {
            _pages = BuildPages();
            _pagesInNavigationFrame = BuildViewsInNavigationFrames();
        }

        /// <summary>
        /// Get page in navigation frame by type.
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public NavigationPage GetPageInNavigationFrameByType(PageTypes viewType) {
            try {
                return _pagesInNavigationFrame[viewType]();
            }
            catch (Exception exc) {
                throw new InvalidOperationException(string.Format("ViewContainer.GetViewInNavigationFrameByType - {0}. Exception details - {1}", 
                    _ERROR_CANT_GET_PAGE_IN_NAVIGATION_FRAME_BY_TYPE,
                    exc.Message));
            }
        }

        /// <summary>
        /// Get page by type
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public INavigatedPage GetPageByType(PageTypes viewType) {
            try {
                return _pages[viewType]();
            }
            catch (Exception) {
                throw new InvalidOperationException(string.Format("ViewContainer.GetPageByType - {0}", _ERROR_CANT_GET_PAGE_BY_TYPE));
            }
        }

        /// <summary>
        /// Build views wrapped by Xamarin.Forms.NavigationPage
        /// </summary>
        /// <returns></returns>
        private Dictionary<PageTypes, Func<NavigationPage>> BuildViewsInNavigationFrames() {
            return new Dictionary<PageTypes, Func<NavigationPage>>() {
                {
                    PageTypes.MainStartupPage,
                    () => new PageBuider<MainStartupPage>().GetPageInNavigationFrame()
                }
            };
        }

        /// <summary>
        /// Build views
        /// </summary>
        /// <returns></returns>
        private Dictionary<PageTypes, Func<INavigatedPage>> BuildPages() {
            return new Dictionary<PageTypes, Func<INavigatedPage>>() {
                {
                    PageTypes.AuditPage,
                    () => new PageBuider<AuditPage>().GetPage()
                },
                {
                    PageTypes.WebinarsPage,
                    () => new PageBuider<WebinarsPage>().GetPage()
                },
                {
                    PageTypes.AboutPage,
                    () => new PageBuider<AboutPage>().GetPage()
                },
                {
                    PageTypes.TrainingPage,
                    () => new PageBuider<TrainingPage>().GetPage()
                },
                {
                    PageTypes.WebViewPage,
                    () => new PageBuider<WebViewPage>().GetPage()
                }
            };
        }
    }
}