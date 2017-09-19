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
            string stairwaysGroup = "Stairways";
            string dockboardsGroup = "Dockboards";
            string fPSAFOPGroup = "Fall Protection Systems and Falling Object Protection";

            Question[] questions = {
                new Question() {
                    QuestionMessage = "Are all walking-working surfaces kept clean and orderly?",
                    IfNoAnswer = "Establish a system to maintain clean and orderly WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are working surfaces kept clean and dry?",
                    IfNoAnswer = "Establish a system to maintain clean and dry WWS.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are walking-working surfaces and surrounding areas kept free of sharp or protruding objects?",
                    IfNoAnswer = "Remove the sharp or protruding objects or install a barrier to prevent contact.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are permanent aisles and passageways marked and kept clear?",
                    IfNoAnswer = "Mark and maintain the markings on permanent aisles and passageways. Establish a practice that aisles and passageways be kept clear.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Is lighting adequate for all walking-working surfaces?",
                    IfNoAnswer = "Install lights where needed or re-lamp burned out lights.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are all hatchway and floor openings guarded?",
                    IfNoAnswer = "Guard all hatchway and floor openings.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are drainage ditches or open pits guarded?",
                    IfNoAnswer = "Provide guards and signage especially near boiler discharge.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are wet surfaces covered with non-slip materials?",
                    IfNoAnswer = "WWS that are continuously or frequently wet should be coated with non-slip materials.",
                    Group = generalGroup
                },
                new Question() {
                    QuestionMessage = "Are ladder rungs, steps and cleats Level and uniformly spaced? (Make the determination when the ladder in in position for use.)",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are ladder rungs, steps and cleats spaced not less than 10 inches nor more than 14 inches apart?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are ladder rungs, steps and cleats have a minimum width of 11.5 inches on portable ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are ladder rungs, steps and cleats have a minimum width of 16 inches for fixed ladders?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are ladder surfaces free of puncture and laceration hazards?",
                    IfNoAnswer = "Eliminate the hazards.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Are all ladders free of structural defects or damage?",
                    IfNoAnswer = "Repair or replace the ladder.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Do all fixed ladders have a minimum of 7 inches’ clearance from the centerline of the rung or step to the nearest fixed object?",
                    IfNoAnswer = "Relocate the ladder to conform to this standard.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Do grab bars extend 42 inches above the landing platform?",
                    IfNoAnswer = "Extend the grab bars.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Do all fixed ladders without cages have at least 15 inches of clear width on either side from the ladder centerline to the nearest permanent object?",
                    IfNoAnswer = "Relocate the ladder to conform to this standard.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Do all fixed ladders without cages have a minimum 30 inches’ clearance from the centerline of the rungs or steps to the nearest object on the climbing side of the ladder?",
                    IfNoAnswer = "Relocate the ladder to conform to this standard.",
                    Group = laddersGroup
                },
                new Question() {
                    QuestionMessage = "Is the vertical clearance above any stair tread to an overhead obstruction at least 6 feet 8 inches?",
                    IfNoAnswer = "Relocate the obstruction.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Do stairs have uniform riser heights and tread widths between landings?",
                    IfNoAnswer = "Repair or replace the stairs.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are stairway landings and platforms at least the width of the stair and at least 30 inches in depth measured in the direction of travel?",
                    IfNoAnswer = "Extend the platform.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are stairs installed at angles between 30-50 degrees from the horizontal?",
                    IfNoAnswer = "Repair or replace the stairs.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are riser heights 9.5 inches or less?",
                    IfNoAnswer = "Repair or replace the stairs.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Do stairs have a minimum width of 22 inches between vertical barriers?",
                    IfNoAnswer = "Repair, replace or relocate the stairs.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are fixed stairways provided with a railing on all open sides?",
                    IfNoAnswer = "Provide the required railings.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are closed stairways provided with a railing on at least one side?",
                    IfNoAnswer = "Provide a railing.",
                    Group = stairwaysGroup
                },
                new Question() {
                    QuestionMessage = "Are portable dockboards securely placed and anchored when in use?",
                    IfNoAnswer = "Stop the operation and securely place and anchor the dockboard.",
                    Group = dockboardsGroup
                },
                new Question() {
                    QuestionMessage = "Are dock locks or wheel chocks used to prevent transport vehicles from moving while dockboards are being used?",
                    IfNoAnswer = "Provide dock locks or chocks and ensure they are used.",
                    Group = dockboardsGroup
                },
                new Question() {
                    QuestionMessage = "Are there any walking-working surfaces with an unprotected side or edge that involve a potential to fall 4 feet or more to a lower level?",
                    IfNoAnswer = "Provide a guardrail system or personal fall protection system.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Do all fixed ladders that extend more than 24 feet above a lower level that were installed before November 19 2018 have a personal fall arrest system, ladder safety system, cage or well?",
                    IfNoAnswer = "Provide a personal fall arrest system, ladder safety system, cage or well.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Do all fixed ladders that extend more than 24 feet above a lower level that were installed after November 19, 2018 have a personal fall arrest system or ladder safety system?",
                    IfNoAnswer = "Provide a personal fall arrest system or ladder safety system.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "All installed safety ladder systems allow employees to use both hands for climbing up and down without having to continuously hold down, push or pull part of the system ? ",
                    IfNoAnswer = "Provide a safety ladder system that will allow employees to climb with both hands and not have to push, pull or hold down part of the system.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Are the top rails of a guard rail system at least 42 inches above the walking-working surface?",
                    IfNoAnswer = "Relocate the top rail to conform to this standard.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Are the mid rails of a guard rail system at least 21 inches above the walking-working surface?",
                    IfNoAnswer = "Relocate the mid rail to conform to this standard.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Are all elevated walking-working surfaces protected with toeboards which prevent objects from falling to a lower level?",
                    IfNoAnswer = "Install toeboards.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Are all toeboards at least 3.5 inches above the walking-working surface?",
                    IfNoAnswer = "Provide toeboards that conform to this standard.",
                    Group = fPSAFOPGroup
                },
                new Question() {
                    QuestionMessage = "Do all toeboards have less than .25 inches’ clearance from the bottom edge of the toeboard to the walking-working surface?",
                    IfNoAnswer = "Provide toeboards that conform to this standard.",
                    Group = fPSAFOPGroup
                }
            };

            return questions;
        }
    }
}