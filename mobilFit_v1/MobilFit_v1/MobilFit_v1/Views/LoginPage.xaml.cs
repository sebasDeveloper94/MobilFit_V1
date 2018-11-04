using MobilFit_v1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilFit_v1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool isBusy { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            btnAcceder.Clicked += BtnAcceder_Clicked;
        }

        private async void BtnAcceder_Clicked(object sender, EventArgs e)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            string usuario = txtEmail.Text;
            string contraseña = txtContraseña.Text;
            bool correcto = false;
            if (string.IsNullOrEmpty(usuario))
            {
                await DisplayAlert("Atención", "Debe indicar un email.", "Aceptar");
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(contraseña))
            {
                await DisplayAlert("Atención", "Debe indicar una contraseña.", "Aceptar");
                txtContraseña.Focus();
                return;
            }
            this.progreso.IsRunning = true;
            try
            {
 
                btnAcceder.IsEnabled = false;
                correcto = loginViewModel.Login(usuario, contraseña);

            }
            catch (Exception)
            {
                await DisplayAlert("Atención", "No hay conexión a internet.", "Aceptar");
                this.progreso.IsRunning = false;
            }
            this.progreso.IsRunning = false;
            if (correcto)
            {
                Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
                App.Current.Properties["isLogged"] = true;
            }
            else
            {
                await DisplayAlert("Atención", "Usuario o contraseña incorrectos, intente nuevamente.", "Aceptar");
            }
            btnAcceder.IsEnabled = true;
        }

        async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPersonalDataPage());
        }
    }
}