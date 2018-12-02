using Newtonsoft.Json;

namespace MobilFit_v1.Models
{
    public class Tips
    {
        [JsonProperty(PropertyName = "id_tips")]
        public int id_tips { get; set; }

        [JsonProperty(PropertyName = "descripcion")]
        public string descripcion { get; set; }
        public Tips()
        {

        }
    }
}
