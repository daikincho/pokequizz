using PokeQuizz.Models;
using PokeQuizz.Services.Manager;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PokeQuizz.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PlayPageViewModel : ViewModelBase
    {

        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;
        MyApplicationManager appManager;


        #endregion

        #region Poroperties

        public Command NextCommand => new Command(NextCommandAction);
        // public string Email { get; set; }
        // public string Name { get; set; }
        public int QuestionNumber { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }
        public Question CurrentQuestion { get; set; }
        public List<Question> AllQuestions = new List<Question>();
        public ObservableCollection<Answer> AllAnswers { get; set; }
        public Answer AnswerSelected { get; set; }
        List<int> numbListQuestion = new List<int>();
        List<int> numbListAnswers = new List<int>();
        int TotalQuestions = 10;
        Random rnd = new Random();
        ObservableCollection<Answer> Answers = new ObservableCollection<Answer>();






        #endregion

        public PlayPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Login Page";

            _dialogService = dialogService;
            _navigationService = navigationService;
            appManager = MyApplicationManager.Instance();
            loopQuestions();

        }

        void loopQuestions()
        {
            QuestionNumber = 1;
            

            var list = App.SQLiteDb.GetItemsAsync();
            int count = list.Count < TotalQuestions ? list.Count : TotalQuestions;
            int aleaNumb;
            


            bool newAleaNumbIsSaved = false;

            int counter = 0;
            do
            {
                
                aleaNumb = rnd.Next(1, list.Count + 1); //+1 because max is a exclusive value
                if (!numbListQuestion.Contains(aleaNumb))
                {
                    numbListQuestion.Add(aleaNumb);
                    counter++;
                    newAleaNumbIsSaved = true;
                }



            } while (counter < count && numbListQuestion.Count < TotalQuestions);

            foreach (var n in numbListQuestion)
                AllQuestions.Add(list[n-1]);


            CurrentQuestion = AllQuestions[0];

            ShuffleAnswers(CurrentQuestion);


        }



        void ShuffleAnswers(Question question)
        {
            numbListAnswers.Clear();
            int count = 3;
            int aleaNumb;

            int counter = 0;
            do
            {

                aleaNumb = rnd.Next(1, count + 1); //+1 because max is a exclusive value
                if (!numbListAnswers.Contains(aleaNumb))
                {
                    numbListAnswers.Add(aleaNumb);
                    counter++;
                }



            } while (counter < count && numbListAnswers.Count < TotalQuestions);

            AllAnswers = new ObservableCollection<Answer>();
            foreach (var n in numbListAnswers)
                AllAnswers.Add(question.Answers[n - 1]);

        }

        async void NextCommandAction()
        {
            if(AnswerSelected != null)
            {
                QuestionNumber++;
                if((QuestionNumber - 1) < AllQuestions.Count)
                {
                    CurrentQuestion = AllQuestions[QuestionNumber - 1]; //because QuestionNumber begin at 1 not 0
                    ShuffleAnswers(CurrentQuestion);
                }

                Score = AnswerSelected.IsCorrect == true ? Score + 1  : Score; //increase or not the score




            }

            if (QuestionNumber > AllQuestions.Count)
            {
                //partie fini
                User user = appManager.CurrentUser;
                user.BestScore = user.BestScore < Score ? Score : user.BestScore;

                App.SQLiteDb.SaveItemAsync(user, false); //update user bestscore in database
                await _dialogService.DisplayAlertAsync("Error", "Merci d'avoir participé, votre score est : " + Score, "OK");
                await _navigationService.NavigateAsync("UserPage");

                

            }
        }

    }
}
