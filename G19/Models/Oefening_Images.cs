namespace G19.Models {
    public class Oefening_Images {
        public int OefeningId { get; set; }
        public string Images { get; set; }
        public string getImageName() {
            string[] pieces = Images.Split('/');
            return pieces[pieces.Length - 1];
        }
    }
}
