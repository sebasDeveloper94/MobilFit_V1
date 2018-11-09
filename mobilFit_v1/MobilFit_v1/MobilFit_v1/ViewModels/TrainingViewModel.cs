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
    public class TrainingViewModel
    {

        #region Constructor
        public TrainingViewModel()
        {

        }
        #endregion
        #region Comandos
        public ICommand endTrainingCommand
        {
            get
            {
                return new RelayCommand(EndTraining);
            }
        }
        #endregion
        #region Metodos
        private void EndTraining()
        {
            Application.Current.MainPage = new NavigationPage(new UserMainMenuPage());
        }
        #endregion
    }
}
