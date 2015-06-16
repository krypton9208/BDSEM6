using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Translatory1.Class;

namespace Translatory1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            using (
                var sr =
                    new StreamReader(@"alg.txt")
                    )
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            var allines = sb.ToString();

            var AnalizaLeks = new Analizator();
            AnalizaLeks.DodajElement(new Element() { Nazwa = "white space", Regex = new Regex(@"^\s+") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "addidtion", Regex = new Regex(@"^\+") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "substraction", Regex = new Regex(@"^\-") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "multiplication", Regex = new Regex(@"^\*") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "division", Regex = new Regex(@"^\\") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "lp", Regex = new Regex(@"^\(") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "rp", Regex = new Regex(@"^\)") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "equal", Regex = new Regex(@"^\=") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "variable", Regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "float", Regex = new Regex(@"^\d+\.\d+") });
            AnalizaLeks.DodajElement(new Element() { Nazwa = "integer", Regex = new Regex(@"^\d+") });

            Console.WriteLine(allines);
            AnalizaLeks.Wejscie = allines;
            while(AnalizaLeks.Wejscie.Length>0)
            {
                var temp = AnalizaLeks.Next();
                if (temp == null) break;
                Console.WriteLine( temp.TypElementu.Nazwa + " " + temp.Dane + " pozycja: " + temp.Pozycja.ToString());
            }
        }
    }
}
