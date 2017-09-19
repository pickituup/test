using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Views.CompoundedViews;

namespace tpm.NavigationFramework {
    public sealed class AuditViewsContainer {
        private static readonly string _ERROR_CANT_GET_VIEW_BY_TYPE = "Can't get view by it's type";

        /// <summary>
        /// Container for views
        /// </summary>
        private readonly Dictionary<AuditViewTypes, Func<IAuditView>> _views;

        /// <summary>
        /// Public ctor
        /// </summary>
        public AuditViewsContainer() {
            _views = BuildViews();
        }

        /// <summary>
        /// Get view by type
        /// </summary>
        /// <param name="viewType"></param>
        /// <returns></returns>
        public IAuditView GetViewByType(AuditViewTypes viewType) {
            try {
                return _views[viewType]();
            }
            catch (Exception) {
                throw new InvalidOperationException(string.Format("AuditViewsContainer.GetViewByType - {0}", _ERROR_CANT_GET_VIEW_BY_TYPE));
            }
        }

        /// <summary>
        /// Build views
        /// </summary>
        /// <returns></returns>
        private Dictionary<AuditViewTypes, Func<IAuditView>> BuildViews() {
            return new Dictionary<AuditViewTypes, Func<IAuditView>>() {
                {
                    AuditViewTypes.AssesmentView,
                    () => new AuditViewBuilder<AssesmentView>().GetView()
                },
                {
                    AuditViewTypes.EnterInfoView,
                    () => new AuditViewBuilder<EnterInfoView>().GetView()
                },
                {
                    AuditViewTypes.QuestionsListView,
                    () => new AuditViewBuilder<QuestionsListView>().GetView()
                },
                {
                    AuditViewTypes.ReviewView,
                    () => new AuditViewBuilder<ReviewView>().GetView()
                },
                {
                    AuditViewTypes.PublishView,
                    () => new AuditViewBuilder<PublishView>().GetView()
                }
            };
        }
    }
}
