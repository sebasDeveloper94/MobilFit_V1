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

namespace MobilFit_v1.ViewModels
{
    class LoginViewModel
    {
        #region Propiedades
        public string user { get; set; }
        public string password { get; set; }
        public bool isRuning { get; set; }
        public bool isEnabled { get; set; }
        public bool rememberme { get; set; }
        #endregion
        #region Constructor
        public LoginViewModel()
        {
            isEnabled = true;
            rememberme = true;
        }
        #endregion
        #region Comandos
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
        #region Metodos
        private async void Login()
        {
            bool isCorrect = false;
            //if (string.IsNullOrEmpty(this.user))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atención", "Debe indicar un email.", "Aceptar");
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.password))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atención", "Debe indicar una contraseña.", "Aceptar");
            //    return;
            //}
            this.isRuning = true;
            this.isEnabled = false;
            try
            {
                Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
                //LoginService loginService = new LoginService();
                //isCorrect = loginService.Acceso(this.user, this.password);
                //if (isCorrect)
                //{
                //    Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
                //    App.Current.Properties["isLogged"] = true;
                //}
                //else
                //{
                //    await Application.Current.MainPage.DisplayAlert("Atención", "Usuario o contraseña incorrectos, intente nuevamente.", "Aceptar");
                //}
                this.isRuning = false;
                this.isEnabled = true;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión a internet.", "Aceptar");
            }

        }
        private async void Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
        #endregion
    }
}
