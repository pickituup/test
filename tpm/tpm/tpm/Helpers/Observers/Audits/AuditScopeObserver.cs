using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers.Observers.Args;
using tpm.Models;

namespace tpm.Helpers.Observers.Audits {
    public sealed class AuditScopeObserver {

        public event EventHandler StartAssesment = delegate { };
        public event EventHandler AuditOneStepBack = delegate { };
        public event EventHandler<UserEventArgs> BeginAnsweringTheQuestions = delegate { };
        public event EventHandler AssessmentCompleted = delegate { };
        public event EventHandler StartOver = delegate { };
        public event EventHandler PreparePublishing = delegate { };
        public event EventHandler GoHome = delegate { };
        public event EventHandler<PublishEventArgs> Publish = delegate { };

        /// <summary>
        /// Invoked by StartAssesmentCommand from apropriate audit related page.
        /// </summary>
        public void OnStartAssesment() {
            StartAssesment(this, new EventArgs());
        }

        /// <summary>
        /// Informs about one step back in Audit page navigation scope
        /// </summary>
        public void OnAuditOneStepBack() {
            AuditOneStepBack(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnBeginAnsweringTheQuestions(User user) {
            BeginAnsweringTheQuestions(this, new UserEventArgs(user));
        }

        /// <summary
        /// 
        /// </summary>
        public void OnAssessmentCompleted() {
            AssessmentCompleted(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnStartOver() {
            StartOver(this, new EventArgs());
        }

        /// <summary>
        /// It's not a sending the email just for displaing appropriate view
        /// </summary>
        public void OnPreparePublishing() {
            PreparePublishing(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnGoHome() {
            GoHome(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnPublish(string email) {
            Publish(this, new PublishEventArgs(email));
        }
    }
}
