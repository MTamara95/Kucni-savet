using System;
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
            var indStanar = 0;
            var indPredsednik = 0;
            var indSifra = 1; // lozinku proveravamo samo ako nalog vec postoji
            // 1 - predsednik (u fajlu predsednik.txt 5. linija je korisnicko ime, a 6. linija je sifra)
            var sadrzajPredsednik = File.ReadAllLines(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt");
            if (tmpKorisnickoIme.CompareTo(sadrzajPredsednik[4]) == 0)
            {
                indPredsednik = 1; // uneti korisnik je predsednik
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
                    indStanar = 1; // uneti korisnik je stanar
                    // proveravamo lozinku u okviru tog fajla - 6. linija
                    var sadrzajStanar = File.ReadAllLines(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\" + tmpKorisnickoIme + "_zahtev.txt");
                    if (tmpLozinka.CompareTo(sadrzajStanar[5]) != 0)
                        indSifra = 0;
                    break;
                }
            }

            // ako nalog jos uvek nije kreiran - ispisuje se odgovarajuca poruka, i nudi se korisniku da
            // posalje zahtev za kreiranje naloga predsedniku
            if (indStanar == 0 && indPredsednik == 0) // uneti korisnik nije ni stanar ni predsednik
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

            if(indStanar == 1) // stanar se prijavio na sistem
            {
                Console.WriteLine("1. Zapisnik sa saveta stanara");
                Console.WriteLine("2. Informacije o stanarima u zgradi");
                Console.WriteLine("3. Odjava");
                var odgovor = Convert.ToInt16(Console.ReadLine());

                if (odgovor == 1) // zapisnik sa saveta stanara...
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
                        if(odgDatoteka == i)
                        {
                            // ispisuje se sadrzaj zeljene datoteke
                            Console.WriteLine(File.ReadAllText(fajl));
                            Console.WriteLine("---------------------------------");
                            Console.WriteLine("Da li zelite da se vratite na pocetni meni (da/ne)?");
                            var odg = Console.ReadLine();
                            if (odg.CompareTo("da") == 0)
                                return ~Ok;
                            break;
                        }
                        i++;
                    }
                }
                else if (odgovor == 2) // informacije o stanarima u zgradi
                {
                    // ispis svih stanara koji zive u njegovoj zgradi
                    var fajloviZgrada = Directory.GetFiles(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
                    foreach (var fajl in fajloviZgrada)
                    {
                        var imeFajla = Path.GetFileName(fajl);
                        if(imeFajla.CompareTo("predsednik.txt") != 0) // naziv tog fajla ne sadrzi korisnicko ime stanara
                            Console.WriteLine(imeFajla.Split('_')[0]); // ispisuje se samo korisicko ime
                    }
                    Console.WriteLine("Da li zelite da se vratite na pocetni meni (da/ne)?");
                    var odg = Console.ReadLine();
                    if (odg.CompareTo("da") == 0)
                        return ~Ok;
                }
                else if (odgovor == 3) // odjava
                {
                    return ~Ok; // vratice korisnika na pocetni meni...
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
