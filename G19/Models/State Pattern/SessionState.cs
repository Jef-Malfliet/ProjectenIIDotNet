using System;

namespace G19.Models.State_Pattern {
    public static class SessionState {

        public static SessionEnum state;
        public static Lid huidigLid;

        public static bool AanwezigheidRegistrerenState() {
            return state == SessionEnum.RegistreerState;
        }

        public static bool OefeningenBekijkenState() {
            return state == SessionEnum.OefeningState;
        }

        public static bool EindState() {
            return state == SessionEnum.EindState;
        }

        public static bool ToegestaandOefeningenBekijken(string graad) {
            int lidGraad = (int)huidigLid.Graad;
            int oefGraad = 0;
            if (graad != "ZWART" && graad != "ALLES")
                oefGraad = (int)Enum.Parse(typeof(GraadEnum), graad.ToUpper());
            else if (graad == "ZWART")
                oefGraad = (int)GraadEnum.DAN1;
            else if (graad == "ALLES")
                oefGraad = (int)GraadEnum.DAN1;

            if (state == SessionEnum.OefeningState) {
                return oefGraad <= lidGraad;
            }
            return false;
        }

        public static void ToState(SessionEnum newState) {
            SessionState.state = newState;
        }

        public static void VeranderHuidigLid(Lid lid) {
            SessionState.huidigLid = lid;
        }

    }
}
