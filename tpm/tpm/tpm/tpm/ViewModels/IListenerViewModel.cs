using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.ViewModels {
    public interface IListenerViewModel {

        /// <summary>
        /// 
        /// </summary>
        void Subscribe();

        /// <summary>
        /// 
        /// </summary>
        void UnSubscribe();
    }
}
