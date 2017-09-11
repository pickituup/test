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
    public class AttachedImage {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string Base64Image { get; set; }
    }
}
