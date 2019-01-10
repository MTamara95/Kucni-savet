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
        static void Main(string[] args)
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

            Directory.CreateDirectory(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi");
            if (!File.Exists(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt") ||
                File.ReadAllText(@"D:\1.1 C# Udemy\C# projects 2\KucniSavet\nalozi\predsednik.txt").Length == 0)
            {
                Console.WriteLine("Unesite informacije o predsedniku kucnog saveta:");
                var predsednik = new Predsednik();
                predsednik.UnosPredsednik();
                predsednik.IspisDatoteka();
            }

            Console.WriteLine("Izaberite neku od sledece dve opcije:");
            Console.WriteLine("1) Prijava na sistem");
            Console.WriteLine("2) Ugasi aplikaciju");
            var opcija = Convert.ToInt16(Console.ReadLine());
            if (opcija == 2) // gasimo aplikaciju
                return;

            // prijava na sistem - stanari zgrade / predsednik saveta stanara
            Console.Write("Unesite korisnicko ime: ");
            var tmpKorisnickoIme = Console.ReadLine();
            Console.Write("Unesite lozinku: ");
            var tmpLozinka = Console.ReadLine();
            
            // proveravamo da li postoji korisnik - predsednik / stanar
        }
    }
}
