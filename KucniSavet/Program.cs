using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KucniSavet
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite informacije o zgradi:");
            var zgrada = new Zgrada();
            zgrada.Unos();
        }
    }
}
