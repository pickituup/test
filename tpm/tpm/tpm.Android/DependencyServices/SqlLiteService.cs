using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using tpm.DependencyServices;
using DataAccess.Database;
using DataAccess.Database.Tables;
using tpm.Models;
using Newtonsoft.Json;
using System.IO;

[assembly: Dependency(typeof(tpm.Droid.DependencyServices.SqlLiteService))]
namespace tpm.Droid.DependencyServices {

    /// <summary>
    /// TODO: make more robust andy try to remove DRY when Serializing/Deserializing
    /// </summary>
    public class SqlLiteService : ISqlLiteService {

        private readonly string _dpPath = "tpm.db";
        private DbContext _dbContext;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public SqlLiteService() {
            _dbContext =
                new DbContext(
                    Path.Combine(
                        System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _dpPath)
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user) {
            DbModelUser dbModelUser = SerializeUser(user);
            _dbContext.AddUser(dbModelUser);
            user.Id = dbModelUser.Id;

            foreach (Question question in user.Questions) {
                DbModelQuestion dbModelQuestion = SerializeQuestion(question);
                dbModelQuestion.UserId = user.Id;

                _dbContext.AddQuestion(dbModelQuestion);
                question.Id = dbModelQuestion.Id;
            }
        }

        /// <summary>
        /// Deletes all users from DB and their all appropriate data
        /// </summary>
        public void DeleteAllUsers() {
            _dbContext.DeleteAllUsers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User GetFirstOrDefaultUser() {
            DbModelUser dbModelUser = _dbContext.GetFirstOrDefaultUser();

            if (dbModelUser == null) {
                return null;
            }

            User user = DeserializeUser(dbModelUser);

            IEnumerable<DbModelQuestion> dbModelQuestions = 
                _dbContext.GetAllUserQuestions(dbModelUser);

            foreach (DbModelQuestion dbModelQuestion in dbModelQuestions) {
                user.Questions.Add(DeserializeQuestion(dbModelQuestion));
            }

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        public void UpdateQuestion(Question question, int userId) {
            DbModelQuestion dbModelQuestion = SerializeQuestion(question);
            dbModelQuestion.Id = question.Id;
            dbModelQuestion.UserId = userId;

            _dbContext.UpdateQuestion(dbModelQuestion);
        }

        /// <summary>
        /// Helper method defines DBModel.User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private DbModelUser SerializeUser(User user) {
            DbModelUser dbModelUser = new DbModelUser() {
                Id = user.Id,
                UserJsonObject = JsonConvert.SerializeObject(user)
            };

            return dbModelUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private DbModelQuestion SerializeQuestion(Question question) {
            DbModelQuestion dbModelQuestion = new DbModelQuestion() {
                Id = question.Id,
                QuestionJsonObject = JsonConvert.SerializeObject(question)
            };

            return dbModelQuestion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbModelUser"></param>
        private User DeserializeUser(DbModelUser dbModelUser) {
            User user = JsonConvert.DeserializeObject<User>(dbModelUser.UserJsonObject);
            user.Id = dbModelUser.Id;

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbModelUser"></param>
        private Question DeserializeQuestion(DbModelQuestion dbModelQuestion) {
            Question question = JsonConvert.DeserializeObject<Question>(dbModelQuestion.QuestionJsonObject);
            question.Id = dbModelQuestion.Id;

            return question;
        }
    }
}