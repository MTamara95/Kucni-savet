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
        protected const short Ok = 1;

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
            try
            {
                GodinaRodjenja = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Nije unet broj.");
                throw;
            }
        }

        public void UnosEmail() { 
            Console.Write("E-mail adresa: ");
            Email = Console.ReadLine();
        }

        public void UnosKorisnickoIme(string tmpKorisnickoIme)
        {
            KorisnickoIme = tmpKorisnickoIme;
        }

        public void UnosLozinka(string tmpLozinka)
        {
            Lozinka = tmpLozinka;
        }

        public void UnosBrojStana()
        {
            Console.Write("Broj stana: ");
            try
            {
                BrojStana = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Nije unet broj.");
                throw;
            }
        }

        public virtual void UnosNosilac()
        {
            Console.Write("Da li je nosilac stana (da/ne): ");
            var tmp = Console.ReadLine();
            if (tmp.CompareTo("da") == 0)
                Nosilac = true;
            else if (tmp.CompareTo("ne") == 0)
                Nosilac = false;
            else
                throw new Exception("Unet odgovor je razlicit od trazenog (da/ne)");
        }

        public void UnosStanar(string tmpKorisnickoIme, string tmpLozinka) 
        {
            UnosIme();
            UnosPrezime();
            UnosGodinaRodjenja();
            UnosEmail();
            UnosKorisnickoIme(tmpKorisnickoIme);
            UnosLozinka(tmpLozinka);
            UnosBrojStana();
            UnosNosilac();
        }

        public void IspisDatoteka(string tmpKorisnickoIme)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("../../../nalozi/" + tmpKorisnickoIme + "_zahtev.txt"))
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

        public static int Zapisnik() // odgovor 1 - zapisnik sa saveta stanara
        {
            // pojavljuje se lista sa nazivima datoteka zapisnika sa saveta stanara:
            var fajloviZapisnik = Directory.GetFiles(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\zapisnici");
            var i = 0;
            foreach (var fajl in fajloviZapisnik)
            {
                Console.WriteLine("{0}. {1}", i, Path.GetFileName(fajl));
                i++;
            }
            Console.WriteLine("...");
            Console.WriteLine("Koju datoteku zelite da otvorite?");
            var odgDatoteka = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("---------------------------------");
            i = 0;
            foreach (var fajl in fajloviZapisnik)
            {
                if (odgDatoteka == i)
                {
                    // ispisuje se sadrzaj zeljene datoteke
                    Console.WriteLine(File.ReadAllText(fajl));
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("Da li zelite da se vratite na pocetni meni (da/ne)?");
                    var odg = Console.ReadLine();
                    if (odg.CompareTo("da") == 0)
                        return ~Ok;
                    else if (odg.CompareTo("ne") != 0)
                        throw new Exception("Unet odgovor je razlicit od trazenog (da/ne)");
                    break;
                }
                i++;
            }
            return Ok;
        }

        public static int Informacije() // odgovor 2 - informacije o stanarima u zgradi
        {
            // ispis svih stanara koji zive u njegovoj zgradi
            var fajloviZgrada = Directory.GetFiles(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
            foreach (var fajl in fajloviZgrada)
            {
                var imeFajla = Path.GetFileName(fajl);
                if (imeFajla.CompareTo("predsednik.txt") != 0) // naziv tog fajla ne sadrzi korisnicko ime stanara
                    Console.WriteLine(imeFajla.Split('_')[0]); // ispisuje se samo korisicko ime
            }
            Console.WriteLine("Da li zelite da se vratite na pocetni meni (da/ne)?");
            var odg = Console.ReadLine();
            if (odg.CompareTo("da") == 0)
                return ~Ok;
            else if (odg.CompareTo("ne") != 0)
                throw new Exception("Unet odgovor je razlicit od trazenog (da/ne)");
            return Ok;
        }

        public static int Odjava() // odgovor 3 - odjava
        {
            return ~Ok; // vratice korisnika na pocetni meni...
        }

        public static void Prijava1Opcije()
        {
            Console.WriteLine("1. Zapisnik sa saveta stanara");
            Console.WriteLine("2. Informacije o stanarima u zgradi");
            Console.WriteLine("3. Odjava");
        }

        public static int Prijava1Akcije()
        {
            var odgovor = Convert.ToInt16(Console.ReadLine());

            if (odgovor == 1) // zapisnik sa saveta stanara...
            {
                return Zapisnik();
            }
            else if (odgovor == 2) // informacije o stanarima u zgradi
            {
                return Informacije();
            }
            else if (odgovor == 3) // odjava
            {
                return Odjava();
            }
            return Ok; // suvisno, ali mora jer se ne prepoznaje da ce se do ovog dela izaci iz metode
        }

        public static int Prijava1() // korisnicka aplikacija - stanar zgrade
        {
            Prijava1Opcije();
            return Prijava1Akcije();
        }
    }
}
