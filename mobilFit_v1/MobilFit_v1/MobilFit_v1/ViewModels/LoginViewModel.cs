using MobilFit_v1.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.ViewModels
{
    class LoginViewModel
    {
        public LoginViewModel()
        {

        }

        public bool Login(string usuario, string contraseña)
        {
            LoginService loginService = new LoginService();
            bool acceso = loginService.Acceso(usuario, contraseña);
            return acceso;
        }
    }
}
