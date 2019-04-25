using Prism;
using Prism.Ioc;
using PokeQuizz.ViewModels;
using PokeQuizz.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PokeQuizz.Services.Data;
using System.IO;
using System;
using PokeQuizz.Models;
using PokeQuizz.Services.Manager;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PokeQuizz
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            MyApplicationManager appManager = MyApplicationManager.Instance();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<AdminPage, AdminPageViewModel>();
            containerRegistry.RegisterForNavigation<UserPage, UserPageViewModel>();


        }

        static SQLiteHelper db;
        public static SQLiteHelper SQLiteDb
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
                }
                return db;
            }
        }
    }


}
