using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class ReporteDesempeño
    {
        [JsonProperty(PropertyName = "id_reporteDesempeño")]
        public int Id_reporteDesempeño { get; set; }

        [JsonProperty(PropertyName = "fechaReporte")]
        public DateTime FechaReporte { get; set; }

        [JsonProperty(PropertyName = "porcentajeRutina")]
        public float PorcentajeRutina { get; set; }

        [JsonProperty(PropertyName = "tiempoEntrenamiento")]
        public float TiempoEntrenamiento { get; set; }

        [JsonProperty(PropertyName = "caloriasQuemadas")]
        public float CaloriasQuemadas { get; set; }

        [JsonProperty(PropertyName = "kmRecorridos")]
        public float KmRecorridos { get; set; }

        [JsonProperty(PropertyName = "IMC")]
        public float IMC { get; set; }

        [JsonProperty(PropertyName = "IGC")]
        public float IGC { get; set; }

        [JsonProperty(PropertyName = "id_desempeño")]
        public int Id_desempeño { get; set; }

        public ReporteDesempeño()
        {

        }
    }
}
