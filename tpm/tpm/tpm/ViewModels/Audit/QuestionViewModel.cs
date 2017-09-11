using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Models;
using Xamarin.Forms;
using tpm.DependencyServices;

namespace tpm.ViewModels {
    public sealed class QuestionViewModel : ViewModelBase, IViewModel {

        private CommentsManager _commentsManager;
        private ImagesManager _imagesManager;
        private Question _question;

        private Nullable<bool> _questionAnswer;
        private bool _isVisibleCommentsSection = false,
            _isVisibleImagesSection = false,
            _isVisibleComentEnrySection = false,
            _isVisibleIfNoSection = false;
        private int _commentsCount,
            _imagesCount;
        private string _typedCommentsMessage;
        //
        // TODO: try to use value converter to convert Yes/No images to the appropriate image
        //
        private ImageSource _yesButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_ok_transparent_bg_with_border.png");
        private ImageSource _noButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_decline_transparent_bg_with_border.png");

        private ObservableCollection<CommentViewModel> _interactiveComments;
        private ObservableCollection<ImageViewModel> _interactiveImages;

        /// <summary>
        /// Check if all questions are answered. Action was provided by QuestionListViewModel.
        /// </summary>
        private Action CheckCompletionAction;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public QuestionViewModel(Question question, Action checkCompletionAction) {
            Question = question;

            SetupQuestionUnswer();
            SetupCommentsSupport();
            SetupImagesSupport();
            SetupCommands();

            CheckCompletionAction = checkCompletionAction;
        }

        /// <summary>
        /// Toggle comments section visibility
        /// </summary>
        public ICommand ToggleCommentsVisibilityCommand {
            get;
            private set;
        }

        /// <summary>
        /// Toggle images section visibility
        /// </summary>
        public ICommand ToggleImagesVisibilityCommand {
            get;
            private set;
        }

        /// <summary>
        /// TODO: that command must display entry for typing the comment...
        /// </summary>
        public ICommand DisplayComentEntryCommand {
            get;
            private set;
        }

        /// <summary>
        /// Add's image to the question
        /// </summary>
        public ICommand AddImageCommand {
            get;
            private set;
        }

        /// <summary>
        /// Add's comment to the question
        /// </summary>
        public ICommand AddCommentCommand {
            get;
            private set;
        }

        /// <summary>
        /// Yes answer command
        /// </summary>
        public ICommand YesAnswerCommand {
            get;
            private set;
        }

        /// <summary>
        /// No answer command
        /// </summary>
        public ICommand NoAnswerCommand {
            get;
            private set;
        }

        /// <summary>
        /// Decline leaving
        /// </summary>
        public ICommand DeclineComentTypingCommand {
            get;
            private set;
        }

        /// <summary>
        /// Close if no section
        /// </summary>
        public ICommand CloseIfNoSectionCommand {
            get;
            private set;
        }

        /// <summary>
        /// Question instance
        /// </summary>
        public Question Question {
            get => _question;
            private set => SetProperty<Question>(ref _question, value);
        }

        /// <summary>
        /// Question answer. 
        /// Can be NULL if question havent answer, TRUE - ok, FALSE - no.
        /// </summary>
        public Nullable<bool> QuestionAnswer {
            get => _questionAnswer;
            private set => SetProperty<Nullable<bool>>(ref _questionAnswer, value);
        }

        /// <summary>
        /// Controls visibility of comments section.
        /// </summary>
        public bool IsVisibleCommentsSection {
            get => _isVisibleCommentsSection;
            set => SetProperty<bool>(ref _isVisibleCommentsSection, value);
        }

        /// <summary>
        /// Controls visibility of images section.
        /// </summary>
        public bool IsVisibleImagesSection {
            get => _isVisibleImagesSection;
            set => SetProperty<bool>(ref _isVisibleImagesSection, value);
        }

        /// <summary>
        /// Controls visibility of coment entry section.
        /// </summary>
        public bool IsVisibleComentEnrySection {
            get => _isVisibleComentEnrySection;
            private set => SetProperty<bool>(ref _isVisibleComentEnrySection, value);
        }

        /// <summary>
        /// Controls visibility of if no section.
        /// </summary>
        public bool IsVisibleIfNoSection {
            get => _isVisibleIfNoSection;
            private set => SetProperty<bool>(ref _isVisibleIfNoSection, value);
        }

        /// <summary>
        /// Displays count of comments
        /// </summary>
        public int CommentsCount {
            get => _commentsCount;
            private set => SetProperty<int>(ref _commentsCount, value);
        }

