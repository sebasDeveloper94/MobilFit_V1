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
    public partial class RegisterPersonalDataPage : ContentPage
    {
        public RegisterPersonalDataPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPersonalDataViewModel();
        }

    }
}