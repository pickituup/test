using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public abstract class ViewModelBase : ObservableObject {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task DisplayAlert(string title, string message, string cancel = "OK") {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="accept"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel) {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}