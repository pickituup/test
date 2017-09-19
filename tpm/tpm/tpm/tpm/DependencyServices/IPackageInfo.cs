using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.DependencyServices {
    public interface IPackageInfo {

        /// <summary>
        /// Version number (build version).
        /// </summary>
        string VersionNumber { get; }

        /// <summary>
        /// Readable version
        /// </summary>
        string VersionName { get; }
    }
}
