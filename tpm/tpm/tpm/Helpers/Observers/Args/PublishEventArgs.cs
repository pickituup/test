using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Helpers.Observers.Args {
    public sealed class PublishEventArgs : EventArgs {

        /// <summary>
        /// Public ctor.
        /// </summary>
        /// <param name="email"></param>
        public PublishEventArgs(string email) {
            Email = email;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; private set; }
    }
}
