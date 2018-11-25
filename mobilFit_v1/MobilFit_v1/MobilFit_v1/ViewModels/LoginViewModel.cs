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
            IsEnabled = true;
            IsRemembered = true;

            this.Email = "a";
            this.Password = "a";    
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
            bool isCorrect = false;
            isDebug = true;
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

            try
            {
                if (!isDebug)
                {
                    LoginService loginService = new LoginService();
                    isCorrect = loginService.Acceso(this.Email, this.Password);
                    if (isCorrect)
                    {
                        MainViewModel.GetInstance().TrainingPlan = new TrainingPlanViewModel();
                        Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
                        App.Current.Properties["isLogged"] = true;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atención", "Usuario o contraseña incorrectos, intente nuevamente.", "Aceptar");
                    }
                }
                else
                {
                    MainViewModel.GetInstance().TrainingPlan = new TrainingPlanViewModel();
                    Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
                }
                this.IsRunning = false;
                this.IsEnabled = true;
                this.Email = string.Empty;
                this.password = string.Empty; 
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "No se puede conectar con el servidor.", "Aceptar");
            }

        }
        private async void Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
        #endregion
    }
}
