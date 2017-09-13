using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using tpm.Views.CompoundedViews.Menu;
using System.Collections.ObjectModel;
using tpm.NavigationFramework;
using tpm.Models;
using tpm.Views.CompoundedViews;
using System.IO;
using tpm.DependencyServices;
using Xfinium.Pdf;
using Xfinium.Pdf.Graphics;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Helpers.Observers.Args;
using tpm.Pdf;

namespace tpm.ViewModels {

    /// <summary>
    /// 
    /// </summary>
    public sealed class AuditPageViewModel : BasePageViewModel, IViewModel, IListenerViewModel {

        private static readonly string _auditHeader = "Audit",
            _reviewHeader = "Review";

        private string _actionBarHeader;
        private bool _isVisibleCompletededSection = false;
        private bool _isOneStepBackAvailable = false;
        private IAuditView _viewSection;

        private PdfDrawing _pdfDrawing = new PdfDrawing();

        /// <summary>
        /// Public ctor.
        /// </summary>
        public AuditPageViewModel() {
            MenuItems.First(i => i.PageType == PageTypes.AuditPage).IsSelected = true;
            ActionBarHeader = _actionBarHeader = _auditHeader;

            IsOneStepBackAvailable = false;
            ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.AssesmentView);

            CompletedCommand = new Command(async () => {

                if (IsVisibleCompletededSection) {
                    ActionBarHeader = _reviewHeader;
                    await UpdateViewSectionAsync(AuditViewTypes.ReviewView);
                }
                else {
                    await DisplayAlert("Audit", "Answer all questions");
                }
                //
                // Old valid behaviour when command was binded with appropriate button from the action bar.
                //
                //ActionBarHeader = _reviewHeader;
                //IsVisibleCompletededSection = false;

                ////ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.ReviewView);
                //await UpdateViewSectionAsync(AuditViewTypes.ReviewView);
            });

            StepBackCommand = new Command(() => {
                AuditOneStepBackViewNavigation();
            });
        }

