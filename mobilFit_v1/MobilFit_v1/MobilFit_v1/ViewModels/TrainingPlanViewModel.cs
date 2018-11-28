using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MobilFit_v1.Service;
using MobilFit_v1.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MobilFit_v1.ViewModels
{
    public class TrainingPlanViewModel : BaseViewModel
    {
        public ApiService apiService = new ApiService();

        #region Attributes
        public PlanEntrenamiento objPlan;
        private ObservableCollection<RoutinesItemViewModel> routines;
        private bool isRefresing = false;
        private bool isEnabled = false;
        private string recommended;
        #endregion

        #region Properties 
        public ObservableCollection<RoutinesItemViewModel> Routines
        {
            get { return this.routines; }
            set { SetValue(ref this.routines, value); }
        }
        public string Recommended
        {
            get { return this.recommended; }
            set { SetValue(ref this.recommended, value); }
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
        #endregion

        #region Contructor
        public TrainingPlanViewModel()
        {
            IsEnabled = false;
            LoadRoutines();
        }
        #endregion

        #region Commands
        public ICommand StartCommand
        {
            get
            {
                return new RelayCommand(StartTraining);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRoutines);
            }
        }
        #endregion

        #region Metodos
        private void StartTraining()
        {
            int numDay = (int)DateTime.Today.DayOfWeek;
            int idRutina = 0;
            foreach (var item in objPlan.DiasEntrenamiento)
            {
                if (numDay == item.dia)
                {
                    idRutina = item.idRutina;
                    break;
                }
            }

            if (idRutina == 0)
            {
                Application.Current.MainPage.DisplayAlert("Atención", "No tiene una rutina para el día de hoy.", "Aceptar");
                return;
            }

            MainViewModel mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Training = new TrainingViewModel();
            mainViewModel.Routine = new RoutineViewModel();
            mainViewModel.Routine.IdRutina = idRutina;

            Application.Current.MainPage = new NavigationPage(new TrainingPage());
        }
        private async void LoadRoutines()
        {
            var connection = await this.apiService.CheckConnection();
            this.IsRefresing = true;
            this.IsEnabled = false;
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRefresing = false;
                this.IsEnabled = true;
                return;
            }

            int idUsuario = MainViewModel.GetInstance().Usuario.Id_usuario;

            var response = await this.apiService.Get<PlanEntrenamiento>(ValuesService.url, "api/", "PlanEntrenamiento/?id_usuario=", idUsuario);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                this.IsRefresing = false;
                this.IsEnabled = true;
                return;
            }

            this.objPlan = new PlanEntrenamiento();
            this.objPlan = (PlanEntrenamiento)response.Result;
            this.Routines = new ObservableCollection<RoutinesItemViewModel>(this.ToRoutineItemViewModel());
            this.Recommended = string.Format("Entrenamiento recomendado por el profesional \n {0}", objPlan.ObjPresional.Nombre);

            this.IsRefresing = false;
            this.IsEnabled = true;
        }
        private IEnumerable<RoutinesItemViewModel> ToRoutineItemViewModel()
        {
            return this.objPlan.RutinasPlan.Select(r => new RoutinesItemViewModel
            {

                Id_rutina = r.Id_rutina,
                Nombre = r.Nombre,
                Meta = r.Meta,
                Id_tipoRutina = r.Id_tipoRutina,
                Id_categoria = r.Id_categoria
            });
        }
        #endregion
    }
}
