using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models;

namespace tpm.ViewModels.Audit.Helpers {
    public sealed class CommentsManager {
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
}