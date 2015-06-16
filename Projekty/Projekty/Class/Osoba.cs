using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekty.Class
{
    public class Osoba
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public  Adres Adr { get; set; }
        public  Telefon Atel { get; set; }

        public virtual List<Telefon> Tel { get; set; }

        public Osoba(string imie, string nazwisko, Adres adr, Telefon tel)
        {
            Tel = new List<Telefon>(); 
            if ((imie.Length >= 3) && (nazwisko.Length >= 3) && (adr != null) && (tel != null))
            {
                Imie = imie;
                Nazwisko = nazwisko;
                Adr = adr;
                
                Tel.Add(tel);
            }
        }
        public Osoba()
        {
            Adr = new Adres();
            Tel = new List<Telefon>();
        }
        public void AddTelefon(Telefon tel)
        {
            if (tel != null)
            {
                Tel.Add(tel);
            }
        }
        public List<Telefon> GetTelList
        {
            get
            {
                return Tel;
            }
        }
    }
}
