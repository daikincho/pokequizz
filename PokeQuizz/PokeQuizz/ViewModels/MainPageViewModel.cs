using Newtonsoft.Json;
using PokeQuizz.Models;
using PokeQuizz.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace PokeQuizz.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;

        #endregion

        #region Poroperties

        public Command LoginCommand => new Command(LoginCommandAction);
        public Command SignUpCommand => new Command(SignUpCommandAction);
        public Command AnonymeCommand => new Command(AnonymeCommandAction);
        public Command AdminCommand => new Command(AdminCommandAction);
        

        #endregion



        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        void LoginCommandAction()
        {
            //implement logic
            //_dialogService.DisplayAlertAsync("Alert", "You have been alerted", "OK");
            _navigationService.NavigateAsync("LoginPage");
        }

        void SignUpCommandAction()
        {
            try
            {
                _navigationService.NavigateAsync("SignUpPage");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        void AnonymeCommandAction()
        {
            //_navigationService.NavigateAsync("SignUpPage");
        }

        void AdminCommandAction()
        {
            

            var list = App.SQLiteDb.GetItemsAsync();
            if(list == null || list.Count == 0)
                GetJsonData();

            _navigationService.NavigateAsync("AdminPage");
        }

        void GetJsonData()
        {
            string jsonQuestions = "PokeQuizz.PokequizzStartQuestions.json";
            string jsonAnswers = "PokeQuizz.PokequizzStartAnswers.json";

            Question ObjQuestionList = new Question();
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var files = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            Stream streamQuestions = assembly.GetManifestResourceStream(jsonQuestions);
            Stream streamAnswsers = assembly.GetManifestResourceStream(jsonAnswers);
            List<Question> allQuestionsFromJson = new List<Question>();
            List<Answer> allAnswersFromJson = new List<Answer>();


            using (var reader = new System.IO.StreamReader(streamQuestions))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                var listQ = JsonConvert.DeserializeObject<List<Question>>(jsonString);
                allQuestionsFromJson = listQ; ;
            }

            using (var reader = new System.IO.StreamReader(streamAnswsers))
            {
                var jsonString = reader.ReadToEnd();

                //Converting JSON Array Objects into generic list    
                var listA = JsonConvert.DeserializeObject<List<Answer>>(jsonString);
                allAnswersFromJson = listA;
               
            }

            int answerCounter = 0;
            

            for(int i = 0;i < allQuestionsFromJson.Count; i++)
            {
                
                allQuestionsFromJson[i].Answers = new List<Answer>();
                //save question
                App.SQLiteDb.SaveItemAsync(allQuestionsFromJson[i]);

                for (int j = answerCounter ; j< answerCounter + 3; j++)
                {
                    //initialise question reference
                    allAnswersFromJson[j].QuestionFK = allQuestionsFromJson[i].QuestionID;
                    allAnswersFromJson[j].Question = allQuestionsFromJson[i];

                    allQuestionsFromJson[i].Answers.Add( allAnswersFromJson[j]);
                    //save answer
                    App.SQLiteDb.SaveItemAsync(allAnswersFromJson[j]);

                }

                //update question
                App.SQLiteDb.SaveItemAsync(allQuestionsFromJson[i]);
                answerCounter = answerCounter + 3;
            }







            //Binding listview with json string     
            //listviewConacts.ItemsSource = ObjContactList.contacts;
        }



    }
}