        /// <summary>
        /// Represents the user which will go through audit. Can be null...
        /// TODO: maby use some singletone of profile helper...
        /// </summary>
        public static User User {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOneStepBackAvailable {
            get => _isOneStepBackAvailable;
            private set => SetProperty<bool>(ref _isOneStepBackAvailable, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand StepBackCommand {
            get;
            private set;
        }

        /// <summary>
        /// Assesment completed command.
        /// </summary>
        public ICommand CompletedCommand {
            get;
            private set;
        }

        /// <summary>
        /// TODO: refactor for new completed logic
        /// </summary>
        public bool IsVisibleCompletededSection {
            get => _isVisibleCompletededSection;
            private set => SetProperty<bool>(ref _isVisibleCompletededSection, value);
        }

        /// <summary>
        /// Appropriate section will be loaded here...
        /// </summary>
        public IAuditView ViewSection {
            get => _viewSection;
            private set {
                SetProperty<IAuditView>(ref _viewSection, value);
                SwapMenuOneStapBackButtons();
            }
        }

        /// <summary>
        /// BasePageViewModel implementation
        /// </summary>
        public override string ActionBarHeader {
            get => _actionBarHeader;
            protected set => SetProperty<string>(ref _actionBarHeader, value);
        }

        /// <summary>
        /// IListenerViewModel implementation
        /// </summary>
        public void Subscribe() {
            BaseSingleton<AuditScopeObserver>.Instance.StartAssesment += OnStartAssesment;
            BaseSingleton<AuditScopeObserver>.Instance.AuditOneStepBack += OnAuditOneStepBack;
            BaseSingleton<AuditScopeObserver>.Instance.BeginAnsweringTheQuestions += OnBeginAnsweringTheQuestions;
            BaseSingleton<AuditScopeObserver>.Instance.AssessmentCompleted += OnAssessmentCompleted;
            BaseSingleton<AuditScopeObserver>.Instance.StartOver += OnStartOver;
            BaseSingleton<AuditScopeObserver>.Instance.PreparePublishing += OnPreparePublishing;
            BaseSingleton<AuditScopeObserver>.Instance.GoHome += OnGoHome;
            BaseSingleton<AuditScopeObserver>.Instance.Publish += OnPublish;
        }

        /// <summary>
        /// IListenerViewModel implementation
        /// </summary>
        public void UnSubscribe() {
            BaseSingleton<AuditScopeObserver>.Instance.StartAssesment -= OnStartAssesment;
            BaseSingleton<AuditScopeObserver>.Instance.AuditOneStepBack -= OnAuditOneStepBack;
            BaseSingleton<AuditScopeObserver>.Instance.BeginAnsweringTheQuestions -= OnBeginAnsweringTheQuestions;
            BaseSingleton<AuditScopeObserver>.Instance.AssessmentCompleted -= OnAssessmentCompleted;
            BaseSingleton<AuditScopeObserver>.Instance.StartOver -= OnStartOver;
            BaseSingleton<AuditScopeObserver>.Instance.PreparePublishing -= OnPreparePublishing;
            BaseSingleton<AuditScopeObserver>.Instance.GoHome -= OnGoHome;
            BaseSingleton<AuditScopeObserver>.Instance.Publish -= OnPublish;
        }

        /// <summary>
        /// Use only for back button press in Audit page
        /// </summary>
        public async void AuditOneStepBackViewNavigation() {
            //
            // TODO: remove to the Audit ViewSwitchingLogic...
            //
            if (ViewSection.GetType() == typeof(AssesmentView)) {
                NavigateOneStepBack();
            }
            else if (ViewSection.GetType() == typeof(EnterInfoView)) {
                //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.AssesmentView);
                await UpdateViewSectionAsync(AuditViewTypes.AssesmentView);
            }
            else if (ViewSection.GetType() == typeof(QuestionsListView)) {
                //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.AssesmentView);
                await UpdateViewSectionAsync(AuditViewTypes.AssesmentView);
                IsVisibleCompletededSection = false;
            }
            else if (ViewSection.GetType() == typeof(ReviewView)) {
                ActionBarHeader = _auditHeader;
                //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.QuestionsListView);
                await UpdateViewSectionAsync(AuditViewTypes.QuestionsListView);
            }
            else if (ViewSection.GetType() == typeof(PublishView)) {
                ActionBarHeader = _reviewHeader;
                //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.ReviewView);
                await UpdateViewSectionAsync(AuditViewTypes.ReviewView);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGoHome(object sender, EventArgs e) {
            User = null;
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }

        /// <summary>
        /// Subscribtion on AuditNavigationObserver.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnStartAssesment(object sender, EventArgs e) {

            //
            // TODO: try to make async call
            // TODO: dispose user when we left the Audit page
            //
            User = DependencyService.Get<ISqlLiteService>().GetFirstOrDefaultUser();

            if (User == null) {
                //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.EnterInfoView);
                await UpdateViewSectionAsync(AuditViewTypes.EnterInfoView);
            }
            else {
                //
                // TODO: display questions list section
                //
                BaseSingleton<AuditScopeObserver>.Instance.OnBeginAnsweringTheQuestions(User);
            }
        }

        /// <summary>
        /// Subscribtion on AuditNavigationObserver.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAuditOneStepBack(object sender, EventArgs e) {
            AuditOneStepBackViewNavigation();
        }

        /// <summary>
        /// Subscribtion on AuditNavigationObserver.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnBeginAnsweringTheQuestions(object sender, UserEventArgs e) {
            User = e.User;

            //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.QuestionsListView);
            await UpdateViewSectionAsync(AuditViewTypes.QuestionsListView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAssessmentCompleted(object sender, EventArgs e) {
            IsVisibleCompletededSection = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnStartOver(object sender, EventArgs e) {
            User = null;
            DependencyService.Get<ISqlLiteService>().DeleteAllUsers();

            IsVisibleCompletededSection = false;
            //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.EnterInfoView);
            await UpdateViewSectionAsync(AuditViewTypes.EnterInfoView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPreparePublishing(object sender, EventArgs e) {
            //await DisplayAlert("Publish", "PDF drawing and mail sending logic");
            ActionBarHeader = _auditHeader;

            //ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(AuditViewTypes.PublishView);
            await UpdateViewSectionAsync(AuditViewTypes.PublishView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPublish(object sender, PublishEventArgs e) {
            IsAwaiting = true;

            await _pdfDrawing.DrawAsync(User);
            DependencyService.Get<IUseExternalComponentService>().IntentToSentMailWithPDF(e.Email);
            //DependencyService.Get<IFileHelper>().IntentToDisplayPDF();

            IsAwaiting = false;
        }

        /// <summary>
        /// Test implementation
        /// </summary>
        /// <param name="auditViewType"></param>
        /// <returns></returns>
        private Task UpdateViewSectionAsync(AuditViewTypes auditViewType) {
            return Task.Run(() => {
                IsAwaiting = true;

                ViewSection = BaseSingleton<AuditSwitchingLogic>.Instance.GetViewByType(auditViewType);

                IsAwaiting = false;
            });
        }

        private void SwapMenuOneStapBackButtons() {
            if (ViewSection.GetType() == typeof(AssesmentView)) {
                IsOneStepBackAvailable = false;

                return;
            }

            IsOneStepBackAvailable = true;
        }
    }
}