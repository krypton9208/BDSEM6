using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Drzewo_Gena
{
    /// <summary>
    /// Interaction logic for UpdatePerson.xaml
    /// </summary>
    public partial class UpdatePerson : Window
    {
        private readonly DbHandler DbHandler;
        Person ActualPerson { get; set; }
        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }
        public void LoadPerson(Person person)
        {
            ImieText.Text = person.Imie;

            BirdDateText.SelectedDate = person.BirthDate;
            DeathDateText.SelectedDate = person.DeathDate;
            ComboGender.SelectedItem = (Gender)person.Gender;
            if (person.Father != null)
            {
                ComboOjcowie.SelectedItem = person.Father.Imie;
            }
            if (person.Mother != null)
            {
                ComboMatki.SelectedItem = person.Mother.Imie;
            }

        }
        public UpdatePerson(DbHandler Db, Person person)
        {
            ActualPerson = person;
            InitializeComponent();
            DbHandler = Db;
            foreach (var item in DbHandler.GetPersonList)
            {
                if (item.Gender == Gender.male && item.CanBeFather(person)) ComboOjcowie.Items.Add(item.Imie);
                if (item.Gender == Gender.female && item.CanBeMother(person)) ComboMatki.Items.Add(item.Imie);
            }
            LoadPerson(ActualPerson);

            DoEvents();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person person = DbHandler.GetPersonFromName(ImieText.Text);
            person.BirthDate = BirdDateText.SelectedDate.GetValueOrDefault();
            person.DeathDate = DeathDateText.SelectedDate.GetValueOrDefault();
            person.Gender = (Gender)ComboGender.SelectedItem;
            if (ComboOjcowie.SelectedItem != null)
            {
                Person tata = DbHandler.GetPersonFromName(ComboOjcowie.SelectedItem.ToString());
                if (tata.CanBeFather(person))
                {

                    person.AddFather(tata);
                    DbHandler.UpdatePerson(tata);
                }
            }
            else
            {
                if (person.Father != null)
                {
                    if (person.Father.Children.Where(x => x.Imie.Contains(person.Imie)).Count() > 0)
                        person.Father.Children.Remove(person);
                    DbHandler.UpdatePerson(person.Father);
                    person.AddFather(null);
                }
            }
            if (ComboMatki.SelectedItem != null)
            {
                Person mama = DbHandler.GetPersonFromName(ComboMatki.SelectedItem.ToString());
                if (mama.CanBeMother(person))
                {
                    person.AddMother(mama);
                    DbHandler.UpdatePerson(mama);
                }

            }
            else
            {
                if (person.Mother != null)
                {
                    if (person.Mother.Children.Where(x => x.Imie.Contains(person.Imie)).Count() > 0)
                        person.Mother.Children.Remove(person);
                    DbHandler.UpdatePerson(person.Mother);
                    person.AddMother(null);
                }
                
            }
            DbHandler.UpdatePerson(person);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPerson(ActualPerson);

        }


    }
}
