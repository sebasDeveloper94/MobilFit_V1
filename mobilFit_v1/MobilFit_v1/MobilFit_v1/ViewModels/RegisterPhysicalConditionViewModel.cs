using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MobilFit_v1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using MobilFit_v1.Service;
using Newtonsoft.Json;

namespace MobilFit_v1.ViewModels
{
    public class RegisterPhysicalConditionViewModel : BaseViewModel
    { 
        private ApiService apiService;

        #region Attributes
        private Objetivo objetivo;
        private TipoCuerpo tipoCuerpo;
        private Nivel nivel;
        private Sexo sexo;
        private decimal peso;
        private decimal altura;
        private List<Objetivo> listObjetivos;
        private List<TipoCuerpo> listTipoCuerpos;
        private List<Nivel> listNiveles;
        private List<Sexo> listsexos;
        public static Usuario ObjUsuario;
        public bool isRunning;
        public bool isEnabled;
        private string loadText;
        #endregion

        #region propiedades
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

        #region Constructor
        public RegisterPhysicalConditionViewModel()
        {
            this.IsEnabled = true;
            this.ListObjetivos = GetObjetivos().OrderBy(x => x.key).ToList();
            this.ListTipoCuerpos = GetTipoCuerpo().OrderBy(x => x.key).ToList();
            this.ListNiveles = GetNivel().OrderBy(x => x.key).ToList();
            this.Listsexos = GetSexos().OrderBy(s => s.key).ToList();
        }
        #endregion

        #region Comandos
        public ICommand SecondRegisterCmd
        {
            get
            {
                return new RelayCommand(SecondRegister);
            }
        }
        #endregion

        #region metodos
        private async void SecondRegister()
        {
            try
            {
                this.Objetivo = Objetivo as Objetivo;
                this.TipoCuerpo = TipoCuerpo as TipoCuerpo;
                this.Peso = Peso > 0 ? Peso : 0;
                this.Altura = Altura > 0 ? Altura : 0;
                this.Nivel = Nivel as Nivel;
                this.Sexo = Sexo as Sexo;

                if (Objetivo == null || Objetivo.key <= 0 || TipoCuerpo == null || TipoCuerpo.key <= 0 || Peso <= 0 || Altura <= 0 || Nivel == null || Nivel.key <= 0 || Sexo == null || Sexo.key <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
                    return;
                }

                this.IsRunning = true;
                this.IsEnabled = false;

                this.apiService = new ApiService();
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión.", "Aceptar");
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    return;
                }

                ObjUsuario.Apellido_materno = string.Empty;
                ObjUsuario.FechaRegistro = DateTime.Now;
                ObjUsuario.Id_tipoCuerpo = TipoCuerpo.key;
                ObjUsuario.Peso = Peso;
                ObjUsuario.Altura = Altura;
                ObjUsuario.Id_nivel = Nivel.key;
                ObjUsuario.Id_objetivo = Objetivo.key;
                ObjUsuario.Sexo = Sexo.key;

                string jsonUsuario = JsonConvert.SerializeObject(ObjUsuario);
                var response = await this.apiService.Post<Usuario>(ValuesService.url, "api/", "Login/?jsonUsuario=" + jsonUsuario, null);

                this.LoadText = "Generando plan de entrenamiento inicial, porfavor espere.";

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    this.LoadText = string.Empty;
                    return;
                }

                Usuario usuario = new Usuario();
                usuario = (Usuario)response.Result;

                if (usuario == null || usuario.Id_usuario == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error al registrar usuario, por favor intente nuevamente.", "Aceptar");
                    IsRunning = false;
                    IsEnabled = true;
                    this.LoadText = string.Empty;
                    return;
                }

                MainViewModel mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Usuario = new Usuario();
                mainViewModel.Usuario = usuario;
                mainViewModel.Settings = new SettingsViewModel();
                mainViewModel.TrainingPlan = new TrainingPlanViewModel();
                Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());

                
                await Application.Current.MainPage.DisplayAlert("Exito", "Se ha creado al nuevo usuario "+  ObjUsuario.Nombre + " " + ObjUsuario.Apellido_paterno, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());

                IsRunning = false;
                IsEnabled = true;
                this.LoadText = string.Empty;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error, por favor intente nuevamente.", "Aceptar");
            }
        }
        public void ReceiveNewUser(Usuario objUsuario)
        {
            RegisterPhysicalConditionViewModel.ObjUsuario = new Usuario();
            RegisterPhysicalConditionViewModel.ObjUsuario = objUsuario;
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
