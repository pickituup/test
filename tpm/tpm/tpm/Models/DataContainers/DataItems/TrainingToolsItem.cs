using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace tpm.Models.DataContainers.DataItems {
    public sealed class TrainingToolsItem {

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VideoSourceLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageSource FirstItemIcon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstItemTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ImageSource SeondItemIcon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecondItemTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DownloadSrc { get; set; }
    }
}
