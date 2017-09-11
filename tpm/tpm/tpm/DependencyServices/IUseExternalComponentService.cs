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
        /// <param name="src"></param>
        void IntentToOpenWebSource(string src);

        /// <summary>
        /// Forms intent to sent the PDF to the email
        /// TODO: can be removed to the IUseExternalComponentService
        /// </summary>
        void IntentToSentMailWithPDF(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        void IntentToDisplayRelativeFile(string fileName);
    }
}
