using PokeQuizz.Services.Manager;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PokeQuizz.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class UserPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;
        MyApplicationManager appManager;


        #endregion

        #region Poroperties

          public Command PlayCommand => new Command(PlayCommandAction);
       // public string Email { get; set; }
       // public string Name { get; set; }
          public string UserName { get; set; }
          public int BestScore { get; set; }


        #endregion

        public UserPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            
            _dialogService = dialogService;
            _navigationService = navigationService;
            appManager = MyApplicationManager.Instance();
            Title = appManager.CurrentUser != null ? "Welcome " + appManager.CurrentUser.Name : "Welcome my friend ! ";
            UserName = appManager.CurrentUser.Name;
            BestScore = appManager.CurrentUser.BestScore;
        }



        void PlayCommandAction()
        {
            _navigationService.NavigateAsync("PlayPage");
        }

    }
}
