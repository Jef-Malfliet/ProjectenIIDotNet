using System;

namespace G19.Models {
    public class Oefening_Comments {
        //public int Id { get; set; }
        public int OefeningId { get; set; }
        public string Comments { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;

    }
}
