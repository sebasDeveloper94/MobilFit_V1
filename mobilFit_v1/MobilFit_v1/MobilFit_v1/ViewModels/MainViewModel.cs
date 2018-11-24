using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.ViewModels
{
    class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public RoutinesViewModel Routines { get; set; }
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        #region Singleton
        private static MainViewModel instance { get; set; }
        public static MainViewModel GetInstance() {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
