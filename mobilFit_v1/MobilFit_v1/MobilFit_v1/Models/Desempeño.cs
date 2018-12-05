using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class Desempeño
    {
        [JsonProperty(PropertyName = "id_desempeño")]
        public int Id_desempeño { get; set; }

        [JsonProperty(PropertyName = "fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty(PropertyName = "horas_entrenamiento")]
        public float Horas_entrenamiento { get; set; }

        [JsonProperty(PropertyName = "distancia_recorrida")]
        public decimal Distancia_recorrida { get; set; }

        [JsonProperty(PropertyName = "id_plan_usuario")]
        public int Id_plan_usuario { get; set; }
        public Desempeño()
        {

        }
    }
}
