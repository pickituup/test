using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IPickImageService {

        /// <summary>
        /// 
        /// </summary>
        Task<string> CatchPhoto();
    }
}
