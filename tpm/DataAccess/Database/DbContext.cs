using DataAccess.Database.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database {

    /// <summary>
    /// TODO: make db context more robust...
    /// </summary>
    public sealed class DbContext {

        private readonly string _dbPath;

        /// <summary>
        /// Public ctor.
        /// </summary>
        /// <param name="dbPath"></param>
        public DbContext(string dbPath) {
            _dbPath = dbPath;

            using (var db = new SQLiteConnection(_dbPath)) {
                db.CreateTable<DbModelUser>();
                db.CreateTable<DbModelQuestion>();
            }
        }

        /// <summary>
        /// Adds user to the db
        /// </summary>
        /// <param name="user"></param>
        public int AddUser(DbModelUser user) {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelUser>();

                    int index = db.Insert(user,typeof(DbModelUser));

                    return index;
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns first or default user from DB (can be null).
        /// </summary>
        /// <returns></returns>
        public DbModelUser GetFirstOrDefaultUser() {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelUser>();

                    DbModelUser dbModelUser = db.Table<DbModelUser>()
                        .FirstOrDefault<DbModelUser>();

                    return dbModelUser;
                }
                catch (Exception) {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete all users from data and all their appropriate data
        /// </summary>
        public void DeleteAllUsers() {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelUser>();
                    //db.CreateTable<DbModelQuestion>();

                    db.DeleteAll<DbModelUser>();
                    db.DeleteAll<DbModelQuestion>();
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbModelQuestion"></param>
        /// <returns></returns>
        public int AddQuestion(DbModelQuestion dbModelQuestion) {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelQuestion>();

                    int index = db.Insert(dbModelQuestion);

                    return index;
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbModelQuestion"></param>
        public void UpdateQuestion(DbModelQuestion dbModelQuestion) {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelQuestion>();

                    db.Update(dbModelQuestion);
                }
                catch (Exception) {
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<DbModelQuestion> GetAllUserQuestions(DbModelUser user) {
            using (var db = new SQLiteConnection(_dbPath)) {
                try {
                    //db.CreateTable<DbModelQuestion>();

                    return db.Table<DbModelQuestion>()
                        .Where<DbModelQuestion>((q) => q.UserId == user.Id)
                        .ToList<DbModelQuestion>();
                }
                catch (Exception) {
                    throw;
                }
            }
        }
    }
}
