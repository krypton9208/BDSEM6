﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drzewo_Gena
{
    public enum Gender { male, female }
    class Osoba
    {
        public  string Imie { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate{ get; set; }
        public Gender Gender { get; set; }
        public Osoba Father { get; set; }
        public Osoba Mother { get; set; }
        public List<Osoba> Children { get; set; }

        public Osoba()
        {
            Children = new List<Osoba>();
            DeathDate = DateTime.MinValue;
        }

        public bool AddFather(Osoba father)
        {
            if (Father==null)
            {
                if (father.Gender == Drzewo_Gena.Gender.male)
                {
                    Father = father;
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool AddMother(Osoba mother)
        {
            if (Mother == null)
            {
                if (mother.Gender == Drzewo_Gena.Gender.female)
                {
                    Mother = mother;
                    return true;
                }
                return false;
            }
            return false;
        }

        public int GetAge
        {
            get
            {
                if (DeathDate == DateTime.MinValue)
                    return DateTime.Now.Year - BirthDate.Year;
                else
                    return DeathDate.Year - BirthDate.Year;
            }
        }
    }
}
