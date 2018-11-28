using MobilFit_v1.Service;
using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

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
            Email = "user@gmail.com";
            Password = "123";
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
            this.isDebug = false;
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
            this.IsRunning = true;
            this.IsEnabled = false;

            if (!isDebug)
            {
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión.", "Aceptar");
                    return;
                }

                var response = await this.apiService.GetLogin<int>(ValuesService.url, "api/", "Login/", "?email="+ Email+ "&password=" + Password);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    return;
                }

                int id_usuario = (int)response.Result;

                if (id_usuario == 0)
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
                mainViewModel.Usuario.Id_usuario = id_usuario;
                mainViewModel.TrainingPlan = new TrainingPlanViewModel();
                Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());

                this.IsRunning = false;
                this.IsEnabled = true;

                this.Email = string.Empty;
                this.password = string.Empty;
            }
            else
            {
                MainViewModel.GetInstance().TrainingPlan = new TrainingPlanViewModel();
                Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
            }
        }
        private async void Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
        #endregion
    }
}
