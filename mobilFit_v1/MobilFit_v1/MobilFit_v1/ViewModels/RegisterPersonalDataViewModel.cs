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
    public class RegisterPersonalDataViewModel
    {
        #region propidades
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string contraseña1 { get; set; }
        public string contraseña2 { get; set; }
        Usuario objUsuario;
        #endregion
        #region Contructor
        public RegisterPersonalDataViewModel()
        {
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
            //if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(email) ||
            //    string.IsNullOrEmpty(contraseña1) || string.IsNullOrEmpty(contraseña1))
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
            //    return;
            //}
            //if (contraseña1 != contraseña2)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atención", "Las contraseñas no coinciden.", "Aceptar");
            //    return;
            //}

            objUsuario.Nombre = nombre;
            objUsuario.Apellido_paterno = apellido;
            objUsuario.Apellido_materno = string.Empty;
            objUsuario.Email = email;
            objUsuario.Password = contraseña1;
            SendNewUsuario(objUsuario);
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPhysicalConditionPage());
        }
        public void SendNewUsuario(Usuario objUsuario)
        {
            RegisterPhysicalConditionViewModel rp = new RegisterPhysicalConditionViewModel();
            rp.ReceiveNewUser(objUsuario);
        }
        #endregion
    }
}
