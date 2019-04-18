using PropertyChanged;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Question
    {

        [PrimaryKey, AutoIncrement]
        public int QuestionID { get; set; }

        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Answers
        public List<Answer> Answers { get; set; }






    }
}
