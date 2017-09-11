using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models;

namespace tpm.Helpers.Observers.Args {
    public class UserEventArgs : EventArgs {

        /// <summary>
        /// 
        /// </summary>
        public UserEventArgs(User user) {
            User = user;
        }

        /// <summary>
        /// 
        /// </summary>
        public User User { get; private set; }
    }
}
