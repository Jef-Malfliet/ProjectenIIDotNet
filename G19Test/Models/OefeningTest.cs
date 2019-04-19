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
        private readonly GraadEnum graad = GraadEnum.BLAUW;
        private readonly string video = "https://www.youtube.com/embed/t7pY-PffCTo";
        private readonly IList<Oefening_Comments> comments = new List<Oefening_Comments>();
        private readonly IList<Oefening_Images> images = new List<Oefening_Images>();

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
        [InlineData("https://www.youtu.be/?v=qsN1LglrX9s")] 
        [InlineData("https://www.youtu.be/watdh?v=qsN1LglrX9s")]
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
        [Theory]
        [InlineData("https://m.youtube.com/watch?v=GYXKCF33hjI", "https://m.youtube.com/embed/GYXKCF33hjI")]
        [InlineData("https://www.youtube.com/watch?v=GYXKCF33hjI", "https://www.youtube.com/embed/GYXKCF33hjI")]
        [InlineData("https://www.youtube.com/watch?v=5Z2C0wy4bmg", "https://www.youtube.com/embed/5Z2C0wy4bmg")]
        public void TestConvertToEmbededUrl(string urlNormaal,string embeded) {

            string convert = (new Oefening()).ConvertVideoUrlToEmbed(urlNormaal);
            Assert.Equal(embeded, convert);
        }
    }
}
