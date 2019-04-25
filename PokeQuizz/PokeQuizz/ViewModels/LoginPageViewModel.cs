using PokeQuizz.Models;
using PokeQuizz.Services.Manager;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PokeQuizz.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;
        MyApplicationManager appManager;

        #endregion

        #region Poroperties

        public Command LoginUserCommand => new Command(LoginUserCommandAction);
        public string Name { get; set; }
        public string Password { get; set; }


        #endregion

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Login Page";

            _dialogService = dialogService;
            _navigationService = navigationService;
            appManager = MyApplicationManager.Instance();

        }


        void LoginUserCommandAction()
        {
            User me = new User
            {
                Name = Name,
                Password = Password
            };

            if (!string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Password))
            {

                List<User> allDatabaseUsers = App.SQLiteDb.GetUsersAsync();
                
                appManager.CurrentUser = allDatabaseUsers.Find(u => u.Name == me.Name);
                if(appManager.CurrentUser != null)
                {
                    _navigationService.NavigateAsync("UserPage");
                }
            }


        }
    }
}
