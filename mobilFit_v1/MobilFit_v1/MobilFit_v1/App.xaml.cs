using MobilFit_v1.Models.Products1.Models;
using MobilFit_v1.Service;
using MobilFit_v1.ViewModels;
using MobilFit_v1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobilFit_v1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {
            if (profile == null)
            {
                Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();
            //var dialogService = new DialogService();

            var checkConnetion = await apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe tener conexión a internet.", "Aceptar");
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            //var token = await apiService.LoginFacebook(
            //    urlAPI,
            //    "/api",
            //    "/Customers/LoginFacebook",
            //    profile);

            //if (token == null)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error.", "Aceptar");
            //    Current.MainPage = new NavigationPage(new LoginPage());
            //    return;
            //}

            var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.Token = token;
            //mainViewModel.RegisterDevice();
            //mainViewModel.Categories = new CategoriesViewModel();
            //Current.MainPage = new MasterView();
        }
    }
}
