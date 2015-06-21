using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drzewo_Gena
{
    public enum Gender { male, female }
    class Osoba
    {
        public string Imie { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
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
            if (Father == null)
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

      

        public bool CanBeFather(DateTime birth)
        {
                if (GetAge >= 12 && GetAge <= 70 && (birth - DeathDate).TotalDays < 270)
                    return true;
                else return false;
           
        }
        public bool CanBeMother(DateTime birth)
        {
             if (GetAge >= 10 && GetAge <= 60 && (birth - DeathDate).TotalDays == 0)
                    return true;
                else return false;
            
        }
        public bool NewChild(string imie, DateTime dataur, Osoba parent, Gender gend)
        {
            if (this.Gender == Gender.male && parent.Gender == Gender.female && this.CanBeFather(dataur) && parent.CanBeMother(dataur))
            {
                Children.Add(new Osoba { Imie = imie, BirthDate = dataur, Father = this, Mother = parent, Gender = gend });
                return true;

            }
            else if (this.Gender == Gender.female && parent.Gender == Gender.male && this.CanBeMother(dataur) && parent.CanBeFather(dataur))
            {
                Children.Add(new Osoba { Imie = imie, BirthDate = dataur, Mother = this, Father = parent, Gender = gend });
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}