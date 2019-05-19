using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace G19.Models {
    public class Oefening {

        #region Fields
        private string _video;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Uitleg { get; set; }
        public int AantalKeerBekeken { get; set; }
        public string Video {
            get {
                return _video;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("VideoURL mag niet leeg zijn.");
                }
                Regex normalUrl = new Regex(@"^(?:.+?)?(?:\/v\/|watch\?v=|\&v=|youtu\\.be\/|\/v=|^youtu\.be\/)([a-zA-Z0-9_-]{11})+$");
                Match match = normalUrl.Match(value);
                if (match.Success) {
                    _video = ConvertVideoUrlToEmbed(value);
                } else {
                    Regex embedUrl = new Regex(@"^(?:.+?)?(?:\/v\/|embed\/|youtu\\.be\/|^youtu\.be\/)([a-zA-Z0-9_-]{11})+$");
                    match = embedUrl.Match(value);
                    if (!match.Success) {
                        throw new ArgumentException("URL is niet van het juiste formaat.");
                    }
                    _video = value;
                }
            }
        }
        public GraadEnum Graad { get; set; }
        public string Naam { get; set; }
        public IList<Oefening_Images> Images { get; set; }
        public IList<Oefening_Comments> Comments { get; set; }
        #endregion

        #region Constructors
        public Oefening() {

        }
        #endregion

        #region Methods
        public string GetFirstImage() {
            if (Images.Count > 0)
                return Images[0].getImageName();
            else
                return "stock.jpg";
        }
        public string ConvertVideoUrlToEmbed(string url) {
            Uri uri;
            try {
                uri = new Uri(url);
            } catch (UriFormatException ex) {
                throw new ArgumentException(ex.Message);
            } catch (ArgumentNullException ex) {
                throw new ArgumentException(ex.Message);
            }

            NameValueCollection query;

            try {
                query = HttpUtility.ParseQueryString(uri.Query);
            } catch (UriFormatException ex) {
                throw new ArgumentException(ex.Message);
            } catch (ArgumentNullException ex) {
                throw new ArgumentException(ex.Message);
            }

            var videoId = string.Empty;

            if (query.AllKeys.Contains("v")) {
                videoId = query["v"];
            } else {
                videoId = uri.Segments.Last();
            }

            return "https://" + uri.Host + "/embed/" + videoId;
        }

        public bool hasUitleg() {
            return Uitleg?.DefaultIfEmpty() != null;
        }
        public bool hasImages() {
            if (Images != null) {
                return Images.Count > 0;
            }
            return false;
        }
        public bool hasVideo() {
            return Video?.DefaultIfEmpty() != null ;
        }
        public bool hasComments() {
            if (Comments != null) {
                return Comments.Count > 0;
            }
            return false;
        }
        #endregion
    }
}
