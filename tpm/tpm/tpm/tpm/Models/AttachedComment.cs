using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Models {

    /// <summary>
    /// MemberSerialization.OptIn - serialized/deserialized only 'atributted parts'
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AttachedComment {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string CommentMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public DateTime PublishDate { get; set; }
    }
}