using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static void RegistracijaZahtev()
        {
            // prva opcija - kreiranje naloga na osnovu zahteva (korisnickoIme_zahtev.txt -> korisnickoIme.txt)
            // proverava se da li postoje zahtevi za kreiranje korisnickog naloga - tj. da li postoji u direktorijumu "nalozi"
            // jos neki fajlovi osim fajla "predsednik.txt"
            var fajlovi = Directory.GetFiles(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
            if (fajlovi.Length > 1)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Zahtevi za kreiranje korisnickog naloga:");
                foreach (var fajl in fajlovi)
                {
                    var imeFajla = Path.GetFileName(fajl);
                    if (imeFajla.CompareTo("predsednik.txt") == 0)
                        continue;
                    Console.WriteLine(imeFajla);
                }
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Unesite indekse datoteka naloga koje zelite da odobrite (brojeve odvojene zarezima):");
                var indeksi = new short[fajlovi.Length - 1];
                indeksi = Console.ReadLine().Split(',').Select(short.Parse).ToArray();
                var i = 0;
                var j = 0;
                foreach (var fajl in fajlovi)
                {
                    var imeFajla = Path.GetFileName(fajl);
                    if (imeFajla.CompareTo("predsednik.txt") == 0)
                        continue;
                    if (i == indeksi[j])
                    {

                        File.Move(fajl, fajl.Substring(0, fajl.Length - 11) + ".txt"); // korisnickoIme_zahtev.txt -> korisnickoIme.txt
                        j++; // prelazimo na sledeci indeks
                        if (j >= fajlovi.Length - 1)
                            break;
                    }
                    i++;
                }
            }
        }

        public static void RegistracijaRucno()
        {
            var stanar = new Stanar();
            stanar.UnosStanar();
            stanar.IspisDatoteka();
        }

        public static void Registracija()
        {
            Console.WriteLine("Izaberite neku od sledece dve opcije:");
            Console.WriteLine("1) kreiranje naloga na osnovu zahteva");
            Console.WriteLine("2) rucni unos podataka za registraciju stanara");
            var odgovor = Convert.ToInt16(Console.ReadLine());
            if (odgovor == 1)
                RegistracijaZahtev();
            else if (odgovor == 2)
                RegistracijaRucno();
            else
                throw new InvalidDataException("Nije unet nijedan od trazenih odgovora (1/2).");
        }

        public static void KreiranjeZapisnika()
        {
            Console.Write("Unesite naziv datoteke u koju zelite da sacuvate zapisnik: ");
            var imeFajla = Console.ReadLine();

            Console.WriteLine("Unesite tekst zapisnika:");
            var zapisnik = new List<string>();
            var i = 0;
            while (true)
            {
                zapisnik.Add(Console.ReadLine());
                if (string.IsNullOrEmpty(zapisnik[i]))
                    break;
                i++;
            }
            i = 0;
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("../../../zapisnici/" + imeFajla + ".txt"))
            {
                for(i = 0; i < zapisnik.Count; i++)
                {
                    file.WriteLine(zapisnik[i]);
                }
            }
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
            if (odgovor == 4) // registracija novog stanara
            {
                Registracija();
            }
            else if (odgovor == 5) // kreiranje zapisnika saveta
            {
                KreiranjeZapisnika();
            }
            return Ok; // suvisno, ali mora jer se ne prepoznaje da ce se do ovog dela izaci iz metode
        }

        public static int Prijava1() // korisnicka aplikacija - stanar zgrade
        {
            Stanar.Prijava1Opcije();
            Prijava1Opcije();
            return Prijava1Akcije();
        }

    }
}
