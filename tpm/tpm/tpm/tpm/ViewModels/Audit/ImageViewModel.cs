using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public class ImageViewModel : ViewModelBase, IViewModel {

        private Action<ImageViewModel, AttachedImage> _deleteAction;
        private AttachedImage _image;
        private string _base64Image;
        private ImageSource _imageSource;

        /// <summary>
        /// Parametered public ctor.
        /// </summary>
        public ImageViewModel(AttachedImage image, Action<ImageViewModel, AttachedImage> deleteAction) {
            DeleteImageCommand = new Command(() => {
                _deleteAction(this, this._image);
                _deleteAction = null;
            });

            _image = image;

            Base64Image = image.Base64Image;
            WorkOutBase64Image(Base64Image);

            _deleteAction = deleteAction;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand DeleteImageCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ImageSource ImageSource {
            get => _imageSource;
            private set => SetProperty<ImageSource>(ref _imageSource, value);
        }

        /// <summary>
        /// TODO: remove - not used
        /// </summary>
        public string Base64Image {
            get => _base64Image;
            private set => SetProperty<string>(ref _base64Image, value);
        }

        /// <summary>
        /// IViewModel implementation
        /// </summary>
        public void Subscribe() { }

        /// <summary>
        /// IViewModel implementation
        /// </summary>
        public void UnSubscribe() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64Image"></param>
        private void WorkOutBase64Image(string base64Image) {
            ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Base64Image)));
        }
    }
}