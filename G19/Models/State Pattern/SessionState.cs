using G19.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace G19.Models.State_Pattern {
    [ServiceFilter(typeof(LidFilter))]
    public static class SessionState {

        public static SessionEnum state;
        public static Lid huidigLid;

        public static DayOfWeek vandaag = DateTime.Today.DayOfWeek;

        public static void FakeVandaag(DayOfWeek fakevandaag) {
            vandaag = fakevandaag;
        }
       
        public static bool AanwezigheidRegistrerenState() {
            return state == SessionEnum.RegistreerState;
        }

        public static bool OefeningenBekijkenState() {
            return state == SessionEnum.OefeningState;
        }

        public static bool EindState() {
            return state == SessionEnum.EindState;
        }

        public static bool ToegestaandOefeningenBekijken(string graad,Boolean isAlsLidIngelogd) {
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

        public static void ToState(SessionEnum newState) {
            SessionState.state = newState;
        }

        [ServiceFilter(typeof(LidFilter))]
        public static void VeranderHuidigLid(Lid lid) {
            SessionState.huidigLid = lid;
        }

    }
}
