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
        private readonly IList<string> comments = new List<string>();
        private readonly IList<string> images = new List<string>();

        [Fact]
        public void TestCorrecteWaardeConstructor() {
            Oefening oef = new Oefening() {Naam = naam, AantalKeerBekeken = aantalKeerBekeken, Comments = comments,
                Graad = graad, Images = images , Uitleg = uitleg, Video = video};
            Assert.Equal(naam, oef.Naam);
            Assert.Equal(uitleg, oef.Uitleg);
            Assert.Equal(aantalKeerBekeken, oef.AantalKeerBekeken);
            Assert.Equal(graad, oef.Graad);
            Assert.Equal(video, oef.Video);
            Assert.Equal(comments, oef.Comments);
            Assert.Equal(images, oef.Images);
            Assert.Empty(comments);
            Assert.Empty(images);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\t\r\n")]
        [InlineData("                      ")]
        [InlineData(null)]
        [InlineData("https://www.youtube.com/watch?v5Z2C0wy4bmg")]
        [InlineData("https://www.youtube.com/watchv=5Z2C0wy4bmg")]
        [InlineData("https://www.youtube.com/watch?v=")]
        [InlineData("https://www.youtube.com/")]
        [InlineData("https://soundcloud.com/nourish")]
        public void TestFoutieveWaardeURLs(string video) {
            Assert.Throws<ArgumentException>(() => new Oefening() {
                Naam = naam,
                AantalKeerBekeken = aantalKeerBekeken,
                Comments = comments,
                Graad = graad,
                Images = images,
                Uitleg = uitleg,
                Video = video
            });
        }
    }
}
