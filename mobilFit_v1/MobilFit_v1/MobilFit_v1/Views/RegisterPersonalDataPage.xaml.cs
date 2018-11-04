using MobilFit_v1.Models;
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
    public partial class RegisterPersonalDataPage : ContentPage
    {
        public RegisterPersonalDataPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPersonalDataViewModel();
        }
        void BtnContunarRegistro_Clicked(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string email = txtEmail.Text;
            string contraseña1 = txtContraseña1.Text;
            string contraseña2 = txtContraseña2.Text;
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(contraseña1) || string.IsNullOrEmpty(contraseña1))
            {
                DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
                this.Focus();
                return;
            }
            if (contraseña1 != contraseña2)
            {
                DisplayAlert("Atención", "Las contraseñas no coinciden.", "Aceptar");
                this.Focus();
                return;
            }

            RegisterPersonalDataViewModel rp = new RegisterPersonalDataViewModel();
            Usuario objUsuario = new Usuario();
            objUsuario.nombre = nombre;
            objUsuario.apellido_paterno = apellido;
            objUsuario.email = email;
            objUsuario.contraseña = contraseña1;
            rp.SendNewUsuario(objUsuario);
            Application.Current.MainPage.Navigation.PushAsync(new RegisterPhysicalConditionPage());
        }
    }
}