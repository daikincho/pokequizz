using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login Page";

        }
    }
}
