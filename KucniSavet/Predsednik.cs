using System;
using System.IO;
using System.Text;

namespace KucniSavet
{
    class Predsednik : Stanar
    {
        public override void UnosGodinaRodjenja()
        {
            base.UnosGodinaRodjenja();
            if (DateTime.Now.Year - GodinaRodjenja < 18)
                throw new ArgumentException("Predsednik mora biti punoletan");
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

        public override void UnosNosilac()
        {
            base.UnosNosilac();
            if (!Nosilac)
                throw new ArgumentException("Predsednik mora biti nosilac stana");
        }

        public void UnosPredsednik()
        {
            UnosIme();
            UnosPrezime();
            while (true)
            {
                try
                {
                    UnosGodinaRodjenja();
                    break;
                }
                catch
                {
                    continue;
                }
            }
            UnosEmail();
            UnosKorisnickoIme();
            UnosLozinka();
            UnosBrojStana();
            while (true)
            {
                try
                {
                    UnosNosilac();
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }

        public void IspisDatoteka()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("../../../nalozi/predsednik.txt"))
            {
                file.WriteLine(Ime);
                file.WriteLine(Prezime);
                file.WriteLine(GodinaRodjenja);
                file.WriteLine(Email);
                file.WriteLine(KorisnickoIme);
                file.WriteLine(Lozinka);
                file.WriteLine(BrojStana);
                file.WriteLine(Nosilac);
            }
        }

        public static void InfoPredsednik()
        {
            Directory.CreateDirectory(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
            if (!File.Exists(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt") ||
                File.ReadAllText(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt").Length == 0)
            {
                Console.WriteLine("Unesite informacije o predsedniku kucnog saveta:");
                var predsednik = new Predsednik();
                predsednik.UnosPredsednik();
                predsednik.IspisDatoteka();
            }
        }

        public static void Prijava1Opcije()
        {
            Console.WriteLine("4. Registracija novog stanara");
            Console.WriteLine("5. Kreiranje zapisnika saveta");
        }

        public static int Prijava1Akcije()
        {
            var odgovor = Convert.ToInt16(Console.ReadLine());

            if (odgovor == 4) // registracija novog stanara
            {
                // ... return Registracija();
            }
            else if (odgovor == 5) // kreiranje zapisnika saveta
            {
                // return KreiranjeZapisnika();
            }
            return Ok; // suvisno, ali mora jer se ne prepoznaje da ce se do ovog dela izaci iz metode
        }

        public static int Prijava1() // korisnicka aplikacija - stanar zgrade
        {
            Stanar.Prijava1Opcije();
            Prijava1Opcije();
            if (Stanar.Prijava1Akcije() == ~Ok)
                return ~Ok;
            return Prijava1Akcije();
        }

    }
}
