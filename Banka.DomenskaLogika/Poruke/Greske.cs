namespace Banka.DomenskaLogika.Poruke
{
    public static class Greske
    {
        #region
        public const string KORISNIK_POSTOJECE_KORISNICKOIME = "Korisnik sa istim Korisnickim imenom vec postoji.";
        public const string KORISNIK_GRESKA_PRI_UNOSU = "Desila se greska pri registrovanju novog korisnika, pokusajte ponovo.";
        public const string KORISNIK_NEPOSTOJECI_ID = "Trazeni korisnik ne postoji.";
        public const string KORISNIK_GRESKA_PRI_IZMENI = "Desila se greska pri izmeni podataka, pokusajte ponovo.";
        #endregion

        #region
        public const string DEVIZNIRACUN_GRESKA_PRI_UNOSI = "Desila se greska pri kreiranju novog deviznog racuna, pokusajte ponovo.";
        public const string DEVIZNIRACUN_POSTOJECI_RACUN = "Korisnik vec ima devizni racun sa unetom valutom.";
        public const string DEVIZNIRACUN_NEPOSTOJECI_RACUN = "Devizni Racun nije pronadjen.";
        public const string DEVIZNIRACUN_NEDOVOLJNO_SREDSTAVA = "Na Vasem racunu nemate dovoljno sredstava da izvrsite ovo placanje.";
        public const string DEVIZNIRACUN_GRESKA_PRI_SKIDANJU_SREDSTAVA = "Doslo je do greske pri skidanju sredstava sa racuna.";
        #endregion

        #region
        public const string DINARSKI_RACUN_NEDOVOLJNO_SREDSTAVA = "Na Vasem racunu nemate dovoljno sredstava da izvrsite ovo placanje.";
        public const string DINARSKI_RACUN_NEPOSTOJECI = "Dinarski racun nije pronadjen.";
        public const string DINARSKI_RACUN_GRESKA_PRI_ODUZIMANJU_SREDSTAVA = "Doslo je do greske pri skidanju sredstava sa dinarskog racuna.";
        #endregion

        #region
        public const string VALUTA_POGRESAN_NAZIV_VALUTE = "Greska pri unosu naziva valute, naziv valuta mora imati tacno 3 slova.";
        public const string VALUTA_POSTOJECI_NAZIV_VALUTE = "Valuta sa unetim nazivom vec postoji.";
        public const string VALUTA_GRESKA_PRI_KREIRANJU = "Doslo je do greske pri unosu nove valute, pokusajte ponovo.";
        #endregion

        #region
        public const string DINARSKO_PLACANJE_GRESKA_PRI_UNOSU = "Doslo je do greske pri placanju, pokusajte ponovo.";
        #endregion

        #region
        public const string DEVIZNOPLACANJE_GRESKA_PRI_UNOSU = "Doslo je do greske pri placanju, pokusajte ponovo.";
        #endregion
    }
}
