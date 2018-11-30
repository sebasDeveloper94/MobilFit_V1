using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace MobilFit_v1.ViewModels
{
    public class SettingsViewModel
    {
        private MainViewModel main;
        #region Propiedade

        #endregion

        #region Contructor
        public SettingsViewModel()
        {
            main = MainViewModel.GetInstance();
        }
        #endregion

        #region Comandos
        public ICommand GoAccountCommand
        {
            get
            {
                return new Command(AccountSettings); 
            }
        }
        public ICommand GoNotifacationsCommand
        {
            get
            {
                return new Command(NotifactionsSettings);
            }
        }
        public ICommand GoAboutCommand
        {
            get
            {
                return new Command(AboutSetings);
            }
        }
        public ICommand closeSesion
        {
            get
            {
                return new RelayCommand(CloseSesion);
            }
        }
        #endregion

        #region Metodos
        private void AccountSettings()
        {
            main.AccountSettings = new AccountSettingsViewModel();
            Application.Current.MainPage.Navigation.PushAsync(new AccountSettingsPage());
        }

        private void NotifactionsSettings()
        {
            Application.Current.MainPage.DisplayAlert("Atención", "Funciona las notifiaciones", "aceptar");
        }
        private void AboutSetings()
        {
            Application.Current.MainPage.DisplayAlert("Atención", "Funciona sobre", "aceptar");
        }
        private void CloseSesion() {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        #endregion
    }

}
