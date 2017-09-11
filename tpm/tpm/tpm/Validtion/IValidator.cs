using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Validtion {
    public interface IValidator {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatingString"></param>
        /// <returns></returns>
        bool IsValid(string validatingString);
    }
}
