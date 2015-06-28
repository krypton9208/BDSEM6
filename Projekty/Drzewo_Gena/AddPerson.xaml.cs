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

namespace Drzewo_Gena
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        private readonly DbHandler DbHandler;
        public List<Person> Ojcowie { get; set; }
        public List<Person> Matki { get; set; }
        public AddPerson(DbHandler db, List<Person> a)
        {
            InitializeComponent();
            Ojcowie = a;
            DbHandler = db;
            foreach (var item in Ojcowie)
            {
                if (item.Gender == Gender.male)
                    ComboOjcowie.Items.Add(item.Imie);
            }
            foreach (var item in Ojcowie)
            {
                if (item.Gender == Gender.female)
                    ComboMatki.Items.Add(item.Imie);
            }

            Matki = Ojcowie;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!DbHandler.IsExist(ImieText.Text))
            {
                Person nowy = new Person() { Imie = ImieText.Text, BirthDate = BirdDateText.SelectedDate.GetValueOrDefault(), DeathDate = DeathDateText.SelectedDate.GetValueOrDefault(), Gender = (Gender)ComboGender.SelectedItem };
                if (ComboOjcowie.SelectedIndex >= 0)
                {
                    Person tata = DbHandler.GetPersonFromName(ComboOjcowie.SelectedItem.ToString());
                    nowy.AddFather(tata);
                    DbHandler.UpdatePerson(tata);
                }
                else
                {
                    nowy.AddFather(null);
                }
                if (ComboMatki.SelectedIndex >= 0)
                {
                    Person mama = DbHandler.GetPersonFromName(ComboMatki.SelectedItem.ToString());
                    nowy.AddMother(mama);
                    DbHandler.UpdatePerson(mama);
                }
                else
                {
                    nowy.AddMother(null);
                }
                DbHandler.AddOsoba(nowy);
                MessageBox.Show("Osoba została dodana");
            }
            else MessageBox.Show("Osoba o takim imieniu już istnieje");
            
           
        }
    }


}
