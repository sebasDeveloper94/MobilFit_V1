using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Service
{
    class LoginService
    {
        private LoginService login;
        public LoginService()
        {
            login = new LoginService();
        }

        public bool Acceso(string usuario, string contraseña) {

            string queryStr = string.Format("Login/?usuario={0}&contraseña={1}", usuario, contraseña);
            return false;
        }

        public Usuario RegistrarNuevoUsuario(Usuario objUsuario) {

            return null;
        }
    }
}
