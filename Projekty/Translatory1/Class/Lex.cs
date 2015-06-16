using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Translatory1.Class
{
    public class Element
    {
        public Regex Regex { get; set; }
        public string Nazwa { get; set; }
    }

    public class Wynik
    {
        public Element TypElementu { get; set; }
        public object Dane { get; set; }
        public int Pozycja { get; set; }
    }

    public class Analizator
    {
        private readonly List<Element> _ListaElementow;
        private readonly List<Wynik> _ListaWynikow;
        public string Wejscie { get; set; }
        public int AktualnaPozycja { get; set; }

        public Analizator()
        {
            _ListaElementow  = new List<Element>();
            _ListaWynikow = new List<Wynik>();
            AktualnaPozycja = 0;
        }

        public void DodajElement(Element el)
        {
            _ListaElementow.Add(el);
        }

        public List<Wynik> Wyniki
        {
            get
            {
                return _ListaWynikow;
            }
        }

        public Wynik Next()
        {
            var zmiana = false;
            Wynik wynik = null;
            foreach (var el in _ListaElementow)
            {
                Match match = el.Regex.Match(Wejscie);
                if (match.Success)
                { 

                    wynik = new Wynik() { Dane = match.Value, TypElementu = el, Pozycja = AktualnaPozycja };
                    Wejscie = el.Regex.Split(Wejscie).Last();
                    zmiana = true;
                    AktualnaPozycja += match.Length;
                    break;
                }
            }
            if (string.IsNullOrEmpty(Wejscie)) return null;
            else if (zmiana == false) throw new Exception("Blad skladny na pozycji: " + AktualnaPozycja + ".  " + Wejscie);
            else if (wynik.TypElementu.Nazwa == "white space")
                return Next();
            return wynik;
        }
    }
}
