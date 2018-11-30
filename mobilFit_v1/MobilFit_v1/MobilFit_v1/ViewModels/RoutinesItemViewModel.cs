using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Models;
using MobilFit_v1.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobilFit_v1.ViewModels
{
    public class RoutinesItemViewModel : Rutinas
    {
        #region Commands
        public ICommand SelectRoutineCommand
        {
            get
            {
                return new RelayCommand(SelectRoutine);
            }
        }
        #endregion

        #region Methods
        private async void SelectRoutine()
        {
            MainViewModel.GetInstance().Routine = new RoutineViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new RoutinePage());
        }
        #endregion
    }
}
