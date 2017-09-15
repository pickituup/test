using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Pdf;

namespace tpm.Models {

    /// <summary>
    /// MemberSerialization.OptIn - serialized/deserialized only 'atributted parts'
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class User : IPdfCanBeDrawed {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public User() {
            Questions = new List<Question>();
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
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string CompanyCityStateZip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string CompanySpecificLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Question> Questions { get; private set; }
    }
}