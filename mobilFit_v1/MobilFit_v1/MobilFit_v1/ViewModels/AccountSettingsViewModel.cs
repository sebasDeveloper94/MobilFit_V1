using MobilFit_v1.Models;
using MobilFit_v1.Service;
using MobilFit_v1.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace MobilFit_v1.ViewModels
{
    class AccountSettingsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private string nombre;
        private string apellido;
        private int sexo;
        private string email;
        private string contraseña;
        private decimal peso;
        private decimal altura;
        private int tipoCuerpo;
        private int nivel;
        private bool isEnabled;
        Usuario objUsuario;
        #endregion

        #region Properties
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
        public int Sexo
        {
            get { return this.sexo; }
            set { SetValue(ref this.sexo, value); }
        }
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }
        public string Contraseña
        {
            get { return this.contraseña; }
            set { SetValue(ref this.contraseña, value); }
        }
        public decimal Peso
        {
            get { return this.peso; }
            set { SetValue(ref this.peso, value); }
        }
        public decimal Altura
        {
            get { return this.altura; }
            set { SetValue(ref this.altura, value); }
        }
        public int TipoCuerpo
        {
            get { return this.tipoCuerpo; }
            set { SetValue(ref this.tipoCuerpo, value); }
        }
        public int Nivel
        {
            get { return this.nivel; }
            set { SetValue(ref this.nivel, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructor
        public AccountSettingsViewModel()
        {
            this.ChargeData();
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveChanges);
            }
        }
        #endregion

        #region Methods
        private void SaveChanges()
        {
            throw new NotImplementedException();
        }

        private void ChargeData()
        {
            objUsuario = new Usuario();
            objUsuario = MainViewModel.GetInstance().Usuario;

            Nombre = objUsuario.Nombre;
            Apellido = objUsuario.Apellido_paterno;
            Email = objUsuario.Email;
            Contraseña = objUsuario.Password;
            Peso = objUsuario.Peso;
            Altura = objUsuario.Altura;
        }
        #endregion
    }
}
