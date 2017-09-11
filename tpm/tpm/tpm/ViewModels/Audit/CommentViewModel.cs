using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using tpm.Models;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class CommentViewModel : ViewModelBase, IViewModel {

        private Action<CommentViewModel, AttachedComment> _deleteAction;
        private AttachedComment _comment;

        /// <summary>
        /// Parametered public ctor.
        /// </summary>
        public CommentViewModel(AttachedComment comment, Action<CommentViewModel, AttachedComment> deleteAction) {
            DeleteCommentCommand = new Command(() => {
                _deleteAction(this, this._comment);
                _deleteAction = null;
            });

            _comment = comment;

            _deleteAction = deleteAction;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand DeleteCommentCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public AttachedComment Comment {
            get => _comment;
            private set => SetProperty<AttachedComment>(ref _comment, value);
        }
    }
}