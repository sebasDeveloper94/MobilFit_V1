using MobilFit_v1.Service;
using MobilFit_v1.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using System.Linq;
using System;
using Newtonsoft.Json;

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
        private string distance;
        private string time;
        private string name;
        private string peso;
        private string descanso;
        private Tips tips;
        private DateTime tiempoInicio;
        private DateTime tiempoTermino;
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
        public string Distance
        {
            get { return this.distance; }
            set { SetValue(ref this.distance, value); }
        }
        public string Peso
        {
            get { return this.peso; }
            set { SetValue(ref this.peso, value); }
        }
        public string Descanso
        {
            get { return this.descanso; }
            set { SetValue(ref this.descanso, value); }
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
        public Tips Tips
        {
            get { return this.tips; }
            set { SetValue(ref this.tips, value); }
        }
        public DateTime TiempoInicio
        {
            get { return this.tiempoInicio; }
            set { SetValue(ref this.tiempoInicio, value); }
        }
        public DateTime TiempoTermino
        {
            get { return this.tiempoTermino; }
            set { SetValue(ref this.tiempoTermino, value); }
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
        public ICommand ShowTipCommand
        {
            get
            {
                return new RelayCommand(ShowTips);
            }
        }
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
        private void ShowTips()
        {
            Application.Current.MainPage.DisplayAlert("Tips", this.Tips.descripcion, "Aceptar");
        }
        private async void EndTraining()
        {

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe tener conexión a internet", "Aceptar");
                return;
            }

            MainViewModel mainViewModel = MainViewModel.GetInstance();

            this.TiempoTermino = DateTime.Now;
            TimeSpan tiempo = TiempoTermino.Subtract(TiempoInicio);

            Desempeño desempeño = new Desempeño();
            desempeño.Distancia_recorrida = exerciseList.Sum(k => k.Distancia);
            desempeño.Horas_entrenamiento = (float)tiempo.TotalHours;
            desempeño.Fecha = DateTime.Now;
            desempeño.Id_plan_usuario = mainViewModel.TrainingPlan.objPlan.Id_PlanUsuario;

            string jsonDesempeño = JsonConvert.SerializeObject(desempeño);
            // probar que funcione el cargo de desempeño
            var response = await this.apiService.GetParameter<ReporteDesempeño>(ValuesService.url, "api/", "PlanEntrenamiento/", "?idRutina=" + mainViewModel.Routine.IdRutina + "&jsonDesempeño=" + jsonDesempeño);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, por favor intente nuevamente.", "Aceptar");
                return;
            }

            mainViewModel.TrainingPlan = new TrainingPlanViewModel();
            mainViewModel.Performance = new PerformanceViewModel();
            mainViewModel.Performance.Reporte = (ReporteDesempeño)response.Result;
            mainViewModel.Performance.LoadReport();
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
            Distance = exerciseList[Index].Distancia.ToString("0") + " km";
            Descanso = exerciseList[Index].Descanso.ToString("0") + " seg";
            Peso = exerciseList[Index].Peso.ToString("0") + " kg";
            Time = exerciseList[Index].Tiempo.Minute.ToString("00") + ":" + exerciseList[Index].Tiempo.Second.ToString("00");
            Tips = exerciseList[Index].Tips;

            this.TiempoInicio = DateTime.Now;
        }


        private async void NextExercise()
        {
            Index++;
            Index = Index >= exerciseList.Count ? (Index - 1) : Index;
            Name = exerciseList[Index].Nombre_ejercicio;
            Repetitions = exerciseList[Index].Repeticiones;
            Sets = exerciseList[Index].Series;
            Distance = exerciseList[Index].Distancia.ToString("0") + " km";
            Descanso = exerciseList[Index].Descanso.ToString("0") + " seg";
            Peso = exerciseList[Index].Peso.ToString("0") + " kg";
            Time = exerciseList[Index].Tiempo.Minute.ToString("00") + ":" + exerciseList[Index].Tiempo.Second.ToString("00");
            Tips = exerciseList[Index].Tips;
        }

        private async void LastExercise()
        {
            Index--;
            Index = Index < 0 ? 0 : Index;
            Name = exerciseList[Index].Nombre_ejercicio;
            Repetitions = exerciseList[Index].Repeticiones;
            Sets = exerciseList[Index].Series;
            Distance = exerciseList[Index].Distancia.ToString("0") + "km";
            Descanso = exerciseList[Index].Descanso.ToString("0") + " seg";
            Peso = exerciseList[Index].Peso.ToString("0") + " kg";
            Time = exerciseList[Index].Tiempo.Minute.ToString("00") + ":" + exerciseList[Index].Tiempo.Second.ToString("00");
            Tips = exerciseList[Index].Tips;
        }
        #endregion
    }
}
