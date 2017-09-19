using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Models {

    /// <summary>
    /// MemberSerialization.OptIn - serialized/deserialized only 'atributted parts'
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Question {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public Question() {
            Comments = new List<AttachedComment>();
            Images = new List<AttachedImage>();
            Id = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string QuestionMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string IfNoAnswer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public Nullable<bool> Answer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public List<AttachedComment> Comments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public List<AttachedImage> Images { get; private set; }
    }
}
