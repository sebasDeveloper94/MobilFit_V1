using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class Profesional
    {
        [JsonProperty(PropertyName = "idProfesional")]
        public int IdProfesional { get; set; }

        [JsonProperty(PropertyName = "profesion")]
        public string Profesion { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        public Profesional()
        {

        }
    }
}
