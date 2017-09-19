using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Models {

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DataGroup<T> : List<T> where T : class {

        /// <summary>
        /// Describes the question group
        /// </summary>
        public string GroupHeader { get; set; }
    }
}
