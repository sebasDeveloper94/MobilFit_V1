using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class RutinaSeleccionada
    {
        [JsonProperty(PropertyName = "DiaEntrenamientos")]
        public DiasEntrenamiento DiaEntrenamientos { get; set; }

        [JsonProperty(PropertyName = "Ejercicios")]
        public List<Ejercicio> Ejercicios { get; set; }
        public RutinaSeleccionada()
        {

        }
    }
}
