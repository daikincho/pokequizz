using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _navigationService.NavigateAsync("SignUpPage");
        }

        void AnonymeCommandAction()
        {
            //_navigationService.NavigateAsync("SignUpPage");
        }

        void AdminCommandAction()
        {
            _navigationService.NavigateAsync("AdminPage");
        }



    }
}
