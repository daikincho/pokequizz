using PokeQuizz.Models;
using Prism.Commands;
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
    public class SignUpPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;

        #endregion

        #region Poroperties

        public DelegateCommand SubmitCommand { get; private set; }
        public Command CreateUserCommand => new Command(CreateUserCommandAction);
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }


        #endregion


        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Login Page";
            _dialogService = dialogService;
            _navigationService = navigationService;

        }

        void CreateUserCommandAction()
        {
            User me = new User
            {
                Email = Email,
                Name = Name,
                Password = Password
            };

            if (!string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Password))
            {
                List<User> allDatabaseUsers = App.SQLiteDb.GetUsersAsync();
                if(!allDatabaseUsers.Any(u => u.Name == Name)){
                    App.SQLiteDb.SaveItemAsync(me);
                    _navigationService.NavigateAsync("LoginPage");
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Sorry, this nale already exist", "OK");
                }
                
            }


        }




    }
}
