using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public class ReviewViewViewModel : ViewModelBase, IViewModel {

        private ObservableCollection<DataGroup<QuestionReviewViewViewModel>> _questionsReviewList =
            new ObservableCollection<DataGroup<QuestionReviewViewViewModel>>();
        private User _user;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public ReviewViewViewModel() {
            OrganiseDataGroups();
            User = AuditPageViewModel.User;

            BackCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnAuditOneStepBack();
            });

            StartOverCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnStartOver();
            });

            PublishCommand = new Command(() => {

                //
                // TODO: place to force AuditPageViewModel to save new User data to the DB.
                //

                BaseSingleton<AuditScopeObserver>.Instance.OnPreparePublishing();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand BackCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand StartOverCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand PublishCommand {
            get;
            private set;
        }

        /// <summary>
        /// Will include ViewModels for each question review. Each VM will contain appropriate Question model inside.
        /// </summary>
        public ObservableCollection<DataGroup<QuestionReviewViewViewModel>> QuestionsReviewList {
            get => _questionsReviewList;
            private set => SetProperty<ObservableCollection<DataGroup<QuestionReviewViewViewModel>>>(ref _questionsReviewList, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public User User {
            get => _user;
            private set => SetProperty<User>(ref _user, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OrganiseDataGroups() {
            User user = AuditPageViewModel.User;

            List<DataGroup<QuestionReviewViewViewModel>> output =
               new List<DataGroup<QuestionReviewViewViewModel>>();

            var groupedResult = user.Questions
                .GroupBy<Question, string>(q => q.Group);

            //
            // TODO: use LINQ for definition of QuestionGroups list.
            //
            foreach (var group in groupedResult) {
                DataGroup<QuestionReviewViewViewModel> questionreviewGroup =
                    new DataGroup<QuestionReviewViewViewModel>() { GroupHeader = group.Key };

                foreach (var question in group) {
                    QuestionReviewViewViewModel questionReviewViewModel = new QuestionReviewViewViewModel(question);

                    questionreviewGroup.Add(questionReviewViewModel);
                }

                output.Add(questionreviewGroup);
            }

            int index = 1;

            foreach (DataGroup<QuestionReviewViewViewModel> item in output) {
                foreach (QuestionReviewViewViewModel questionReviewViewModel in item) {
                    questionReviewViewModel.Question.Index = index;
                    index++;
                }
            }

            foreach (DataGroup<QuestionReviewViewViewModel> group in output) {
                QuestionsReviewList.Add(group);
            }
        }
    }
}
