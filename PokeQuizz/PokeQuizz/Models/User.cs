using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.Models
{
    [AddINotifyPropertyChangedInterface]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int BestScore { get; set; }
    }
}
