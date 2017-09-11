using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class QuestionsListViewViewModel : ViewModelBase, IViewModel {

        private ObservableCollection<DataGroup<QuestionViewModel>> _questionsList =
            new ObservableCollection<DataGroup<QuestionViewModel>>();

        /// <summary>
        /// Public ctor.
        /// </summary>
        public QuestionsListViewViewModel() {
            OrganiseDataGroups();
            CheckCompletion();
        }

        /// <summary>
        /// Will include ViewModels for each question. Each VM will contain appropriate Question model inside.
        /// </summary>
        public ObservableCollection<DataGroup<QuestionViewModel>> QuestionsList {
            get => _questionsList;
            private set => SetProperty<ObservableCollection<DataGroup<QuestionViewModel>>>(ref _questionsList, value);
        }

        /// <summary>
        /// Check if all questions are answered
        /// </summary>
        private void CheckCompletion() {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
            foreach (DataGroup<QuestionViewModel> item in QuestionsList) {
                foreach (QuestionViewModel qusetionViewModel in item) {
                    questionViewModels.Add(qusetionViewModel);
                }
            }

            bool isNotCompleted = questionViewModels
                .Any<QuestionViewModel>(q => q.QuestionAnswer == null);

            if (!isNotCompleted) {
                BaseSingleton<AuditScopeObserver>.Instance.OnAssessmentCompleted();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OrganiseDataGroups() {
            User user = AuditPageViewModel.User;

            List<DataGroup<QuestionViewModel>> output =
                new List<DataGroup<QuestionViewModel>>();

            var groupedResult = user.Questions
                .GroupBy<Question, string>(q => q.Group);

            //
            // TODO: use LINQ for definition of QuestionGroups list.
            //
            foreach (var group in groupedResult) {
                DataGroup<QuestionViewModel> questionGroup =
                    new DataGroup<QuestionViewModel>() { GroupHeader = group.Key };

                foreach (var question in group) {
                    QuestionViewModel questionViewModel = new QuestionViewModel(question, CheckCompletion);

                    questionGroup.Add(questionViewModel);
                }

                output.Add(questionGroup);
            }

            int index = 1;

            foreach (DataGroup<QuestionViewModel> item in output) {
                foreach (QuestionViewModel questionViewModel in item) {
                    questionViewModel.Question.Index = index;
                    index++;
                }
            }

            foreach (DataGroup<QuestionViewModel> group in output) {
                QuestionsList.Add(group);
            }
        }
    }
}