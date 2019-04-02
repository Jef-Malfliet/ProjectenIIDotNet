using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models {
    public class Oefening {

        #region Properties
        public int Id { get; set; }
        public string Uitleg { get; set; }
        public int AantalKeerBekeken { get; set; }
        public string Video { get; set; }
        public GraadEnum Graad { get; set; }
        public string Naam { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> Comments { get; set; }
        #endregion

        #region Constructors
        public Oefening() {

        }
        #endregion

    }
}
