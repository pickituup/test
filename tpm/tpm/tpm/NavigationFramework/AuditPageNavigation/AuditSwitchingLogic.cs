using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using tpm.Views.CompoundedViews;
using tpm.Views.Pages;

namespace tpm.NavigationFramework {

    /// <summary>
    /// 
    /// </summary>
    public sealed class AuditSwitchingLogic {

        private readonly AuditViewsContainer _auditViewsContainer;

        /// <summary>
        /// Public ctor
        /// </summary>
        public AuditSwitchingLogic() {
            _auditViewsContainer = new AuditViewsContainer();
        }

        /// <summary>
        /// Get page of the appropriate type
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public IAuditView GetViewByType(AuditViewTypes auditViewType) {
            return _auditViewsContainer.GetViewByType(auditViewType);
        }
    }
}