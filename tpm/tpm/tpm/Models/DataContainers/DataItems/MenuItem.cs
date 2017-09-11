using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.NavigationFramework;

namespace tpm.Models.DataContainers.DataItems {
    public class MenuItem : ObservableObject {
        private bool _isSelected;

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected {
            get => _isSelected;
            set => SetProperty<bool>(ref _isSelected, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public PageTypes PageType { get; set; }
    }
}
