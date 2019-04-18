using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.Models
{
    public class Answer
    {

        [PrimaryKey, AutoIncrement]
        public int AnswerID { get; set; }

        public string Description { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey(typeof(Question))]     // Specify the foreign key
        public int QuestionFK { get; set; }
        public DateTime Time { get; set; }


        [ManyToOne]      // Many to one relationship with Question
        public Question Question { get; set; }




    }
}
