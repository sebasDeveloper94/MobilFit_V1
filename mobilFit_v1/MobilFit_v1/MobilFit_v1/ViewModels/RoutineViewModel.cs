using MobilFit_v1.Models;
using MobilFit_v1.Service;
using MobilFit_v1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace MobilFit_v1.ViewModels
{
    public class RoutineViewModel : BaseViewModel
    {
        public ApiService apiService = new ApiService();

        #region Attributes
        private Rutinas routinesItemViewModel;
        private string name;
        private string meta;
        private int idRutina;
        private List<Ejercicio> ejercicios;
        private bool isRefresing;
        private bool isEnabled;
        private bool isRunning;
        private List<DiasRutina> days;
        private DiasRutina daySelected;
        private RutinaSeleccionada rutinaSeleccionada;
        #endregion

        #region Properties
        public Rutinas RoutinesItemViewModel
        {
            get { return this.routinesItemViewModel; }
            set { SetValue(ref this.routinesItemViewModel, value); }
        }
        public string Name
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }
        public string Meta
        {
            get { return this.meta; }
            set { SetValue(ref this.meta, value); }
        }
        public int IdRutina
        {
            get { return this.idRutina; }
            set { SetValue(ref this.idRutina, value); }
        }
        public List<Ejercicio> Exercises
        {
            get { return this.ejercicios; }
            set { SetValue(ref this.ejercicios, value); }
        }
        public bool IsRefresing
        {
            get { return this.isRefresing; }
            set { SetValue(ref this.isRefresing, value); }
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
        public List<DiasRutina> Days
        {
            get { return this.days; }
            set { SetValue(ref this.days, value); }
        }
        public DiasRutina DaySelected
        {
            get { return this.daySelected; }
            set { SetValue(ref this.daySelected, value); }
        }
        public RutinaSeleccionada RutinaSeleccionada
        {
            get { return this.rutinaSeleccionada; }
            set { SetValue(ref this.rutinaSeleccionada, value); }
        }
        #endregion

        #region Constructor
        public RoutineViewModel(Rutinas routinesItemViewModel)
        {
            apiService = new ApiService();

            this.RoutinesItemViewModel = routinesItemViewModel;
            this.Name = this.RoutinesItemViewModel.Nombre;
            this.Meta = this.RoutinesItemViewModel.Meta;
            this.IdRutina = this.RoutinesItemViewModel.Id_rutina;

            Days = new List<DiasRutina>();
            this.Days = this.ChargeDays().OrderBy(d => d.Key).ToList();

            this.ChargeExercises();
        }
        public RoutineViewModel()
        {
                
        }
        #endregion

        #region Commands
        public ICommand SaveDaysCommand
        {
            get
            {
                return new RelayCommand(SaveDay);
            }
        }
        #endregion

        #region Methods
        private async void ChargeExercises()
        {
            var connection = await this.apiService.CheckConnection();
            this.IsRefresing = true;
            this.IsRunning = true;
            this.IsEnabled = false;
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRefresing = false;
                return;
            }

            var response = await this.apiService.Get<RutinaSeleccionada>(ValuesService.url, "api/", "PlanEntrenamiento/?idRutina=", IdRutina);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRefresing = false;
                return;
            }

            this.RutinaSeleccionada = new RutinaSeleccionada();
            this.RutinaSeleccionada = (RutinaSeleccionada)response.Result;
            this.Exercises = new List<Ejercicio>();
            this.Exercises = RutinaSeleccionada.Ejercicios;

            this.IsRefresing = false;
            this.IsRunning = false;
            this.IsEnabled = true;

            this.SelectedDay();
        }
        private async void SaveDay()
        {
            var connection = await this.apiService.CheckConnection();
            IsRunning = true;

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRefresing = false;
                return;
            }
            int idPlan = MainViewModel.GetInstance().TrainingPlan.objPlan.Id_PlanUsuario;

            DiasEntrenamiento objDia = new DiasEntrenamiento();
            objDia.idPlan = idPlan;
            objDia.idRutina = IdRutina;
            objDia.dia = DaySelected.Key;

            var json = JsonConvert.SerializeObject(objDia);
            var response = await this.apiService.Post<string>(ValuesService.url, "api/", "PlanEntrenamiento/?jsonDias=" + json, "");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRefresing = false;
                return;
            }

            IsRunning = false;
            await Application.Current.MainPage.DisplayAlert("Exito", "El día de la rutina ha sido guardado", "Aceptar");
        }
        public List<DiasRutina> ChargeDays()
        {
            List<DiasRutina> Days = new List<DiasRutina>()
            {
                new DiasRutina() {Key = 1, Value="Lunes" },
                new DiasRutina() {Key = 2, Value="Martes" },
                new DiasRutina() {Key = 3, Value="Miércoles" },
                new DiasRutina() {Key = 4, Value="Jueves" },
                new DiasRutina() {Key = 5, Value="Viernes" },
                new DiasRutina() {Key = 6, Value="Sábado" },
                new DiasRutina() {Key = 7, Value="Domingo" },
            };
            return Days;
        }
        public void SelectedDay()
        {
            DaySelected = new DiasRutina();
            DaySelected.Key = RutinaSeleccionada.DiaEntrenamientos.dia;
            DaySelected.Value = DayName(RutinaSeleccionada.DiaEntrenamientos.dia);
        }
        private string DayName(int numDay)
        {
            string dayName = string.Empty;
            switch (numDay)
            {
                case 1:
                    dayName = "Lunes";
                    break;
                case 2:
                    dayName = "Martes";
                    break;
                case 3:
                    dayName = "Miércoles";
                    break;
                case 4:
                    dayName = "Jueves";
                    break;
                case 5:
                    dayName = "Viernes";
                    break;
                case 6:
                    dayName = "Sábado";
                    break;
                case 7:
                    dayName = "Domingo";
                    break;
                default:
                    break;
            }
            return dayName;
        }
        #endregion
    }

    public class DiasRutina
    {
        public string Value { get; set; }
        public int Key { get; set; }
    }
}
