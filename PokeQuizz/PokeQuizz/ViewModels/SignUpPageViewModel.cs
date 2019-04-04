using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeQuizz.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        #region Fields

        IPageDialogService _dialogService;
        INavigationService _navigationService;

        #endregion

        #region Poroperties

        public DelegateCommand SubmitCommand { get; private set; }



        #endregion


        public SignUpPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Login Page";

        }
    }
}
