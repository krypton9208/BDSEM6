using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Constraints;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekty.Class
{
    class DbHandler
    {
        private string filename = String.Empty;
       
        IObjectContainer db;


        public DbHandler()
        {

            filename = "DataBase.dbo";
            _Konekt();
        }

        private void _Konekt()
        {
            if (db != null)
            {
                db.Close();
            }

            if (db == null)
            {
              

                db = Db4oEmbedded.OpenFile( filename);
            }

        }

        public bool AddOsoba(Osoba person)
        {
            bool temp = false;
            try
            {
                db.Store(person);
            }
            finally
            {
                temp = true;
            }
            return temp;
        }

        public List<Osoba> getListItems
        {
            get
            {
                return db.Query<Osoba>().ToList();
            }
        }

        public void _Close()
        {
            db.Dispose();
            db.Close();
        }

        public bool Update(Osoba data)
        {
            foreach (var item in data.Tel)
            {
                db.Store(item);
            }
            db.Store(data.Tel);
            db.Store(data.Adr);
            db.Store(data);
            return true;
        }


        public bool Delete(Osoba data)
        {
            foreach (var item in data.Tel)
            {
                db.Delete(item);
            }
            db.Delete(data.Tel);
            db.Delete(data.Atel);
            db.Delete(data.Adr);
            db.Delete(data);
            return true;
        }

        public List<Osoba> Wyszukaj(string imie, string nazwisko)
        {
            return db.Query<Osoba>(x => x.Imie.Contains(imie) || x.Nazwisko.Contains(nazwisko)).ToList<Osoba>();
        }

        public List<Osoba> WyszukajDokladnie(string imie, string nazwisko)
        {
            return db.Query<Osoba>(x => x.Imie.Equals(imie) && x.Nazwisko.Equals(nazwisko)).ToList<Osoba>();
        }
    }
}
