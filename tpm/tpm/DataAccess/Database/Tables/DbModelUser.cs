using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database.Tables {
    [Table("Users")]
    public sealed class DbModelUser {

        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserJsonObject { get; set; }

        /*
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyCityStateZip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanySpecificLocation { get; set; }
        */
    }
}
