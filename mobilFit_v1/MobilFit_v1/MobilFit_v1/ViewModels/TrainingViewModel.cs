using System;
using MobilFit_v1.Service;
using MobilFit_v1.Models;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;


namespace MobilFit_v1.ViewModels
{
    public class TrainingViewModel : BaseViewModel
    {
        public ApiService apiService = new ApiService();

        #region Attributes
        private int index;
        private bool isRunning = false;
        private List<Ejercicio> exerciseList;
        private int repetitions;
        private int sets;
        private decimal distance;
        private string time;
        private string name;
        #endregion

        #region Properties
        public int Index
        {
            get { return this.index; }
            set { SetValue(ref this.index, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public int Repetitions
        {
            get { return this.repetitions; }
            set { SetValue(ref this.repetitions, value); }
        }
        public int Sets
        {
            get { return this.sets; }
            set { SetValue(ref this.sets, value); }
        }
        public decimal Distance
        {
            get { return this.distance; }
            set { SetValue(ref this.distance, value); }
        }
        public string Time
        {
            get { return this.time; }
            set { SetValue(ref this.time, value); }
        }
        public string Name
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }
        #endregion

        #region Constructor
        public TrainingViewModel()
        {
            Index = 0;
            LoadExercise();
        }
        #endregion

        #region Commands
        public ICommand endTrainingCommand
        {
            get
            {
                return new RelayCommand(EndTraining);
            }
        }
        public ICommand changeCommandRight
        {
            get
            {
                return new Command(NextExercise);
            }
        }
        public ICommand changeCommandLeft
        {
            get
            {
                return new Command(LastExercise);
            }
        }
        #endregion

        #region Methods
        private void EndTraining()
        {
            MainViewModel.GetInstance().TrainingPlan = new TrainingPlanViewModel();
            Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
        }
        private async void LoadExercise()
        {
            var connection = await this.apiService.CheckConnection();
            IsRunning = true;

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRunning = false;
                return;
            }

            int idRutina = MainViewModel.GetInstance().Routine.IdRutina;

            var response = await this.apiService.GetList<Ejercicio>(ValuesService.url, "api/", "Ejercicios/?id_rutina=" + idRutina);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                IsRunning = false;
                return;
            }

            exerciseList = new List<Ejercicio>();
            exerciseList = (List<Ejercicio>)response.Result;

            Name = exerciseList[Index].Nombre_ejercicio;
            Repetitions = exerciseList[Index].Repeticiones;
            Sets = exerciseList[Index].Series;
            Distance = exerciseList[Index].Distancia;
            Time = exerciseList[Index].Tiempo.Minute.ToString() + ":"+ exerciseList[Index].Tiempo.Second.ToString();
        }
        private async void NextExercise()
        {
            Index++;
            Index = Index >= exerciseList.Count ? (Index - 1) : Index;
            Name = exerciseList[Index].Nombre_ejercicio;
            Repetitions = exerciseList[Index].Repeticiones;
            Sets = exerciseList[Index].Series;
            Distance = exerciseList[Index].Distancia;
            Time = exerciseList[Index].Tiempo.Minute.ToString() + ":" + exerciseList[Index].Tiempo.Second.ToString();
        }

        private async void LastExercise()
        {
            Index--;
            Index = Index < 0 ? 0 : Index;
            Name = exerciseList[Index].Nombre_ejercicio;
            Repetitions = exerciseList[Index].Repeticiones;
            Sets = exerciseList[Index].Series;
            Distance = exerciseList[Index].Distancia;
            Time = exerciseList[Index].Tiempo.Minute.ToString() + ":" + exerciseList[Index].Tiempo.Second.ToString();
        }
        #endregion
    }
}
