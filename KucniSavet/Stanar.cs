using System;

namespace KucniSavet
{
    class Stanar
    {
        protected string Ime;
        protected string Prezime;
        protected short GodinaRodjenja;
        protected string Email;
        protected string KorisnickoIme;
        protected string Lozinka;
        protected short BrojStana;
        protected bool Nosilac;

        public void UnosIme()
        {
            Console.Write("Ime: ");
            Ime = Console.ReadLine();
        }

        public void UnosPrezime()
        {
            Console.Write("Prezime: ");
            Prezime = Console.ReadLine();
        }

        public virtual void UnosGodinaRodjenja()
        {
            Console.Write("Godina rodjenja: ");
            GodinaRodjenja = Convert.ToInt16(Console.ReadLine());
        }

        public void UnosEmail() { 
            Console.Write("E-mail adresa: ");
            Email = Console.ReadLine();
        }

        public void UnosKorisnickoIme()
        {
            Console.Write("Korisnicko ime: ");
            KorisnickoIme = Console.ReadLine();
        }

        public void UnosLozinka()
        {
            Console.Write("Lozinka: ");
            Lozinka = Console.ReadLine();
        }

        public void UnosBrojStana()
        {
            Console.Write("Broj stana: ");
            BrojStana = Convert.ToInt16(Console.ReadLine());
        }

        public virtual void UnosNosilac()
        {
            Console.Write("Da li je nosilac stana (da/ne): ");
            var tmp = Console.ReadLine();
            if (tmp.CompareTo("da") == 0)
                Nosilac = true;
            else
                Nosilac = false;
        }

        public void UnosStanar()
        {
            UnosIme();
            UnosPrezime();
            UnosGodinaRodjenja();
            UnosEmail();
            UnosKorisnickoIme();
            UnosLozinka();
            UnosBrojStana();
            UnosNosilac();
        }
    }
}
