using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace MobilFit_v1.ViewModels
{
    public class RoutinesViewModel
    {
        #region Propiedades
        public List<Rutinas> rutinas { get; set; }
        #endregion
        #region Constructor
        public RoutinesViewModel()
        {
            ChargeRoutines();
        }
        #endregion
        #region Comandos
        public ICommand StartCommand
        {
            get
            {
                return new RelayCommand(StartTraining);
            }
        }
        #endregion
        #region Metodos
        private async void StartTraining()
        {
             Application.Current.MainPage = new NavigationPage(new TrainingPage());
        }
        private async void ChargeRoutines()
        {
            rutinas = new List<Rutinas>();
            rutinas.Add(new Rutinas() { name = "Rutina 1" });
            rutinas.Add(new Rutinas() { name = "Rutina 1" });
            rutinas.Add(new Rutinas() { name = "Rutina 1" });
        }
        #endregion
    }
    public class Rutinas
    {
        public string name { get; set; }
    }
}
