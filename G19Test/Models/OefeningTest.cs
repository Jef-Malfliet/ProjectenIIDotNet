using G19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G19Test.Models {
    public class OefeningTest {
        private readonly string naam = "testoef1";
        private readonly string uitleg = "testuitleg";
        private readonly int aantalKeerBekeken = 5;
        private readonly GraadEnum graad = GraadEnum.Blauw;
        private readonly string video = "https://www.youtube.com/embed/t7pY-PffCTo";
        //private readonly IEnumerable<string> comments = new IEnumerable<string>();

        [Fact]
        public void TestCorrecteWaardeConstructor() {
            Oefening oef = new Oefening() {Naam = naam, AantalKeerBekeken = aantalKeerBekeken, Comments = , Graad = graad, Images = , Uitleg = uitleg, Video = video};
        }
    }
}
