using PokeQuizz.Models;
using PokeQuizz.Services.Interfaces;
using Prism.Commands;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
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
    public class SignUpPageViewModel : ViewModelBase
    {
        #region Fields
        IContacts _contactService;
        IPageDialogService _dialogService;
        INavigationService _navigationService;
        IContainerRegistry _containerRegistry;

        #endregion

        #region Poroperties

        public DelegateCommand SubmitCommand { get; private set; }
        public Command CreateUserCommand => new Command(CreateUserCommandAction);
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ObservableCollection<Contact> ListContact { get; set; }

        private Contact _contactSelected;

        public Contact ContactSelected
        {
            get { return _contactSelected; }
            set
            {
                _contactSelected = value;
                if (value != null)
                {
                    Name = _contactSelected.DisplayName;

                }

                RaisePropertyChanged();
            }
        }


        #endregion


        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IContacts contactService)
            : base(navigationService)
        {
            Title = "Login Page";
            _dialogService = dialogService;
            _navigationService = navigationService;
            _contactService = contactService;
            OpenContacts();

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
                    App.SQLiteDb.SaveItemAsync(me, true);
                    _navigationService.NavigateAsync("LoginPage");
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Sorry, this nale already exist", "OK");
                }
                
            }


        }

        async void OpenContacts()
        {
            var contactList = await _contactService.GetDeviceContactsAsync();
            ListContact = new ObservableCollection<Contact>(contactList);


        }




    }
}
