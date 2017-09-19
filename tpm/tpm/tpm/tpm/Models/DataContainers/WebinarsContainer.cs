using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models.DataContainers.DataItems;

namespace tpm.Models.DataContainers {
    public sealed class WebinarsContainer {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WebinarItem> BuildWebinarItems() {
            return new List<WebinarItem>() {
                new WebinarItem() {
                    Author = "Timber Products Mfr",
                    PublishDate = new DateTime(2017,6,13),
                    Title = "Building an Effective Safety Committee",
                    SourcePath = "http://www.youtube.com/embed/iY382tRVkSM"
                }
            };
        }
    }
}