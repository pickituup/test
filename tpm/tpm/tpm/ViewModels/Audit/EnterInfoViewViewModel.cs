using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Models;
using tpm.DependencyServices;

namespace tpm.ViewModels {
    public class EnterInfoViewViewModel : ViewModelBase, IViewModel {

        private string _userName,
            _comanyName,
            _companyAddress,
            _companyCityStateZip,
            _companySpecificLocation;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public EnterInfoViewViewModel() {
            BackCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnAuditOneStepBack();
            });

            NextCommand = new Command(() => {
                //Validation();
                User newUser = SeedUser();
                DependencyService.Get<ISqlLiteService>().AddUser(newUser);

                BaseSingleton<AuditScopeObserver>.Instance.OnBeginAnsweringTheQuestions(newUser);
            });
        }

        /// <summary>
        /// Starts back command
        /// </summary>
        public ICommand BackCommand {
            get;
            private set;
        }

        /// <summary>
        /// Starts back command
        /// </summary>
        public ICommand NextCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName {
            get => _userName;
            set => SetProperty<string>(ref _userName, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string ComanyName {
            get => _comanyName;
            set => SetProperty<string>(ref _comanyName, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyAddress {
            get => _companyAddress;
            set => SetProperty<string>(ref _companyAddress, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyCityStateZip {
            get => _companyCityStateZip;
            set => SetProperty<string>(ref _companyCityStateZip, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanySpecificLocation {
            get => _companySpecificLocation;
            set => SetProperty<string>(ref _companySpecificLocation, value);
        }

        /// <summary>
        /// TODO: remove form validation. It's not important to fill all the form...
        /// </summary>
        private async void Validation() {
            string[] inputs = { UserName, ComanyName, CompanyAddress, CompanyCityStateZip, CompanySpecificLocation };

            bool nullValidation = inputs.Any(s => string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s));

            if (nullValidation) {
                await DisplayAlert("Registration", "Fill all form");
                return;
            }
            
            User newUser = SeedUser();
            DependencyService.Get<ISqlLiteService>().AddUser(newUser);

            BaseSingleton<AuditScopeObserver>.Instance.OnBeginAnsweringTheQuestions(newUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private User SeedUser() {
            User newUser = new User() {
                UserName = UserName,
                CompanyName = ComanyName,
                CompanyAddress = CompanyAddress,
                CompanyCityStateZip = CompanyCityStateZip,
                CompanySpecificLocation = CompanySpecificLocation
            };

            newUser.Questions.AddRange(SeedQuestions());

            return newUser;
        }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<Question> SeedQuestions() {
            string generalGroup = "General";
            string laddersGroup = "Ladders";

            Question[] questions = {
                new Question() {
                    QuestionMessage = "Are all walking-working surfaces kept clean and orderly?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are working surfaces kept clean and dry?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are walking-working surfaces and surrounding areas kept free of sharp or protruding objects?",
                    IfNoAnswer = "Remove the sharp or protruding objects or install a barrier to prevent contact.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are permanent aisles and passageways marked and kept clear?",
                    IfNoAnswer = "Mark and maintain the markings on permanent aisles and passageways.Establish a practice that aisles and passageways be kept clear.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Level and uniformly spaced? (Make the determination when the ladder in in position for use.) ",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Spaced not less than 10 inches nor more than 14 inches apart?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 11.5 inches on portable ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 16 inches for fixed ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are all walking-working surfaces kept clean and orderly?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are working surfaces kept clean and dry?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are walking-working surfaces and surrounding areas kept free of sharp or protruding objects?",
                    IfNoAnswer = "Remove the sharp or protruding objects or install a barrier to prevent contact.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are permanent aisles and passageways marked and kept clear?",
                    IfNoAnswer = "Mark and maintain the markings on permanent aisles and passageways.Establish a practice that aisles and passageways be kept clear.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Level and uniformly spaced? (Make the determination when the ladder in in position for use.) ",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Spaced not less than 10 inches nor more than 14 inches apart?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 11.5 inches on portable ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 16 inches for fixed ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are all walking-working surfaces kept clean and orderly?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are working surfaces kept clean and dry?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are walking-working surfaces and surrounding areas kept free of sharp or protruding objects?",
                    IfNoAnswer = "Remove the sharp or protruding objects or install a barrier to prevent contact.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are permanent aisles and passageways marked and kept clear?",
                    IfNoAnswer = "Mark and maintain the markings on permanent aisles and passageways.Establish a practice that aisles and passageways be kept clear.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Level and uniformly spaced? (Make the determination when the ladder in in position for use.) ",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Spaced not less than 10 inches nor more than 14 inches apart?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 11.5 inches on portable ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Have a minimum width of 16 inches for fixed ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                }
            };

            return questions;
        }
    }
}