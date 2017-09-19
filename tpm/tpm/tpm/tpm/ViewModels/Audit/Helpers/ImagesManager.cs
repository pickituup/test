using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels.Audit.Helpers {
    public sealed class ImagesManager {
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
