using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace MobilFit_v1.ViewModels
{
    class RoutinesViewModel
    {

        #region Constructor
        public RoutinesViewModel()
        {

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
        #endregion
    }
}
