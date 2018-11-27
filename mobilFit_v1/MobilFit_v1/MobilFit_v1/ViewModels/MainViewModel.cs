using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.ViewModels
{
    class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public TrainingPlanViewModel TrainingPlan { get; set; }
        public TrainingViewModel Training { get; set; }
        public RoutineViewModel Routine { get; set; }
        public TokenResponse Token { get; set; }
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
