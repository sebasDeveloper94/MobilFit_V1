using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class Usuario
    {
        [JsonProperty(PropertyName = "id_usuario")]
        public int Id_usuario { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "apellido_paterno")]
        public string Apellido_paterno { get; set; }

        [JsonProperty(PropertyName = "apellido_materno")]
        public string Apellido_materno { get; set; }

        [JsonProperty(PropertyName = "sexo")]
        public int Sexo { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "contraseña")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "fechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        [JsonProperty(PropertyName = "peso")]
        public decimal Peso { get; set; }

        [JsonProperty(PropertyName = "altura")]
        public decimal Altura { get; set; }

        [JsonProperty(PropertyName = "id_tipoCuerpo")]
        public int Id_tipoCuerpo { get; set; }

        [JsonProperty(PropertyName = "id_nivel")]
        public int Id_nivel { get; set; }

        [JsonProperty(PropertyName = "id_objetivo")]
        public int Id_objetivo { get; set; }

        public Usuario()
        {

        }
    }
}
