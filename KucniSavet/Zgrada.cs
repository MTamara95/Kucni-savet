using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace KucniSavet
{
    class Zgrada
    {
        public string Adresa { get; set; }
        public short Broj { get; set; }
        public string Opstina { get; set; }
        public string Grad { get; set; }
        public short BrojStanova { get; set; }
        public List<short> NizStanova { get; set; }

        public void UnosZgrada()
        {
            Console.Write("Adresa: ");
            Adresa = Console.ReadLine();
            Console.Write("Broj: ");
            try
            {
                Broj = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Nije unet broj.");
                throw;
            }
            Console.Write("Opstina: ");
            Opstina = Console.ReadLine();
            Console.Write("Grad: ");
            Grad = Console.ReadLine();
            Console.Write("Broj stanova: ");
            try
            {
                BrojStanova = Convert.ToInt16(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Nije unet broj.");
                throw;
            }
            Console.WriteLine("Niz stanova u kojima zive stanari (uneti brojeve odvojene zarezom):");
            var tmp = new short[BrojStanova];
            try
            {
                tmp = Console.ReadLine().Split(',').Select(short.Parse).ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine("Nije unet niz brojeva.");
                throw;
            }
            NizStanova = new List<short>();
            for (int i = 0; i < BrojStanova; i++)
            {
                NizStanova.Add(tmp[i]);
            }
        }

        public void IspisDatoteka()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("../../../config/zgrada.txt"))
            {
                file.WriteLine(Adresa);
                file.WriteLine(Broj);
                file.WriteLine(Opstina);
                file.WriteLine(Grad);
                file.WriteLine(BrojStanova);
                foreach (var stan in NizStanova)
                {
                    file.Write(stan + " ");
                }
                file.WriteLine();
            }
        }

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

    }
}
