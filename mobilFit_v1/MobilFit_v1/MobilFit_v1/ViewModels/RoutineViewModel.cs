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
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        public bool IsEnabled
        {
            get { return this.isRefresing; }
            set { SetValue(ref this.isRefresing, value); }
        }
        public bool IsRunning
        {
            get { return this.isRefresing; }
            set { SetValue(ref this.isRefresing, value); }
        }
        public List<DiasRutina> Days { get; set; }
        #endregion

        #region Constructor
        public RoutineViewModel(Rutinas routinesItemViewModel)
        {
            this.IsEnabled = true;
            this.IsRunning = false;
            this.RoutinesItemViewModel = routinesItemViewModel;
            this.Name = this.RoutinesItemViewModel.Nombre;
            this.Meta = this.RoutinesItemViewModel.Meta;
            this.IdRutina = this.RoutinesItemViewModel.Id_rutina;
            this.ChargeExercises();
            Days = new List<DiasRutina>();
            this.Days = this.ChargeDays().OrderBy(d => d.Key).ToList();
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
            IsRefresing = true;

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRefresing = false;
                return;
            }

            var response = await this.apiService.GetList<Ejercicio>("https://mobilfitapiservice.azurewebsites.net/", "api/", "Ejercicios/?id_rutina=" + IdRutina);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRefresing = false;
                return;
            }

            Exercises = new List<Ejercicio>();
            Exercises = (List<Ejercicio>)response.Result;
            IsRefresing = false;
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

            //var response = await this.apiService.GetList<Ejercicio>("https://mobilfitapiservice.azurewebsites.net/", "api/", "Ejercicios/?id_rutina=" + IdRutina);
            //if (!response.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
            //    Application.Current.MainPage = new NavigationPage(new LoginPage());
            //    IsRefresing = false;
            //    return;
            //}

            await Application.Current.MainPage.DisplayAlert("Exito", "El día de la ruitna ha sido guardado", "Aceptar");
            IsRunning = false;
        }
        #endregion
    }

    public class DiasRutina
    {
        public string Value { get; set; }
        public int Key { get; set; }
    }
}
