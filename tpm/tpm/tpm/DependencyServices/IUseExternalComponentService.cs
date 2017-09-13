using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IUseExternalComponentService {

        /// <summary>
        ///
        /// </summary>
        void IntentToSentMailWithPDF(string email);

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="src"></param>
        //void IntentToOpenWebSource(string src);

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="fileName"></param>
        //void IntentToDisplayRelativeFile(string fileName);
    }
}
