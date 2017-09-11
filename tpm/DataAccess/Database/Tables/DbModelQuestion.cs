using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database.Tables {

    [Table("Questions")]
    public sealed class DbModelQuestion {

        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string QuestionJsonObject { get; set; }
    }
}
