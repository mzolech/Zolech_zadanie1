using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zolech_zadanie1
{
    public class Uzytkownik
    {
        private String MZ_Identyfikator;
        private DateTime MZ_CzasRejestracji;
        private int MZ_ZmianyRozmiaru;
        private int MZ_ZmianyPolozenia;

        public Uzytkownik(String MZ_Id, DateTime MZ_czas, int MZ_rozmiar, int MZ_polozenie)
        {
            MZ_Identyfikator = MZ_Id;
            MZ_CzasRejestracji = MZ_czas;
            MZ_ZmianyRozmiaru = MZ_rozmiar;
            MZ_ZmianyPolozenia = MZ_polozenie;
        }

        public Uzytkownik(String MZ_Id)
        {
            MZ_Identyfikator = MZ_Id;
            MZ_CzasRejestracji = DateTime.Now;
            MZ_ZmianyRozmiaru = 0;
            MZ_ZmianyPolozenia = 0;
        }

        public String getID()
        {
            return MZ_Identyfikator;
        }

        public void zmienRozmiar()
        {
            MZ_ZmianyRozmiaru++;
        }

        public void zmienPolozenie()
        {
            MZ_ZmianyPolozenia++;
        }

        public String getDate()
        {
            return MZ_CzasRejestracji.ToShortDateString();
        }

        public String getTime()
        {
            return MZ_CzasRejestracji.ToShortTimeString();
        }

        public int getZmianyRozmiaru()
        {
            return MZ_ZmianyRozmiaru;
        }

        public int getZmianyPolozenia()
        {
            return MZ_ZmianyPolozenia;
        }

        
    }
}