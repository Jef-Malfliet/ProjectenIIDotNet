using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models {
    public class Session {
        public int Id { get; set; }
        public FormuleEnum Formule { get; set; }
        public DateTime Date { get; set; }

        //public IEnumerable<Lid> Leden { get; set; }
    }
}
