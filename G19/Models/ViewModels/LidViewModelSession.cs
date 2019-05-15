using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace G19.Models.ViewModels {
    public class LidViewModelSession {
        #region fields
        public int Id { get; set; }

      

        [StringLength(60, ErrorMessage = "Maximum 60"), Required]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20"), Required]
        public string GSM { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20"), Required]
        public string Telefoon { get; set; }


        [StringLength(3, ErrorMessage = "Maximum 3")]
        public string Busnummer { get; set; }

        [StringLength(3, ErrorMessage = "Maximum 3"), Required]
        public string Huisnummer { get; set; }

        [StringLength(60, ErrorMessage = "Maximum 60")]
        public string EmailOuders { get; set; }


        [StringLength(4, ErrorMessage = "Maximum 4"), Required]
        public string Postcode { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string Stad { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string StraatNaam { get; set; }

       
        #endregion

        public LidViewModelSession() {

        }
        public LidViewModelSession(Lid lid) {
            Id = lid.Id;
            Email = lid.Email;
            GSM = lid.GSM;
            Telefoon = lid.Telefoon;
            Busnummer = lid.Busnummer;
            Huisnummer = lid.Huisnummer;
            EmailOuders = lid.EmailOuders;
            Postcode = lid.PostCode;
            Stad = lid.Stad;
            StraatNaam = lid.StraatNaam;
          
        }
    }
}
