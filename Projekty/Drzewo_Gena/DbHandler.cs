using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using System.Windows;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Constraints;

namespace Drzewo_Gena
{
    public class DbHandler
    {
        private IObjectContainer baza;
        private string filename = String.Empty;
        private IEmbeddedConfiguration db_config;

        public DbHandler()
        {
            filename = "DataBase.dbo";
            _Konekt();
        }

        private void _Konekt()
        {
            if (baza != null)
            {
                baza.Close();
            }
            if (baza == null)
            {
                db_config = Db4oEmbedded.NewConfiguration();

                db_config.Common.ObjectClass(typeof(Person)).ObjectField("<Imie>k__BackingField").Indexed(true);
                db_config.Common.Add(new UniqueFieldValueConstraint(typeof(Person), "<Imie>k__BackingField"));

                baza = Db4oEmbedded.OpenFile(db_config,filename);
            }
        }

        public bool AddOsoba(Person person)
        {
            bool temp = false;
            try
            {
                baza.Store(person.Children);
                baza.Store(person.Mother);
                baza.Store(person.Father);
                baza.Store(person);
                baza.Commit();
            }
            finally
            {
                temp = true;
            }
            return temp;
        }
        public List<Person> GetPersonList
        {
            get
            {
                return baza.Query<Person>().ToList();
            }
        }

        public virtual Person GetPersonFromName(string name)
        {
            return baza.Query<Person>().FirstOrDefault(x => x.Imie.Contains(name)) as Person;
        }

        public void UpdatePerson(Person data)
        {
            Person person = GetPersonFromName(data.Imie);
            if (person.Children.Count > 0)
            {
                foreach (Person item in person.Children)
                {
                    baza.Store(item);
                }
                baza.Store(person.Children);
            }
            baza.Store(person.Father);
            baza.Store(person.Mother);
            baza.Store(person);
            baza.Commit();
        }

        public void DeletePerson(Person data)
        {   
            Person person = GetPersonFromName(data.Imie);
            if (person.Children.Count > 0)
            {
                foreach (Person item in person.Children)
                {
                    baza.Delete(item);
                }
            }
            baza.Delete(person.Children);
            if (person.Mother != null)
            baza.Delete(person.Mother);
            if (person.Father != null)
            baza.Delete(person.Father);
            baza.Delete(person);
            baza.Commit();
        }

        public void _Close()
        {
            baza.Dispose();
            baza.Close();
        }

        public bool IsExist(string name)
        {
            if (baza.Query<Person>().Where(x => x.Imie.Contains(name)).Count() > 0) return true;
            else return false;
        }
    }
}
