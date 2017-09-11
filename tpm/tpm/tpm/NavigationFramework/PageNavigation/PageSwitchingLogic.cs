using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using tpm.Views.CompoundedViews;
using tpm.Views.Pages;

namespace tpm.NavigationFramework {
    public sealed class PageSwitchingLogic {

        private static readonly string _ERROR_NAVIGATION_STACK_IS_EMPTY = "Make shure that the current navigation stack is not empty",
            _ERROR_INVALID_PAGE_IN_NAVIGATION_STACK = "Page must implement Tulsi.NavigationFramework.IView interface.";
        private readonly PageContainer _pageContainer;

        /// <summary>
        /// Public ctor
        /// </summary>
        public PageSwitchingLogic() {
            _pageContainer = new PageContainer();
        }

        /// <summary>
        /// Creates new navigation stack. Root element will be appropriate to the viewType.
        /// </summary>
        public void BuildNavigationStack(PageTypes pageType) {
            NavigationPage targetNavigationPage = GetPageInNavigationFrameByType(pageType);

            Application.Current.MainPage = targetNavigationPage;
        }

        /// <summary>
        /// Navigate by PageType.
        /// </summary>
        public void NavigateTo(PageTypes pageType) {
            //
            // TODO: try to avoid extra page creation
            //
            Page pageToPush = (Page)_pageContainer.GetPageByType(pageType);

            Page relativePageFromNavigationStack =
                Application.Current.MainPage.Navigation.NavigationStack
                .FirstOrDefault(p => p.GetType() == pageToPush.GetType());

            if (relativePageFromNavigationStack != null) {
                MoveToTheExistingPageInNavigationStack(relativePageFromNavigationStack);
            }
            else {
                ApplyVisualChangesWhileNavigating(Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault());

                PushPage(pageToPush);
            }
        }

        /// <summary>
        /// TODO: improve async navigating
        /// </summary>
        public Task NavigateToAsync(PageTypes pageType) {
            return Task.Run(() => {
                Page pageToPush = (Page)_pageContainer.GetPageByType(pageType);

                Page relativePageFromNavigationStack =
                    Application.Current.MainPage.Navigation.NavigationStack
                    .FirstOrDefault(p => p.GetType() == pageToPush.GetType());

                if (relativePageFromNavigationStack != null) {
                    Device.BeginInvokeOnMainThread(() => {
                        MoveToTheExistingPageInNavigationStack(relativePageFromNavigationStack); 
                    });
                }
                else {
                    Device.BeginInvokeOnMainThread(() => {
                        ApplyVisualChangesWhileNavigating(Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault());

                        PushPage(pageToPush);
                    });
                }
            });
        }

        /// <summary>
        /// Removes the last page from navigation stack. Root page will not be exclude from navigation stack. 
        /// </summary>
        public void NavigateOneStepBack() {
            Application.Current.MainPage.Navigation.PopAsync(false);
        }

        /// <summary>
        /// Get page of the appropriate type
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public INavigatedPage GetPageByType(PageTypes pageType) {
            return _pageContainer.GetPageByType(pageType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public void DisplayWebViewPage(string uri) {
            //
            // TODO: before inserting the page check if is not also exist in page stack.
            //
            WebViewPage webViewPage = (WebViewPage)GetPageByType(PageTypes.WebViewPage);
            webViewPage.SrcPath = uri;
            PushPage(webViewPage);
        }

        /// <summary>
        /// Get page of the appropriate type wrapped by Xamarin.Forms.NavigationPage
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        private NavigationPage GetPageInNavigationFrameByType(PageTypes pageType) {
            return _pageContainer.GetPageInNavigationFrameByType(pageType);
        }

        /// <summary>
        /// PageToGoTo must be also setted in navigation stack.
        /// Pages which goes after 'pageToGoTo' simply will be poped without animations.
        /// </summary>
        /// <param name="pageToGoTo"></param>
        private async void MoveToTheExistingPageInNavigationStack(Page pageToGoTo) {
            List<Page> pagesToLeaveInStack = new List<Page>();

            foreach (Page page in Application.Current.MainPage.Navigation.NavigationStack) {
                pagesToLeaveInStack.Add(page);

                if (page.Equals(pageToGoTo)) {
                    break;
                }
            }

            int timesToPop = Application.Current.MainPage.Navigation.NavigationStack
                .Except(pagesToLeaveInStack).Count();

            for (int i = 0; i < timesToPop; i++) {
                await Application.Current.MainPage.Navigation.PopAsync(false);
            }
        }

        /// <summary>
        /// Pushes 'pageToPush' to the Navigation stack.
        /// </summary>
        /// <param name="targetPage"></param>
        private async void PushPage(Page pageToPush) {
            await Application.Current.MainPage.Navigation.PushAsync(pageToPush, false);
        }

        /// <summary>
        /// Forse to make 'navigation' visual changes for target page.
        /// </summary>
        private void ApplyVisualChangesWhileNavigating(Page targetPage) {
            try {
                ((INavigatedPage)targetPage).ApplyVisualChangesWhileNavigating();
            }
            catch (NullReferenceException exc) {
                throw new InvalidOperationException(string.Format("ViewSwitchingLogic.ApplyVisualChangesWhileNavigating - {0}. Exception details - {1}",
                    _ERROR_NAVIGATION_STACK_IS_EMPTY, exc.Message));
            }
            catch (Exception exc) {
                throw new InvalidOperationException(string.Format("ViewSwitchingLogic.ApplyVisualChangesWhileNavigating - {0}, Exception details - {1}",
                    _ERROR_INVALID_PAGE_IN_NAVIGATION_STACK, exc.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        //private void ComposeActionBarAndPage(BasePage targetPage, ActionBarBase targetActionBar) {
        //    if (targetPage.ActionBar != null &&
        //        targetPage.ActionBar.GetType() == targetActionBar.GetType()) {
        //        return;
        //    }

        //    targetPage.ActionBar = targetActionBar;
        //}
    }
}