using Newtonsoft.Json;
using System;

namespace G19.Models.State_Pattern {

    [JsonObject(MemberSerialization.OptIn)]
    public class SessionState {

        [JsonProperty]
        public SessionEnum state;
        [JsonProperty]
        public Lid huidigLid;
        public DayOfWeek vandaag = DateTime.Today.DayOfWeek;

        public void FakeVandaag(DayOfWeek fakevandaag) {
            vandaag = fakevandaag;
        }

        public bool AanwezigheidRegistrerenState() {
            return state == SessionEnum.RegistreerState;
        }

        public bool OefeningenBekijkenState() {
            return state == SessionEnum.OefeningState;
        }

        public bool EindState() {
            return state == SessionEnum.EindState;
        }

        public bool ToegestaandOefeningenBekijken(string graad, bool isAlsLidIngelogd) {
            int lidGraad = (int)huidigLid.Graad;
            int oefGraad = 0;
            if (graad != "ZWART" && graad != "ALLES")
                oefGraad = (int)Enum.Parse(typeof(GraadEnum), graad.ToUpper());
            else if (graad == "ZWART")
                oefGraad = (int)GraadEnum.DAN1;
            else if (graad == "ALLES")
                oefGraad = (int)GraadEnum.DAN1;

            if (state == SessionEnum.OefeningState || isAlsLidIngelogd) {
                return oefGraad <= lidGraad;
            }
            return false;
        }

        public void ToState(SessionEnum newState) {
            state = newState;
        }

        public void VeranderHuidigLid(Lid lid) {
            huidigLid = lid;
        }

    }
}
