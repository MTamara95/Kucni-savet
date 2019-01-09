using System;
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

        public void Unos()
        {
            Console.Write("Adresa: ");
            Adresa = Console.ReadLine();
            Console.Write("Broj: ");
            Broj = Convert.ToInt16(Console.ReadLine());
            Console.Write("Opstina: ");
            Opstina = Console.ReadLine();
            Console.Write("Grad: ");
            Grad = Console.ReadLine();
            Console.Write("Broj stanova: ");
            BrojStanova = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Niz stanova u kojima zive stanari (uneti brojeve odvojene zarezom):");
            var tmp = new short[BrojStanova];
            tmp = Console.ReadLine().Split(',').Select(short.Parse).ToArray();
            NizStanova = new List<short>();
            for (int i = 0; i < BrojStanova; i++)
            {
                NizStanova.Add(tmp[i]);
            }
            
        }

    }
}
