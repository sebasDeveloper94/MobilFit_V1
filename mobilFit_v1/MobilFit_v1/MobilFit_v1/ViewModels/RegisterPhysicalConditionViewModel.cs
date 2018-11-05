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

namespace MobilFit_v1.ViewModels
{
    public class RegisterPhysicalConditionViewModel
    {
        #region propiedades
        public Objetivo objetivo { get; set; }
        public TipoCuerpo tipoCuerpo { get; set; }
        public decimal peso { get; set; }
        public decimal altura { get; set; }
        public Nivel nivel { get; set; }
        public Contraindicacion contraindicacion { get; set; }
        public List<Objetivo> listObjetivos { get; set; }
        public List<TipoCuerpo> listTipoCuerpos { get; set; }
        public List<Nivel> listNiveles { get; set; }
        public List<Contraindicacion> listContraindicaciones { get; set; }
        public static Usuario usuario;
        #endregion
        #region Constructor
        public RegisterPhysicalConditionViewModel()
        {
            this.listObjetivos = GetObjetivos().OrderBy(x => x.key).ToList();
            this.listTipoCuerpos = GetTipoCuerpo().OrderBy(x => x.key).ToList();
            this.listNiveles = GetNivel().OrderBy(x => x.key).ToList();
            this.listContraindicaciones = GetContraindicacion().OrderBy(x => x.key).ToList();
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
            objetivo = objetivo as Objetivo;
            tipoCuerpo = tipoCuerpo as TipoCuerpo;
            peso = peso > 0 ? peso : 0;
            altura = altura > 0 ? altura : 0;
            nivel = nivel as Nivel;
            contraindicacion = contraindicacion as Contraindicacion;

            if (objetivo.key <= 0 || tipoCuerpo.key <= 0 || peso <= 0 || altura <= 0 || nivel.key <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos.", "Aceptar");
                return;
            }

            usuario.id_tipoCuerpo = tipoCuerpo.key;
            usuario.peso = peso;
            usuario.altura = altura;
            usuario.id_nivel = nivel.key;
            var obj = usuario;
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        public void ReceiveNewUser(Usuario objUsuario)
        {
            usuario = new Usuario();
            usuario = objUsuario;
        }
        public List<Objetivo> GetObjetivos()
        {
            List<Objetivo> objetivos = new List<Objetivo>()
            {
                new Objetivo() {key = 1, value="Tonificar" },
                new Objetivo() {key = 2, value="Mas fuerza" },
                new Objetivo() {key = 3, value="Bajar de peso" },
                new Objetivo() {key = 4, value="Mantenerme" },
                new Objetivo() {key = 5, value="Mejor salud" }
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
        public List<Contraindicacion> GetContraindicacion()
        {
            List<Contraindicacion> contraindicacions = new List<Contraindicacion>()
            {
                new Contraindicacion() {key = 1, value="Tengo o tuve una o varias lesiones" },
                new Contraindicacion() {key = 2, value="Asma" },
                new Contraindicacion() {key = 3, value="Osteoporosis" },
                new Contraindicacion() {key = 4, value="Enfermedades neurológicas" }
            };
            return contraindicacions;
        }
        #endregion
    }
}
