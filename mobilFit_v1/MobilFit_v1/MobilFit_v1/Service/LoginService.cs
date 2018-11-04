using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MobilFit_v1.Service
{
    class LoginService
    {
        private MasterService masterService;
        public LoginService()
        {
            masterService = new MasterService();
        }

        public bool Acceso(string usuario, string contraseña) {

            string queryStr = string.Format("Login/?usuario={0}&contraseña={1}", usuario, contraseña);
            var response = masterService.Get(ValuesService.url + queryStr);
            return JsonConvert.DeserializeObject<string>(response.content.ToString()) == "ok" ? true: false;
        }

        public Usuario RegistrarNuevoUsuario(Usuario objUsuario) {

            return null;
        }
    }
}
