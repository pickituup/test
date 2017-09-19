using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IScreenService {

        /// <summary>
        /// Provide full screen landscape orientation
        /// </summary>
        void LandscapeFullScreen();

        /// <summary>
        /// Portrait default mode
        /// </summary>
        void DefaultOrientation();
    }
}
