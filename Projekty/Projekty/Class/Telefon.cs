using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekty.Class
{
    public class Telefon
    {
        public int Numer{get;set;}
        public enum Operatorzy { Play, Plus, Orange, T_Mobile }
        public enum TypTelefonu { Stacjonarny,Komorka, Komorka2, Praca,Gabinet }
        public Operatorzy Operator {get;set;}
        
        public TypTelefonu Rodzaj {get;set;}


        public Telefon( string numer, Operatorzy oper, TypTelefonu typ)
        {
            int n = 0;
            if (WalidacjaNumeru(numer, typ, out n))
            {
                Numer = n;
                Operator = oper;
                Rodzaj = typ;
            }
        }
        private bool WalidacjaNumeru(string x, TypTelefonu k, out int t)
        {

            t = Convert.ToInt32(x);
            if ((x.Length != 8) && (k == TypTelefonu.Stacjonarny)) return false;
            else if ((x.Length != 9) && ((k == TypTelefonu.Komorka) || (k == TypTelefonu.Komorka2))) return false;
            else if ((x.Length == 9) && ((k == TypTelefonu.Gabinet) || (k==TypTelefonu.Praca))) return true;
            else return true;
        }
        public Telefon()
        {

        }

    }

}
