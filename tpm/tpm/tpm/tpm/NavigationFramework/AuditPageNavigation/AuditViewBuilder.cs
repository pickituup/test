using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers.Builders.ViewBuilders;

namespace tpm.NavigationFramework {
    public sealed class AuditViewBuilder<TView> : ViewBuilderBase<TView>
        where TView : IAuditView, new() {

        /// <summary>
        /// Return target view instance.
        /// </summary>
        /// <returns></returns>
        public IAuditView GetView() {
            return View;
        }
    }
}
