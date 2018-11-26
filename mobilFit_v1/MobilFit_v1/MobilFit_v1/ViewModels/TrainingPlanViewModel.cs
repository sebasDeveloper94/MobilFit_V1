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
        private PlanEntrenamiento objPlan;
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
            MainViewModel.GetInstance().Training = new TrainingViewModel();
            Application.Current.MainPage = new NavigationPage(new TrainingPage());
        }
        private async void LoadRoutines()
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

            var response = await this.apiService.Get<PlanEntrenamiento>("https://mobilfitapiservice.azurewebsites.net/", "api/", "PlanEntrenamiento/?id_usuario=", 3);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRefresing = false;
                return;
            }

            objPlan = new PlanEntrenamiento();
            objPlan = (PlanEntrenamiento)response.Result;
            this.Routines = new ObservableCollection<RoutinesItemViewModel>(this.ToRoutineItemViewModel());
            Recommended = string.Format("Entrenamiento recomendado por el profesional \n {0}", objPlan.ObjPresional.Nombre);
            IsRefresing = false;
            IsEnabled = true;
        }
        private IEnumerable<RoutinesItemViewModel> ToRoutineItemViewModel()
        {
            return this.objPlan.RutinasPlan.Select(r => new RoutinesItemViewModel {

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
