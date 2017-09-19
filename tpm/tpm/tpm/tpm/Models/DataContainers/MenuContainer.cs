using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models.DataContainers.DataItems;
using tpm.NavigationFramework;

namespace tpm.Models.DataContainers {
    public sealed class MenuContainer {

        /// <summary>
        /// Compound page types and their titles in to menu items
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> BuildMenuItems() {
            return new List<MenuItem> {
                new MenuItem {
                    Title = "Audit",
                    PageType = PageTypes.AuditPage},
                new MenuItem {
                    Title = "Webinars",
                    PageType = PageTypes.WebinarsPage },
                new MenuItem {
                    Title = "Training",
                    PageType = PageTypes.TrainingPage },
                new MenuItem {
                    Title = "About",
                    PageType = PageTypes.AboutPage },
            };
        }
    }
}