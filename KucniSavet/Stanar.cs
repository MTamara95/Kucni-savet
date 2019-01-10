using System;
using System.IO;
using System.Text;

namespace KucniSavet
{
    class Stanar
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public short GodinaRodjenja { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public short BrojStana { get; set; }
        public bool Nosilac { get; set; }

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
