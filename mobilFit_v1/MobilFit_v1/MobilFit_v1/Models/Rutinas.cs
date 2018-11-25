using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class Rutinas
    {
        [JsonProperty(PropertyName = "id_rutina")]
        public int Id_rutina { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public object Meta { get; set; }

        [JsonProperty(PropertyName = "id_tipoRutina")]
        public int Id_tipoRutina { get; set; }

        [JsonProperty(PropertyName = "id_categoria")]
        public int Id_categoria { get; set; }

        public Rutinas()
        {

        }
    }
}
