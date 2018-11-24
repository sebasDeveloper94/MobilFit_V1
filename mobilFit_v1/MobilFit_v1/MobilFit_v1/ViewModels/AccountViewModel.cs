using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Command;
using MobilFit_v1.Views;
using Xamarin.Forms;
using System.Windows.Input;

namespace MobilFit_v1.ViewModels
{
    public class AccountViewModel
    {
        #region Propiedade
        public List<AccountSettings> settings { get; set; } 
        #endregion
        #region Contructor
        public AccountViewModel()
        {
            AccountSettings();
        }
        #endregion
        #region Comandos
        public ICommand selectSettingCommand
        {
            get
            {
                return new RelayCommand(SelectSetings); 
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
        public void AccountSettings()
        {
            settings = new List<AccountSettings>();
            settings.Add(new AccountSettings() { description = "Notificaciones" });
            settings.Add(new AccountSettings() { description = "Ajustes de cuenta" });
            settings.Add(new AccountSettings() { description = "Acerca de MobilFit" });
        }
        private void SelectSetings()
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        private void CloseSesion() {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        #endregion
    }
    public class AccountSettings
    {
        public string description { get; set; }
    }
}
