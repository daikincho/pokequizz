using PokeQuizz.Models;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PokeQuizz.ViewModels
{
  

    public class AdminPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;

        #endregion

        #region Poroperties

        public Command AddOrUpdateCommand => new Command(AddOrUpdateCommandAction);
        public Command RefreshQuestionListCommand => new Command(GetAllQuestion);
        public Command DeleteQuestionCommand => new Command(DeleteQuestionCommandAction);
        public Command InitializeQuestionCommand => new Command(InitializeQuestion);


        
        private Question _question;

        public Question Question
        {
            get { return _question; }
            set {
                _question = value;
                RaisePropertyChanged();
            }
        }

        private Question _questionSelected;

        public Question QuestionSelected
        {
            get { return _questionSelected; }
            set
            {
                _questionSelected = value;
                if(value != null)
                {
                    Question = _questionSelected;
                    AnswerList = new ObservableCollection<Answer>(Question.Answers);
                }
                
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Answer> _answerList;

        public ObservableCollection<Answer> AnswerList
        {
            get { return _answerList; }
            set
            {
                _answerList = value;
                RaisePropertyChanged();

            }
        }



        private ObservableCollection<Question> _questionList;

        public ObservableCollection<Question> QuestionList
        {
            get { return _questionList; }
            set {
                _questionList = value;
                RaisePropertyChanged();

            }
        }








        #endregion


        public AdminPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Admin Page";
            _dialogService = dialogService;
            InitializeQuestion();
                

        }

        void InitializeQuestion()
        {
            Question = new Question();
            AnswerList = new ObservableCollection<Answer>();
            for (int i = 0; i < 3; i++)
            {
                AnswerList.Add(new Answer());
                if (i == 0)
                    AnswerList[i].IsCorrect = true;
            }
        }

        async void AddOrUpdateCommandAction()
        {


            if (!string.IsNullOrEmpty(Question.Description) &&
                !string.IsNullOrEmpty(AnswerList[0].Description) &&
                !string.IsNullOrEmpty(AnswerList[1].Description) &&
                !string.IsNullOrEmpty(AnswerList[1].Description))

                
            {

                //Question.QuestionID = 0;

                if (AnswerList[0].QuestionFK != Question.QuestionID)
                {
                    Question.QuestionID = AnswerList[0].QuestionFK;
                    await _dialogService.DisplayAlertAsync("Error", "You can not change the ID, please select concerned Question or create new question !", "OK");

                    return;
                    
                }

                //Add New Question
                App.SQLiteDb.SaveItemAsync(Question);

                //Add all Answers of questions in Answer Table
                for (int i = 0; i < 3; i++)
                     App.SQLiteDb.SaveItemAsync(AnswerList[i]);

                //fill all answer in question --> quesion id as changed after that
                Question.Answers = AnswerList.ToList();
                

                //finally update Question
                App.SQLiteDb.SaveItemAsync(Question);



                //nooooooooooon il faut utilser SQLiteNetExtensions packet nugget
                //foreach (var answer in AnswerList)
                //    await App.SQLiteDb.SaveItemAsync(answer);


                await _dialogService.DisplayAlertAsync("Sucess", "Question saved !", "OK");


                GetAllQuestion();
                InitializeQuestion();
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Error", "Question not saved ! You forgot something", "OK");
            }
        }

        void DeleteQuestionCommandAction()
        {
            if (!string.IsNullOrEmpty(Question.Description) &&
               !string.IsNullOrEmpty(AnswerList[0].Description) &&
               !string.IsNullOrEmpty(AnswerList[1].Description) &&
               !string.IsNullOrEmpty(AnswerList[1].Description))


            {
                App.SQLiteDb.DeleteItemAsync(Question);
                InitializeQuestion();
                GetAllQuestion();

            }

            }






        void GetAllQuestion()
        {
            var list =  App.SQLiteDb.GetItemsAsync();
            QuestionList = new ObservableCollection<Question>(list);
        }
        
    }
}
