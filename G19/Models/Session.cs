using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models {
    [JsonObject(MemberSerialization.OptOut)]
    public class Session {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public FormuleEnum Formule { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }

       

        //public IEnumerable<Lid> Leden { get; set; }
    }
}
