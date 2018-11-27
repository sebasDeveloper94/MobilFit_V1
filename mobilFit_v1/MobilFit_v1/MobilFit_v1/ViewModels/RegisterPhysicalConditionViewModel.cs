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
    public class RegisterPhysicalConditionViewModel
    {
        private ApiService apiService;
        #region propiedades
        public Objetivo objetivo { get; set; }
        public TipoCuerpo tipoCuerpo { get; set; }
        public decimal peso { get; set; }
        public decimal altura { get; set; }
        public Nivel nivel { get; set; }
        public Sexo sexo { get; set; }
        public List<Objetivo> listObjetivos { get; set; }
        public List<TipoCuerpo> listTipoCuerpos { get; set; }
        public List<Nivel> listNiveles { get; set; }
        public List<Sexo> listsexos { get; set; }
        public static Usuario objUsuario;
        #endregion
        #region Constructor
        public RegisterPhysicalConditionViewModel()
        {
            this.listObjetivos = GetObjetivos().OrderBy(x => x.key).ToList();
            this.listTipoCuerpos = GetTipoCuerpo().OrderBy(x => x.key).ToList();
            this.listNiveles = GetNivel().OrderBy(x => x.key).ToList();
            this.listsexos = GetSexos().OrderBy(s => s.key).ToList();
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
                this.objetivo = objetivo as Objetivo;
                this.tipoCuerpo = tipoCuerpo as TipoCuerpo;
                this.peso = peso > 0 ? peso : 0;
                this.altura = altura > 0 ? altura : 0;
                this.nivel = nivel as Nivel;
                this.sexo = sexo as Sexo;
                bool isCorrect = false;

                if (objetivo.key <= 0 || tipoCuerpo.key <= 0 || peso <= 0 || altura <= 0 || nivel.key <= 0 || sexo.key <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
                    return;
                }
                apiService = new ApiService();
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "No hay conexión.", "Aceptar");
                    return;
                }

                objUsuario.Apellido_materno = string.Empty;
                objUsuario.FechaRegistro = DateTime.Now;
                objUsuario.Id_tipoCuerpo = tipoCuerpo.key;
                objUsuario.Peso = peso;
                objUsuario.Altura = altura;
                objUsuario.Id_nivel = nivel.key;
                objUsuario.Sexo = sexo.key;
                string jsonUsuario = JsonConvert.SerializeObject(objUsuario);

                var response = await this.apiService.Post<string>("https://mobilfitapiservice.azurewebsites.net/", "api/", "Login/?jsonUsuario=" + jsonUsuario, "");
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    return;
                }

                await Application.Current.MainPage.DisplayAlert("Exito", "Se ha creado al nuevo usuario "+  objUsuario.Nombre + " " + objUsuario.Apellido_paterno, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Ha ocurrido un error, por favor intente nuevamente.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
        public void ReceiveNewUser(Usuario objUsuario)
        {
            RegisterPhysicalConditionViewModel.objUsuario = new Usuario();
            RegisterPhysicalConditionViewModel.objUsuario = objUsuario;
        }
        public List<Objetivo> GetObjetivos()
        {
            List<Objetivo> objetivos = new List<Objetivo>()
            {
                new Objetivo() {key = 3, value="Ganar mas fuerza" },
                new Objetivo() {key = 4, value="Bajar de peso" },
                new Objetivo() {key = 5, value="Tonificar" },
                new Objetivo() {key = 6, value="Ganar masa muscular" },
                new Objetivo() {key = 7, value="Mantenerme" },
                new Objetivo() {key = 8, value="Buena salud" }
            };
            return objetivos;
        }
        public List<TipoCuerpo> GetTipoCuerpo()
        {
            List<TipoCuerpo> tipoCuerpos = new List<TipoCuerpo>()
            {
                new TipoCuerpo() {key = 2, value="Endomorfo (Contextura gruesa)" },
                new TipoCuerpo() {key = 3, value="Mesomorfo (Contextura atlética)" },
                new TipoCuerpo() {key = 4, value="Ectomorfo (Contextura delgada)" }
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
