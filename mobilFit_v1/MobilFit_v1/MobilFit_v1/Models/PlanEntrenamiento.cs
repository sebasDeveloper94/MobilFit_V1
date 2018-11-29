using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilFit_v1.Models
{
    public class PlanEntrenamiento
    {
        [JsonProperty(PropertyName = "idPlan")]
        public int IdPlan { get; set; }

        [JsonProperty(PropertyName = "nombre")]
        public string Nombre { get; set; }

        [JsonProperty(PropertyName = "candidadDias")]
        public int CandidadDias { get; set; }

        [JsonProperty(PropertyName = "tipoPlan")]
        public int TipoPlan { get; set; }

        [JsonProperty(PropertyName = "objetivo")]
        public int Objetivo { get; set; }

        [JsonProperty(PropertyName = "nivel")]
        public int Nivel { get; set; }

        [JsonProperty(PropertyName = "objPresional")]
        public Profesional ObjPresional { get; set; }

        [JsonProperty(PropertyName = "rutinasPlan")]
        public List<Rutinas> RutinasPlan { get; set; }

        [JsonProperty(PropertyName = "objUsuario")]
        public Usuario ObjUsuario { get; set; }

        //[JsonProperty(PropertyName = "DiasEntrenamiento")]
        //public List<DiasEntrenamiento> DiasEntrenamiento { get; set; }

        [JsonProperty(PropertyName = "id_planUsuario")]
        public int Id_PlanUsuario { get; set; }

        public PlanEntrenamiento()
        {

        }
    }
}