        /// <summary>
        /// Displays count of images
        /// </summary>
        public int ImagesCount {
            get => _imagesCount;
            private set => SetProperty<int>(ref _imagesCount, value);
        }

        /// <summary>
        /// Holds the actual typed comments message.
        /// </summary>
        public string TypedCommentsMessage {
            get => _typedCommentsMessage;
            set => SetProperty<string>(ref _typedCommentsMessage, value);
        }

        /// <summary>
        /// TODO: try to use value converter to convert Yes/No images to the appropriate image
        /// Yes button image
        /// </summary>
        public ImageSource YesButtonImage {
            get => _yesButtonImage;
            private set => SetProperty<ImageSource>(ref _yesButtonImage, value);
        }

        /// <summary>
        /// TODO: try to use value converter to convert Yes/No images to the appropriate image
        /// No button image
        /// </summary>
        public ImageSource NoButtonImage {
            get => _noButtonImage;
            private set => SetProperty<ImageSource>(ref _noButtonImage, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<CommentViewModel> InteractiveComments {
            get => _interactiveComments;
            private set => SetProperty<ObservableCollection<CommentViewModel>>(ref _interactiveComments, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ImageViewModel> InteractiveImages {
            get => _interactiveImages;
            private set => SetProperty<ObservableCollection<ImageViewModel>>(ref _interactiveImages, value);
        }

        /// <summary>
        /// Just toogle visibility of coment entry section.
        /// true - TypedCommentsMessage will be cleared, false - will be the same.
        /// </summary>
        private void ToogleVisibilityOfComentEntrySection(bool clearTypedCommentsMessage) {
            if (clearTypedCommentsMessage) {
                TypedCommentsMessage = "";
            }

            IsVisibleComentEnrySection = !IsVisibleComentEnrySection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newAnswer"></param>
        private void ChangeQuestionAnswer(bool newAnswer) {
            QuestionAnswer = newAnswer;
            Question.Answer = QuestionAnswer.Value;

            if (newAnswer) {
                YesButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_ok_fill_green_bg.png");
                NoButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_decline_no_border.png");
            }
            else {
                YesButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_ok_no_border.png");
                NoButtonImage = ImageSource.FromResource("tpm.Resourses.Img.ic_decline_fill_red_bg.png");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInteractiveCommentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            CommentsCount = ((IList)sender).Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInteractiveImagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            ImagesCount = ((IList)sender).Count;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetupQuestionUnswer() {
            if (Question.Answer.HasValue) {
                ChangeQuestionAnswer(Question.Answer.Value);
            }
        }

        /// <summary>
        /// Helping method just groups all comments optionings
        /// </summary>
        private void SetupCommentsSupport() {
            _commentsManager = new CommentsManager(Question, UpdateCurrentQuestionState);

            InteractiveComments = _commentsManager.InteractiveComments;
            //
            // TODO: unsubscribe
            //
            InteractiveComments.CollectionChanged += OnInteractiveCommentsCollectionChanged;

            CommentsCount = InteractiveComments.Count;
        }

        /// <summary>
        /// Helping method just groups all images optionings
        /// </summary>
        private void SetupImagesSupport() {
            _imagesManager = new ImagesManager(Question, UpdateCurrentQuestionState);

            InteractiveImages = _imagesManager.InteractiveImages;
            //
            // TODO: unsubscribe
            //
            InteractiveImages.CollectionChanged += OnInteractiveImagesCollectionChanged;

            ImagesCount = InteractiveImages.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetupCommands() {
            ToggleCommentsVisibilityCommand = new Command(() => {
                IsVisibleImagesSection = false;
                IsVisibleCommentsSection = !IsVisibleCommentsSection;
            });

            ToggleImagesVisibilityCommand = new Command(() => {
                IsVisibleCommentsSection = false;
                IsVisibleImagesSection = !IsVisibleImagesSection;
            });

            DisplayComentEntryCommand = new Command(() => {
                ToogleVisibilityOfComentEntrySection(false);
            });

            AddImageCommand = new Command(async () => {
                await _imagesManager.AddImageAsync();

                UpdateCurrentQuestionState();
            });

            DeclineComentTypingCommand = new Command(() => {
                ToogleVisibilityOfComentEntrySection(true);
            });

            AddCommentCommand = new Command(() => {
                if (string.IsNullOrEmpty(TypedCommentsMessage) || string.IsNullOrWhiteSpace(TypedCommentsMessage)) {
                    return;
                }

                _commentsManager.AddComment(TypedCommentsMessage);
                UpdateCurrentQuestionState();

                ToogleVisibilityOfComentEntrySection(true);
            });

            YesAnswerCommand = new Command(() => {
                ChangeQuestionAnswer(true);
                UpdateCurrentQuestionState();
            });

            NoAnswerCommand = new Command(() => {
                ChangeQuestionAnswer(false);

                UpdateCurrentQuestionState();

                IsVisibleIfNoSection = true;
            });

            CloseIfNoSectionCommand = new Command(() => {
                IsVisibleIfNoSection = false;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateCurrentQuestionState() {
            DependencyService.Get<ISqlLiteService>().UpdateQuestion(_question, AuditPageViewModel.User.Id);
            CheckCompletionAction();
        }
    }

    /// <summary>
    /// TEMPORARY
    /// TODO: define factory
    /// </summary>
    public class CommentsManager {

        private Action UpdateCurrentQuestionStateAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        public CommentsManager(Question question, Action updateCurrentQuestionStateAction) {
            InteractiveComments = new ObservableCollection<CommentViewModel>();
            QuestionComments = question.Comments;
            UpdateCurrentQuestionStateAction = updateCurrentQuestionStateAction;

            foreach (AttachedComment attachedComment in QuestionComments) {
                InteractiveComments.Add(ConstructInteractiveComment(attachedComment));
            }
        }

        /// <summary>
        /// Real comments also attached to the appropriate Question
        /// </summary>
        public IList<AttachedComment> QuestionComments { get; private set; }

        /// <summary>
        /// Comments view models.
        /// </summary>
        public ObservableCollection<CommentViewModel> InteractiveComments { get; private set; }

        /// <summary>
        /// Creates and attach comment to the appropriate question
        /// TODO: will take some attributes for potential comment.
        /// </summary>
        public void AddComment(string commentMessage) {
            //AttachedComment newComment = new AttachedComment() {
            //    CommentMessage = "Dynamicaly attached comment",
            //    PublishDate = DateTime.Now
            //};

            AttachedComment newComment = new AttachedComment() {
                PublishDate = DateTime.Now,
                CommentMessage = commentMessage
            };

            QuestionComments.Add(newComment);
            InteractiveComments.Add(ConstructInteractiveComment(newComment));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private CommentViewModel ConstructInteractiveComment(AttachedComment comment) {
            return new CommentViewModel(comment, DeleteCommentAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentViewModelToDelete"></param>
        /// <param name="commentToDelete"></param>
        private void DeleteCommentAction(CommentViewModel commentViewModelToDelete, AttachedComment commentToDelete) {
            InteractiveComments.Remove(commentViewModelToDelete);
            QuestionComments.Remove(commentToDelete);

            UpdateCurrentQuestionStateAction();
        }
    }

    /// <summary>
    /// TEMPORARY
    /// TODO: define factory
    /// </summary>
    public class ImagesManager {

        private Action UpdateCurrentQuestionStateAction;

        /// <summary>
        /// Public ctor.
        /// </summary>
        /// <param name="question"></param>
        public ImagesManager(Question question, Action updateCurrentQuestionStateAction) {
            InteractiveImages = new ObservableCollection<ImageViewModel>();
            QuestionImages = question.Images;
            UpdateCurrentQuestionStateAction = updateCurrentQuestionStateAction;

            foreach (AttachedImage attachedImage in QuestionImages) {
                InteractiveImages.Add(ConstructInteractiveImage(attachedImage));
            }
        }

        /// <summary>
        /// Real images also attached to the appropriate Question
        /// </summary>
        public IList<AttachedImage> QuestionImages { get; private set; }

        /// <summary>
        /// Images view models.
        /// </summary>
        public ObservableCollection<ImageViewModel> InteractiveImages { get; private set; }

        /// <summary>
        /// Creates and attach comment to the appropriate question
        /// TODO: will take some attributes for potential comment.
        /// </summary>
        public async Task AddImageAsync() {
            string base64photo = await DependencyService.Get<IPickImageService>().CatchPhoto();

            if (base64photo == null) {
                return;
            }

            AttachedImage newImage = new AttachedImage() {
                Base64Image = base64photo
            };

            QuestionImages.Add(newImage);
            InteractiveImages.Add(ConstructInteractiveImage(newImage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private ImageViewModel ConstructInteractiveImage(AttachedImage image) {
            return new ImageViewModel(image, DeleteImageAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentViewModelToDelete"></param>
        /// <param name="commentToDelete"></param>
        private void DeleteImageAction(ImageViewModel imageViewModelToDelete, AttachedImage imageToDelete) {
            InteractiveImages.Remove(imageViewModelToDelete);
            QuestionImages.Remove(imageToDelete);
            UpdateCurrentQuestionStateAction();
        }
    }
}