using G19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G19Test.Models {
    public class LidTest {
        private readonly string voornaam = "Mout";
        private readonly string achternaam = "Pessemier";
        private readonly string email = "moutpessemier@hotmail.com";

        [Fact]
        public void TestConstructor() {
            Lid lid = new Lid() { Voornaam = voornaam, Familienaam = achternaam, Email = email };
            Assert.Equal(voornaam, lid.Voornaam);
            Assert.Equal(achternaam, lid.Familienaam);
            Assert.Equal(email, lid.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        public void TestFoutieveWaardenVoornaam(string voornaam) {
            Assert.Throws<ArgumentException>(() => new Lid() { Voornaam = voornaam, Familienaam = achternaam, Email = email });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        public void TestFoutieveWaardenFamilienaam(string achternaam) {
            Assert.Throws<ArgumentException>(() => new Lid() { Voornaam = voornaam, Familienaam = achternaam, Email = email });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("test@test")]
        [InlineData("test.com")]
        [InlineData("test@.com")]
        [InlineData("@test.com")]
        [InlineData("-test@test.com")]
        [InlineData("_test@test.com")]
        [InlineData("*test@test.com")]
        [InlineData(".test@test.com")]
        [InlineData("$test@test.com")]
        [InlineData("@test@test.com")]
        [InlineData("!test@test.com")]
        [InlineData("test&@test.com")]
        [InlineData("test@test&.com")]
        public void TestFoutieveWaardenEmail(string email) {
            Assert.Throws<ArgumentException>(() => new Lid() { Voornaam = voornaam, Familienaam = achternaam, Email = email });
        }
    }
}
