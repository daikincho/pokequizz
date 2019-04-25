using PokeQuizz.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.Services.Manager
{
    public class MyApplicationManager
    {
        private static MyApplicationManager _instance = null;

        static internal MyApplicationManager Instance()
        {
            if (_instance == null)
            {
                _instance = new MyApplicationManager();
            }

            return _instance;
        }

        private MyApplicationManager()
        {
        }

        public User CurrentUser { get; set; }
    }
}
