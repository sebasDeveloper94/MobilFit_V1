using MobilFit_v1.Models;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using MobilFit_v1.Service;

namespace MobilFit_v1.ViewModels
{

    public class RegisterPersonalDataViewModel : BaseViewModel
    {
        private ApiService apiService;

        #region attributes
        private string nombre;
        private string apellido;
        private string email;
        private string contraseña1;
        private string contraseña2;
        private bool isRunning;
        public bool isEnabled;
        private string loadText;
        Usuario objUsuario;
        #endregion

        #region propidades
        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
        public string Apellido
        {
            get { return this.apellido; }
            set { SetValue(ref this.apellido, value); }
        }
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Contraseña1
        {
            get { return this.contraseña1; }
            set { SetValue(ref this.contraseña1, value); }
        }
        public string Contraseña2
        {
            get { return this.contraseña2; }
            set { SetValue(ref this.contraseña2, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public string LoadText
        {
            get { return this.loadText; }
            set { SetValue(ref this.loadText, value); }
        }
        #endregion

        #region Contructor
        public RegisterPersonalDataViewModel()
        {
            apiService = new ApiService();
            objUsuario = new Usuario();
        }
        #endregion

        #region Comandos
        public ICommand FirstRegisterCmd
        {
            get
            {
                return new RelayCommand(FirstRegister);
            }
        }
        #endregion

        #region Metodos
        private async void FirstRegister()
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(contraseña1) || string.IsNullOrEmpty(contraseña1))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
                return;
            }
            if (contraseña1 != contraseña2)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Las contraseñas no coinciden.", "Aceptar");
                return;
            }

            if (!ValidateEmail.Validate(this.email))
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Dirección de email invalida.", "Aceptar");
                this.Email = string.Empty;
                return;
            }
            IsRunning = true;
            IsEnabled = false;

            apiService = new ApiService();
            this.LoadText = "Validando información, porfavor espere.";
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión.", "Aceptar");
                IsRunning = false;
                IsEnabled = true;
                this.LoadText = string.Empty;
                return;
            }

            var response = await this.apiService.GetParameter<int>(ValuesService.url, "api/", "Login/", "?email=" + Email);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRunning = false;
                IsEnabled = true;
                this.LoadText = string.Empty;
                return;
            }

            int id_usuario = (int)response.Result;

            if (id_usuario > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ya existe un usuario con esta dirección de email, porfavor intenta con una diferente.", "Aceptar");
                IsRunning = false;
                IsEnabled = true;
                this.Email = string.Empty;
                this.LoadText = string.Empty;
                return;
            }

            objUsuario.Nombre = nombre;
            objUsuario.Apellido_paterno = apellido;
            objUsuario.Apellido_materno = string.Empty;
            objUsuario.Email = email;
            objUsuario.Password = contraseña1;
            SendNewUsuario(objUsuario);
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPhysicalConditionPage());
            IsRunning = false;
            IsEnabled = true;
            this.LoadText = string.Empty;
        }
        public void SendNewUsuario(Usuario objUsuario)
        {
            RegisterPhysicalConditionViewModel rp = new RegisterPhysicalConditionViewModel();
            rp.ReceiveNewUser(objUsuario);
        }
        #endregion
    }
}
