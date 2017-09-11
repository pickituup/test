using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Models;

namespace tpm.DependencyServices {
    public interface ISqlLiteService {

        /// <summary>
        /// 
        /// </summary>
        void AddUser(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        User GetFirstOrDefaultUser();

        /// <summary>
        /// 
        /// </summary>
        void DeleteAllUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        void UpdateQuestion(Question question, int userId);
    }
}
