using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public class QuestionReviewViewViewModel : ViewModelBase, IViewModel {

        private List<AttachedComment> _comments = 
            new List<AttachedComment>();
        private List<AttachedImage> _images =
           new List<AttachedImage>();

        private Question _question;
        private bool _isVisibleImagesSection = false,
            _isVisibleCommentsSection = false,
            _isVisibleIfNoSection = false;
        private int _commentsCount,
            _imagesCount;

        /// <summary>
        /// 
        /// </summary>
        public QuestionReviewViewViewModel(Question question) {
            Question = question;

            CommentsCount = _question.Comments.Count;
            ImagesCount = _question.Images.Count;
            IsVisibleIfNoSection = !(_question.Answer.Value);

            ToggleCommentsVisibilityCommand = new Command(() => {
                IsVisibleImagesSection = false;
                IsVisibleCommentsSection = !IsVisibleCommentsSection;
            });

            ToggleImagesVisibilityCommand = new Command(() => {
                IsVisibleCommentsSection = false;
                IsVisibleImagesSection = !IsVisibleImagesSection;
            });
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public ICommand ToggleCommentsVisibilityCommand {
            get;
            private set;
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public ICommand ToggleImagesVisibilityCommand {
            get;
            private set;
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public Question Question {
            get => _question;
            private set => SetProperty<Question>(ref _question, value);
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public bool IsVisibleImagesSection {
            get => _isVisibleImagesSection;
            set => SetProperty<bool>(ref _isVisibleImagesSection, value);
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public bool IsVisibleCommentsSection {
            get => _isVisibleCommentsSection;
            set => SetProperty<bool>(ref _isVisibleCommentsSection, value);
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public bool IsVisibleIfNoSection {
            get => _isVisibleIfNoSection;
            private set => SetProperty<bool>(ref _isVisibleIfNoSection, value);
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public int CommentsCount {
            get => _commentsCount;
            private set => SetProperty<int>(ref _commentsCount, value);
        }

        /// <summary>
        /// TODO: that behavior is the same as in the QuestionViewModel - very important to move that to the abstract ViewModel layer. And it will be used by both QuestionViewMOdel and QuestionReviewViewModel
        /// </summary>
        public int ImagesCount {
            get => _imagesCount;
            private set => SetProperty<int>(ref _imagesCount, value);
        }
    }
}
