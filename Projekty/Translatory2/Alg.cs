using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translatory1.Class;

namespace Translatory2
{
    public class Alg
    {
        private Analizator _lekser;
        private Element _ElementOczekiwany;

        public Alg(Analizator anal)
        {
            _lekser = anal;
            CurrentElement();
        }

        public void Start()
        {
            Debug.Print("Start");
            W();
        }
        public void W()
        {
            Debug.Print("W");
            S();
            X();
        }

        public void S()
        {
            Debug.Print("S");
            C();
            Y();
        }
        public void Y()
        {
            while(true)
            {
                Debug.Print("Y");
                if (_ElementOczekiwany.Nazwa == "multiplication" || _ElementOczekiwany.Nazwa == "division")
                {
                    CheckCurrent(_ElementOczekiwany);
                    C();
                    continue;
                }
                break;
            }
        }

        public void C()
        {
            Debug.Print("C");
            if (_ElementOczekiwany.Nazwa == "integer" ||
                _ElementOczekiwany.Nazwa == "float" ||
                _ElementOczekiwany.Nazwa == "variable")
                CheckCurrent(_ElementOczekiwany);
            else if (_ElementOczekiwany.Nazwa == "lp")
            {
                CheckCurrent(_ElementOczekiwany);
                W();
                CheckCurrent(_lekser.Wyniki.First(x => x.Nazwa == "rp"));

            }
            else
                throw new Exception("Error " + _ElementOczekiwany.Nazwa);
        }

        

        public void X()
        {
            while(true)
            {
                Debug.Print("X");
                if (_ElementOczekiwany.Nazwa != "addidtion" &&
                    _ElementOczekiwany.Nazwa != "substraction") return;
                CheckCurrent(_ElementOczekiwany);
                S();
            }
        }

        private void CheckCurrent(Element type)
        {
            if (_ElementOczekiwany == type)
                CurrentElement();
            else
                throw new Exception("Error " + type.Nazwa);
        }

        public void CurrentElement()
        {
            Debug.Print("CurrentElement");
            var temp = _lekser.Next();
            if (temp == null)
                throw new Exception("end");
            _ElementOczekiwany = temp.TypElementu;

        }
    }
}
