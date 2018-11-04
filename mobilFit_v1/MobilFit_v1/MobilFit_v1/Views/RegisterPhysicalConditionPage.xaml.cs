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
    public partial class RegisterPhysicalConditionPage : ContentPage
    {
        public RegisterPhysicalConditionPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPhysicalConditionViewModel();
        }

        void BtnContunarRegistro_Clicked(object sender, EventArgs e)
        {
            Objetivo objetivo = cboObjetivo.SelectedItem as Objetivo;
            TipoCuerpo tipoCuerpo = cboTipoCuerpo.SelectedItem as TipoCuerpo;
            //decimal peso = txtEmail.Text;
            //decimal altura = txtContraseña1.Text;
            //int nivel = txtContraseña2.Text;
            //int contrainidcacion = 0;
            //if (string.IsNullOrEmpty(objetivo) || string.IsNullOrEmpty(tipoCuerpo) || string.IsNullOrEmpty(email) ||
            //    string.IsNullOrEmpty(contraseña1) || string.IsNullOrEmpty(contraseña1))
            //{
            //    DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
            //    this.Focus();
            //    return;
            //}
            //if (contraseña1 != contraseña2)
            //{
            //    DisplayAlert("Atención", "Las contraseñas no coinciden.", "Aceptar");
            //    this.Focus();
            //    return;
            //}
            //UserRegisterViewModel.objUsuario = new Models.Usuario();
            //UserRegisterViewModel.objUsuario.nombre = objetivo;
            //UserRegisterViewModel.objUsuario.id_tipoCuerpo = tipoCuerpo;
            //UserRegisterViewModel.objUsuario.email = email;
            //UserRegisterViewModel.objUsuario.contraseña = contraseña1;
            //UserRegisterViewModel.objUsuario.fechaRegistro = DateTime.Now;

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isContraindicacion = chkContraindicacion.IsToggled;
            if (isContraindicacion)
            {
                cboContraindicacion.IsEnabled = true;
            }
            else
            {
                cboContraindicacion.IsEnabled = false;
                cboContraindicacion.SelectedItem = string.Empty;
            }
        }
    }
}