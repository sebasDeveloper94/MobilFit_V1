using MobilFit_v1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace MobilFit_v1.Service
{
    class LoginService
    {
        private MasterService masterService;
        public LoginService()
        {
            masterService = new MasterService();
        }

        public bool Acceso(string usuario, string contraseña)
        {

            string queryStr = string.Format("Login/?usuario={0}&contraseña={1}", usuario, contraseña);
            var response = masterService.Get(ValuesService.url + queryStr);
            return JsonConvert.DeserializeObject<string>(response.content.ToString()) == "ok" ? true : false;
        }

        public bool RegistrarNuevoUsuario(string jsonUsuario)
        {

            string contentType = "application/json";
            string queryStr = string.Format("Login/?jsonUsuario={0}", jsonUsuario);
            //var response = masterService.Post(ValuesService.url + queryStr, jsonUsuario);

            HttpClient oHttpClient = new HttpClient();
            var oTaskPostAsync = oHttpClient.PostAsync(ValuesService.url + queryStr, new StringContent(jsonUsuario, Encoding.UTF8, contentType)).Result;

            if (oTaskPostAsync.IsSuccessStatusCode)
            {
                return true;

            }
            else
            {
                return false;
            }

            //return JsonConvert.DeserializeObject<string>(response.content.ToString()) == "ok" ? true : false;
        }
    }
}
