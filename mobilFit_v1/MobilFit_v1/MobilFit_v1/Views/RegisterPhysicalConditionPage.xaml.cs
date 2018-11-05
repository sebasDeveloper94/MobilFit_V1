﻿using MobilFit_v1.Models;
using MobilFit_v1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilFit_v1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPhysicalConditionPage : ContentPage
    {
        public RegisterPhysicalConditionPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPhysicalConditionViewModel();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool isContraindicacion = chkContraindicacion.IsToggled;
            if (isContraindicacion)
            {
                cboContraindicacion.IsEnabled = true;
            }
            else
            {
                cboContraindicacion.IsEnabled = false;
                cboContraindicacion.SelectedItem = string.Empty;
            }
        }
    }
}