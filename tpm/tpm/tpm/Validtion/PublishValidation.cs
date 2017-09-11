using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Validtion.Validators;

namespace tpm.Validtion {
    public class PublishValidation {

        private static readonly string _ERROR_INVALID_EMAIL = "Invalid email";
        private EmailValidator _emailValidator = new EmailValidator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool IsValidEmail(string email, out string errorMessage) {
            bool isValid = true;
            errorMessage = string.Empty;

            if (!_emailValidator.IsValid(email)) {
                errorMessage = _ERROR_INVALID_EMAIL;
                isValid = false;
            }

            return isValid;
        }
    }
}
