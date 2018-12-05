using MobilFit_v1.Service;
using MobilFit_v1.Models;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;

namespace MobilFit_v1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ApiService apiService;
        #region Attributes
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isRunning = false;
        private bool isEnabled = false;
        #endregion

        #region properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool isDebug = false;
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            IsEnabled = true;
            IsRemembered = true;
            //Email = "user@gmail.com";
            //Password = "123";
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }
        #endregion

        #region Methods
        private async void Login()
        {

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe indicar un email.", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe indicar una contraseña.", "Aceptar");
                return;
            }

            if (!ValidateEmail.Validate(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Dirección de email invalida.", "Aceptar");
                this.Email = string.Empty;
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión.", "Aceptar");
                return;
            }

            var response = await this.apiService.GetParameter<Usuario>(ValuesService.url, "api/", "Login/", "?email=" + Email + "&password=" + Password);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            Usuario usuario = new Usuario();
            usuario = (Usuario)response.Result;

            if (usuario == null || usuario.Id_usuario == 0)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Atención", "Email o contraseña incorrecta, por favor intente nuevamente.", "Aceptar");
                this.Password = string.Empty;
                this.Email = string.Empty;
                return;
            }
            
            MainViewModel mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Usuario = new Usuario();
            mainViewModel.Usuario = usuario;
            mainViewModel.Settings = new SettingsViewModel();
            mainViewModel.TrainingPlan = new TrainingPlanViewModel();
            App.Current.Properties["IsLoggedIn"] = true;
            Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.password = string.Empty;
        }
        private async void Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
        #endregion
    }
}
