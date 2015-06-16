using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekty.Class
{
    public class Adres
    {
        public string Miejscowość { get; set; }
        public string Ulica { get; set; }
        public string Kod_Pocztowy { get; set; }
        public string Województwo { get; set; }
        public string Kraj { get; set; }
        public Adres(string miejsc, string ul, string kod, string woj, string kraj)
        {
            if ((miejsc != string.Empty) && (ul != string.Empty) && (kod.Length == 6 ) && (woj != string.Empty) && (kraj != string.Empty))
            {
                Miejscowość = miejsc;
                Ulica = ul;
                Kod_Pocztowy = kod;
                Województwo = woj;
                Kraj = kraj;
            }
        }
        public Adres()
        {

        }


    }
}
