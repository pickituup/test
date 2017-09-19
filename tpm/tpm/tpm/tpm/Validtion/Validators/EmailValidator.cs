using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tpm.Validtion.Validators {
    public sealed class EmailValidator : IValidator {

        /// <summary>
        /// Valid condition
        /// </summary>
        private readonly Regex _emailRegex = new Regex("^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatingString"></param>
        /// <returns></returns>
        public bool IsValid(string validatingEmail) {
            bool isValid = true;

            if (!_emailRegex.IsMatch(validatingEmail)) {
                isValid = false;
            }

            return isValid;
        }
    }
}
