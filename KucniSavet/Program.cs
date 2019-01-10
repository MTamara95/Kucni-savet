﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json.Net;

namespace KucniSavet
{
    class Program
    {
        public const int Ok = 1;

        public static void InfoZgrada()
        {
            Directory.CreateDirectory(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\config");
            if (!File.Exists(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\config\zgrada.txt") ||
                File.ReadAllText(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\config\zgrada.txt").Length == 0) // unosimo podatke samo prvi put
            {
                Console.WriteLine("Unesite informacije o zgradi:");
                var zgrada = new Zgrada();
                zgrada.UnosZgrada();
                zgrada.IspisDatoteka();
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

        public static int PocetniMeni()
        {
            Console.WriteLine("Izaberite neku od sledece dve opcije:");
            Console.WriteLine("1) Prijava na sistem");
            Console.WriteLine("2) Ugasi aplikaciju");
            var opcija = Convert.ToInt16(Console.ReadLine());
            return opcija;
        }

        public static int Prijava()
        {

            // prijava na sistem - stanari zgrade / predsednik saveta stanara
            Console.Write("Unesite korisnicko ime: ");
            var tmpKorisnickoIme = Console.ReadLine();
            Console.Write("Unesite lozinku: ");
            var tmpLozinka = Console.ReadLine();

            // proveravamo da li postoji korisnik - predsednik / stanar
            var ind = 0;
            var indSifra = 1; // lozinku proveravamo samo ako nalog vec postoji
            // 1 - predsednik (u fajlu predsednik.txt 5. linija je korisnicko ime, a 6. linija je sifra)
            var sadrzajPredsednik = File.ReadAllLines(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt");
            if (tmpKorisnickoIme.CompareTo(sadrzajPredsednik[4]) == 0)
            {
                ind = 1; // uneti korisnik je predsednik
                if (tmpLozinka.CompareTo(sadrzajPredsednik[5]) != 0) // lozinka nije ispravna
                    indSifra = 0;
            }
            // 2 - stanar (u folderu nalozi datoteke sa informacijama o stanarima su oblika korisnickoime_zahtev.json")
            var fajlovi = Directory.GetFiles(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
            foreach (var fajl in fajlovi)
            {
                var parcijalniNaziv = Path.GetFileName(fajl).Split('_')[0]; // naziv fajla bez "_zahtev.txt"
                if (tmpKorisnickoIme.CompareTo(parcijalniNaziv) == 0)
                {
                    ind = 1;
                    // proveravamo lozinku u okviru tog fajla - 6. linija
                    var sadrzajStanar = File.ReadAllLines(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\" + tmpKorisnickoIme + "_zahtev.txt");
                    if (tmpLozinka.CompareTo(sadrzajStanar[5]) != 0)
                        indSifra = 0;
                    break;
                }
            }

            // ako nalog jos uvek nije kreiran - ispisuje se odgovarajuca poruka, i nudi se korisniku da
            // posalje zahtev za kreiranje naloga predsedniku
            if (ind == 0)
            {
                Console.WriteLine("Nalog Vam jos uvek nije kreiran.");
                Console.WriteLine("Da li zelite da posaljete zahtev za kreiranje naloga predsedniku? (da/ne)");
                var odgovor = Console.ReadLine();
                if (odgovor.CompareTo("da") == 0)
                {
                    var stanar = new Stanar();
                    stanar.UnosStanar(tmpKorisnickoIme, tmpLozinka);
                    stanar.IspisDatoteka(tmpKorisnickoIme);
                }
            }
            else // nalog vec postoji, ispitujemo validnost lozinke
            {
                if (indSifra == 0)
                {
                    Console.WriteLine("Lozinka nije validna.");
                    Console.WriteLine("Da li zelite da se vratite na pocetni meni (da/ne)?");
                    var odgovor = Console.ReadLine();
                    if (odgovor.CompareTo("da") == 0)
                        return ~Ok;
                }
            }

            return Ok;
        }

        static void Main(string[] args)
        {

            InfoZgrada();
            InfoPredsednik();

            while (true)
            {
                if (PocetniMeni() != Ok) // gasimo aplikaciju
                    return;
                else
                {
                    if (Prijava() != Ok) // korisnik moze da se vrati na pocetni meni, ukoliko ne moze da se prijavi na sistem
                        continue;
                    else
                        break;
                }
            }
        }
    }
}
