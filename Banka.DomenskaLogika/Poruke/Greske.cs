namespace Banka.DomenskaLogika.Poruke
{
    public static class Greske
    {
        #region
        public const string KORISNIK_POSTOJECE_KORISNICKOIME = "Korisnik sa istim Korisnickim imenom vec postoji.";
        public const string KORISNIK_GRESKA_PRI_UNOSU = "Desila se greska pri registrovanju novog korisnika, pokusajte ponovo.";
        #endregion

        #region
        public const string DEVIZNIRACUN_GRESKA_PRI_UNOSI = "Desila se greska pri kreiranju novog deviznog racuna, pokusajte ponovo.";
        public const string DEVIZNIRACUN_POSTOJECI_RACUN = "Korisnik vec ima devizni racun sa unetom valutom.";
        #endregion
    }
}
