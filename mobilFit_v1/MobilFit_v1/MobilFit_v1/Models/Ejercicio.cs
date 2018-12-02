using Newtonsoft.Json;
using System;

namespace MobilFit_v1.Models
{
    public class Ejercicio
    {
        [JsonProperty(PropertyName = "id_ejercicio")]
        public int Id_ejercicio { get; set; }

        [JsonProperty(PropertyName = "nombre_ejercicio")]
        public string Nombre_ejercicio { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty(PropertyName = "repeticiones")]
        public int Repeticiones { get; set; }

        [JsonProperty(PropertyName = "series")]
        public int Series { get; set; }

        [JsonProperty(PropertyName = "peso")]
        public decimal Peso { get; set; }

        [JsonProperty(PropertyName = "tiempo")]
        public DateTime Tiempo { get; set; }

        [JsonProperty(PropertyName = "distancia")]
        public decimal Distancia { get; set; }

        [JsonProperty(PropertyName = "descanso")]
        public decimal Descanso { get; set; }

        [JsonProperty(PropertyName = "Tips")]
        public Tips Tips { get; set; }
        public Ejercicio()
        {

        }
    }
}
