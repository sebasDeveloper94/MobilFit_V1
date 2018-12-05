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
using System.Linq;

namespace MobilFit_v1.ViewModels
{
    class AccountSettingsViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

        private string nombre;
        private string apellido;
        private string email;
        private string contraseña;
        private int edad;
        private decimal peso;
        private decimal altura;
        private bool isEnabled;
        private bool isRunning;
        private Usuario objUsuario;
        private List<Objetivo> listObjetivos;
        private List<TipoCuerpo> listTipoCuerpos;
        private List<Nivel> listNiveles;
        private List<Sexo> listsexos;
        private Objetivo objetivo;
        private TipoCuerpo tipoCuerpo;
        private Nivel nivel;
        private Sexo sexo;
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
        public int Edad
        {
            get { return this.edad; }
            set { SetValue(ref this.edad, value); }
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
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public Objetivo Objetivo
        {
            get { return this.objetivo; }
            set { SetValue(ref this.objetivo, value); }
        }
        public TipoCuerpo TipoCuerpo
        {
            get { return this.tipoCuerpo; }
            set { SetValue(ref this.tipoCuerpo, value); }
        }
        public Nivel Nivel
        {
            get { return this.nivel; }
            set { SetValue(ref this.nivel, value); }
        }
        public Sexo Sexo
        {
            get { return this.sexo; }
            set { SetValue(ref this.sexo, value); }
        }
        public List<Objetivo> ListObjetivos
        {
            get { return this.listObjetivos; }
            set { SetValue(ref this.listObjetivos, value); }
        }
        public List<TipoCuerpo> ListTipoCuerpos
        {
            get { return this.listTipoCuerpos; }
            set { SetValue(ref this.listTipoCuerpos, value); }
        }
        public List<Nivel> ListNiveles
        {
            get { return this.listNiveles; }
            set { SetValue(ref this.listNiveles, value); }
        }
        public List<Sexo> Listsexos
        {
            get { return this.listsexos; }
            set { SetValue(ref this.listsexos, value); }
        }
        #endregion

        #region Constructor
        public AccountSettingsViewModel()
        {
            apiService = new ApiService();
            this.ListObjetivos = GetObjetivos().OrderBy(x => x.key).ToList();
            this.ListTipoCuerpos = GetTipoCuerpo().OrderBy(x => x.key).ToList();
            this.ListNiveles = GetNivel().OrderBy(x => x.key).ToList();
            this.Listsexos = GetSexos().OrderBy(s => s.key).ToList();
            this.ChargeUserData();
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
        private async void SaveChanges()
        {
            var connection = await this.apiService.CheckConnection();
            IsRunning = true;
            this.IsEnabled = false;

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            int idUsuario = MainViewModel.GetInstance().Usuario.Id_usuario;
            Usuario usuario = new Usuario();
            usuario.Id_usuario = idUsuario;
            usuario.Nombre = this.Nombre;
            usuario.Apellido = this.Apellido;
            usuario.Edad = this.Edad;
            usuario.Sexo = this.Sexo.key;
            usuario.Email = this.Email;
            usuario.FechaRegistro = DateTime.Now;
            usuario.Password = this.Contraseña;
            usuario.Peso = this.Peso;
            usuario.Altura = this.Altura;
            usuario.Id_tipoCuerpo = this.TipoCuerpo.key;
            usuario.Id_nivel = this.Nivel.key;
            int objetivo = 0;
            switch (usuario.Id_objetivo)
            {
                case 1:
                    objetivo = 6;
                    break;
                case 2:
                    objetivo = 3;
                    break;
                case 3:
                    objetivo = 4;
                    break;
                default:
                    break;
            }
            usuario.Id_objetivo = objetivo;
            var json = JsonConvert.SerializeObject(usuario);

            var response = await this.apiService.GetParameter<Usuario>(ValuesService.url, "api/", "Login/", "?idUsuario=" + idUsuario + "&jsonUsuario=" + json);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error al editar usuario, por favor intente nuevamente.", "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)response.Result;

            if (oUsuario != null && oUsuario.Id_usuario == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error al editar usuario, por favor intente nuevamente.", "Aceptar");
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            MainViewModel.GetInstance().Usuario = oUsuario;
            await Application.Current.MainPage.DisplayAlert("Atención", "Los datos del usuario han sido actualizados.", "Aceptar");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private void ChargeUserData()
        {
            objUsuario = new Usuario();
            objUsuario = MainViewModel.GetInstance().Usuario;

            this.Nombre = objUsuario.Nombre;
            this.Apellido = objUsuario.Apellido;
            this.Email = objUsuario.Email;
            this.Edad = objUsuario.Edad;
            this.Contraseña = objUsuario.Password;
            this.Peso = objUsuario.Peso;
            this.Altura = objUsuario.Altura;
            this.Objetivo = this.ListObjetivos.Where(o => o.key == objUsuario.Id_objetivo).FirstOrDefault();
            this.TipoCuerpo = this.ListTipoCuerpos.Where(o => o.key == objUsuario.Id_tipoCuerpo).FirstOrDefault();
            this.Sexo = this.Listsexos.Where(o => o.key == objUsuario.Sexo).FirstOrDefault();
            this.Nivel = this.ListNiveles.Where(o => o.key == objUsuario.Id_nivel).FirstOrDefault();
        }

        public List<Objetivo> GetObjetivos()
        {
            List<Objetivo> objetivos = new List<Objetivo>()
            {
                new Objetivo() {key = 1, value="Ganar mas fuerza" },
                new Objetivo() {key = 2, value="Bajar de peso" },
                new Objetivo() {key = 3, value="Tonificar" },
                new Objetivo() {key = 4, value="Ganar masa muscular" },
                new Objetivo() {key = 5, value="Mantenerme" },
                new Objetivo() {key = 6, value="Buena salud" }
            };
            return objetivos;
        }
        public List<TipoCuerpo> GetTipoCuerpo()
        {
            List<TipoCuerpo> tipoCuerpos = new List<TipoCuerpo>()
            {
                new TipoCuerpo() {key = 1, value="Endomorfo (Contextura gruesa)" },
                new TipoCuerpo() {key = 2, value="Mesomorfo (Contextura atlética)" },
                new TipoCuerpo() {key = 3, value="Ectomorfo (Contextura delgada)" }
            };
            return tipoCuerpos;
        }
        public List<Nivel> GetNivel()
        {
            List<Nivel> niveles = new List<Nivel>()
            {
                new Nivel() {key = 1, value="Principiante" },
                new Nivel() {key = 2, value="Intermedio" },
                new Nivel() {key = 3, value="Avanzado" }
            };
            return niveles;
        }
        public List<Sexo> GetSexos()
        {
            List<Sexo> sexos = new List<Sexo>()
            {
                new Sexo() {key = 1, value="Hombre" },
                new Sexo() {key = 0, value="Mujer" }
            };
            return sexos;
        }

        #endregion
    }
}
