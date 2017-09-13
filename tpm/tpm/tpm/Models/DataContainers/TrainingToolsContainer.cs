using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tpm.Models.DataContainers.DataItems;
using Xamarin.Forms;

namespace tpm.Models.DataContainers {
    public sealed class TrainingToolsContainer {

        /*
         return new List<TrainingToolsItem>() {
                new TrainingToolsItem() {
                    Index = 1,
                    Header = "Youtube video",
                    VideoSourceLink = "http://www.youtube.com/embed/iY382tRVkSM"
                },
                new TrainingToolsItem() {
                    Index = 1,
                    Header = "Keeping worksurfaces clean and dry",
                    TextContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus suscipit, libero volutpat scelerisque faucibus, erat justo faucibus est, ac dapibus lectus augue ac libero. Sed accumsan pretium ligula. Proin eget nulla nec lacus ullamcorper vestibulum. Proin lacus ante, ultrices in leo eu, aliquam sodales massa.",
                    FirstItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_pdf.png");
                        return stream; }),
                    FirstItemTitle = "File name",
                    SeondItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_download_fill_blue_bg.png");
                        return stream; }),
                    SecondItemTitle="3mb",
                    DownloadSrc = true,
                    Src = "https://drive.google.com/uc?authuser=0&id=0B62wyfOLiDX8NVNQemdHR1JBRDg&export=download"
                },
                new TrainingToolsItem() {
                    Index = 2,
                    Header = "Toolbox Talk",
                    TextContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    FirstItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_toolkit.png");
                        return stream; }),
                    FirstItemTitle = "www.link.com/toolboxtalk",
                    Src="https://www.google.com/"
                },
                new TrainingToolsItem() {
                    Index = 3,
                    Header = "Safety Commeitte Meeting Hangout",
                    TextContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus suscipit, libero volutpat scelerisque faucibus, erat justo faucibus est, ac dapibus lectus augue ac libero.",
                    FirstItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_centralize.png");
                        return stream; }),
                    FirstItemTitle = "www.link.com/safetycommeittemeetinghangout",
                    Src="https://www.xamarin.com/"
                }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TrainingToolsItem> BuildWebinarItems() {
            return new List<TrainingToolsItem>() {
                new TrainingToolsItem() {
                    Index = 1,
                    Header = "Keeping worksurfaces clean and dry",
                    TextContent = "Walk and Work surfaces safety training",
                    FirstItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_pdf.png");
                        return stream; }),
                    FirstItemTitle = "File name",
                    SeondItemIcon = ImageSource.FromStream(()=> {
                        Assembly assembly = GetType().GetTypeInfo().Assembly;
                        Stream stream = assembly.GetManifestResourceStream("tpm.Resourses.Img.ic_download_fill_blue_bg.png");
                        return stream; }),
                    SecondItemTitle="3mb",
                    DownloadSrc = true,
                    Src = "http://ccidahra.com/wp-content/uploads/2016/03/sample.ppt"
                }
            };
        }
    }
}
