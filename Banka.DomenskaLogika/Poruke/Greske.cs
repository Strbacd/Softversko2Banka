namespace Banka.DomenskaLogika.Poruke
{
    public static class Greske
    {
        #region
        public const string KORISNIK_POSTOJECE_KORISNICKOIME = "Korisnik sa istim Korisnickim imenom vec postoji.";
        public const string KORISNIK_GRESKA_PRI_UNOSU = "Desila se greska pri registrovanju novog korisnika, pokusajte ponovo.";
        public const string KORISNIK_NEPOSTOJECI_ID = "Trazeni korisnik ne postoji.";
        public const string KORISNIK_GRESKA_PRI_IZMENI = "Desila se greska pri izmeni podataka, pokusajte ponovo.";
        public const string KORISNIK_POGRESNO_KORISNICKO_IME = "Korisnicko ime mora biti izmedju 4-20 karaktera. ";
        public const string KORISNIK_POGRESNO_IMEPREZIME = "Ime i prezime ne moze biti duze od 50 karaktera. ";
        public const string KORISNIK_ADRESA = "Adresa mora biti do 100 karaktera.";
        #endregion

        #region
        public const string RACUN_GRESKA_PRI_UNOSI = "Desila se greska pri kreiranju novog racuna, pokusajte ponovo.";
        public const string RACUN_POSTOJECI_RACUN = "Korisnik vec ima devizni racun sa unetom valutom.";
        public const string RACUN_NEPOSTOJECI_RACUN = "Devizni Racun nije pronadjen.";
        public const string RACUN_NEDOVOLJNO_SREDSTAVA = "Na Vasem racunu nemate dovoljno sredstava da izvrsite ovo placanje.";
        public const string RACUN_GRESKA_PRI_SKIDANJU_SREDSTAVA = "Doslo je do greske pri skidanju sredstava sa racuna.";
        #endregion

        #region
        public const string VALUTA_POGRESAN_NAZIV_VALUTE = "Greska pri unosu naziva valute, naziv valuta mora imati tacno 3 slova.";
        public const string VALUTA_POSTOJECI_NAZIV_VALUTE = "Valuta sa unetim nazivom vec postoji.";
        public const string VALUTA_GRESKA_PRI_KREIRANJU = "Doslo je do greske pri unosu nove valute, pokusajte ponovo.";
        public const string VALUTA_NEPOSTOJECI_ID = "Izabrana valuta ne postoji.";
        #endregion

        #region
        public const string DINARSKO_PLACANJE_GRESKA_PRI_UNOSU = "Doslo je do greske pri placanju, pokusajte ponovo.";
        public const string DINARSKO_PLACANJE_POGRESAN_RACUNID = "Trazeni racun ne postoji. ";
        public const string DINARSKO_PLACANJE_POGRESAN_ID = "Trazeno Dinarsko placanje ne postoji. ";
        public const string DINARSKO_PLACANJE_POGRESAN_BROJRACUNA = "Broj racuna mora biti 13 cifara. ";
        #endregion

        #region
        public const string DEVIZNOPLACANJE_GRESKA_PRI_UNOSU = "Doslo je do greske pri placanju, pokusajte ponovo.";
        public const string DEVIZNOPLACANJE_POGRESAN_RACUNID = "Trazeni racun ne postoji.";
        public const string DEVIZNOPLACANJE_POGRESAN_ID = "Trazeno Dinarsko placanje ne postoji. ";
        public const string DEVIZNOPLACANJE_POGRESAN_BROJRACUNA = "Broj racuna mora biti 13 cifara. ";
        #endregion
    }
}
