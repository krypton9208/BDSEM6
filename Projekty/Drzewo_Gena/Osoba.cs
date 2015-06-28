using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drzewo_Gena
{
    public enum Gender { male, female }
    public class Person
    {
        public string Imie { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public Gender Gender { get; set; }
        public virtual Person Father { get; set; }
        public virtual Person Mother { get; set; }
        public virtual List<Person> Children { get; set; }

        public Person()
        {
            Children = new List<Person>();
            DeathDate = DateTime.MinValue;
            Imie = string.Empty;
        }

        public bool AddFather(Person newfather)
        {
            if (newfather != null)
            {
                if (newfather.Gender == Drzewo_Gena.Gender.male && newfather.CanBeFather(this))
                {
                    Father = newfather;
                    if (newfather.Children.Where<Person>(x=>x == this).Count() == 0)
                        newfather.AddChild(this);
                    return true;
                }
                return false;
            }
            Father = null;
            return false;
        }

        public List<Person> RemovePerson()
        {
            List<Person> temp = new List<Person>();
            if (Father != null)
            {
                temp.Add(Father);
                Father.Children.Remove(this);
                if (Father.Children.Count == 0) Father.Children = new List<Person>();
                Father = null;
            }
            if (Mother != null)
            {
                temp.Add(Mother);
                Mother.Children.Remove(this);
                if (Mother.Children.Count == 0) Mother.Children = new List<Person>();
                Mother = null;
            }
            if (Children.Count > 0 )
            {
                foreach (var item in Children)
                {
                    temp.Add(item); 
                    if (Gender == Gender.male)
                        item.Father = null;
                    if (Gender == Gender.female)
                        item.Mother = null;
                }
            }
            Children = new List<Person>();
            return temp;
        }
        public bool AddMother(Person newmother)
        {
            if (newmother != null)
            {
                if (newmother.Gender == Drzewo_Gena.Gender.female && newmother.CanBeMother(this))
                {
                    Mother = newmother;
                    if (newmother.Children.Where<Person>(x=>x == this).Count() == 0)
                        newmother.AddChild(this);
                    return true;
                }
                return false;
            }
            Mother = null;
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



        public bool CanBeFather(Person child)
        {
            if (DeathDate == DateTime.MinValue && GetAge >= 12 && GetAge <= 70) return true;
            if (GetAge >= 12 && GetAge <= 70 && (child.BirthDate - DeathDate).TotalDays <= 270)
                return true;
            else return false;
        }
        public bool CanBeMother(Person child)
        {
            if (DeathDate == DateTime.MinValue && GetAge >= 12 && GetAge <= 70) return true;
            if (GetAge >= 10 && GetAge <= 60 && (child.BirthDate - DeathDate).TotalDays == 0)
                return true;
            else return false;
        }

        public void AddChild(Person person)
        {
            Children.Add(person);
        }

        public List<Person> GetSiblings
        {

            get
            {
                List<Person> temp = new List<Person>();
                if (Father.Children.Count > 1)
                {
                    foreach (Person item in Father.Children)
                    {
                        if (item != this)
                            temp.Add(item);
                    }
                }
                if (Mother.Children.Count > 1)
                {
                    foreach (Person item in Mother.Children)
                    {
                        if (item != this) temp.Add(item);
                    }
                }
                return temp;
            }

        }
    }
}