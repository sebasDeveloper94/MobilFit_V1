using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MobilFit_v1.Service;
using MobilFit_v1.Models;

namespace MobilFit_v1.ViewModels
{
    public class TrainingPlanViewModel : BaseViewModel
    {
        public ApiService apiService = new ApiService();

        #region Attributes
        private ObservableCollection<Rutinas> routines;
        bool isRefresing = false;
        #endregion

        #region Properties 
        public ObservableCollection<Rutinas> Routines
        {
            get { return this.routines; }
            set { SetValue(ref this.routines, value); }
        }
        public bool IsRefresing
        {
            get { return this.isRefresing; }
            set { SetValue(ref this.isRefresing, value); }
        }
        #endregion

        #region Contructor
        public TrainingPlanViewModel()
        {
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
        private async void StartTraining()
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
                IsRefresing  = false;
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

            PlanEntrenamiento objplan = (PlanEntrenamiento)response.Result;
            this.Routines = new ObservableCollection<Rutinas>(objplan.RutinasPlan);
            IsRefresing = false;
        }
        #endregion
    }
}
