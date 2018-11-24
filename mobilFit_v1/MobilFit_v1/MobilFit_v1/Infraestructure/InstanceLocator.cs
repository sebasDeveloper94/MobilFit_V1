using MobilFit_v1.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Infraestructure
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
