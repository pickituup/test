using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Models.DataContainers.DataItems {
    public sealed class WebinarItem {

        /// <summary>
        /// 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SourcePath { get; set; }
    }
}