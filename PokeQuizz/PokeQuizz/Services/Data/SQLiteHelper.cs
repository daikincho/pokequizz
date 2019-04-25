using PokeQuizz.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeQuizz.Services.Data
{
    public class SQLiteHelper
    {
        SQLiteConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Question>();
            db.CreateTable<Answer>();
            db.CreateTable<User>();
        }


        public List<Question> GetItemsAsync()
        {
            //return db.Table<Question>().ToListAsync();
            return ReadOperations.GetAllWithChildren<Question>(db);
            
        }

        public void SaveItemAsync(Question question)
        {
            if (question.QuestionID != 0)
                WriteOperations.UpdateWithChildren(db, question); //return db.UpdateAsync(question);
            else
                WriteOperations.InsertWithChildren(db,question);//return db.InsertAsync(question);
        }

        public void SaveItemAsync(Answer answer)
        {
            if (answer.AnswerID != 0)
                WriteOperations.UpdateWithChildren(db, answer); //return db.UpdateAsync(answer);
            else
                WriteOperations.InsertWithChildren(db, answer);  //return db.InsertAsync(answer);
        }

        public void DeleteItemAsync(Question question)
        {
            if (question.QuestionID != 0)
                WriteOperations.Delete(db, question, true); //return db.UpdateAsync(question);
            
        }


        //user
        public void SaveItemAsync(User user)
        {
            if (user != null)
                WriteOperations.InsertWithChildren(db, user);
        }

        public List<User> GetUsersAsync()
        {
            //return db.Table<Question>().ToListAsync();
            return ReadOperations.GetAllWithChildren<User>(db);

        }


    }
}
