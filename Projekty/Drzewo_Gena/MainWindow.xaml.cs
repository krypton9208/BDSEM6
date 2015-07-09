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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drzewo_Gena
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DbHandler db;
        private bool rem { get; set; }
        public List<Person> Rodzina { get; set; }
        public Person ActualPerson { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            db = new DbHandler();
            Rodzina = new List<Person>();
            Rodzina = db.GetPersonList;
            Drzewko.ItemsSource = Rodzina;
        }

        private void DodajOsobe_Click(object sender, RoutedEventArgs e)
        {
            AddPerson Okno = new AddPerson(db, Rodzina);
            Okno.Show();
        }

        private void Drzewko_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!rem)
            {
                if (Drzewko.SelectedItems.Count == 2)
                {
                    PokazOsobe.Visibility = System.Windows.Visibility.Hidden;
                    UsunOsobe.Visibility = System.Windows.Visibility.Hidden;
                    EdytujOsobe.Visibility = System.Windows.Visibility.Hidden;
                    HeirsPerson.Visibility = System.Windows.Visibility.Hidden;
                    AncestryPerson.Visibility = System.Windows.Visibility.Visible;
                    GenTree.Visibility = System.Windows.Visibility.Hidden;
                    //MessageBox.Show("2");
                }
                else if (Drzewko.SelectedItems.Count == 1)
                {
                    Rodzina = db.GetPersonList;
                    ActualPerson = Rodzina[Drzewko.SelectedIndex];
                    PokazOsobe.Visibility = System.Windows.Visibility.Visible;
                    EdytujOsobe.Visibility = System.Windows.Visibility.Visible;
                    UsunOsobe.Visibility = System.Windows.Visibility.Visible;
                    HeirsPerson.Visibility = System.Windows.Visibility.Visible;
                    AncestryPerson.Visibility = System.Windows.Visibility.Hidden;
                    GenTree.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    PokazOsobe.Visibility = System.Windows.Visibility.Hidden;
                    UsunOsobe.Visibility = System.Windows.Visibility.Hidden;
                    EdytujOsobe.Visibility = System.Windows.Visibility.Hidden;
                    HeirsPerson.Visibility = System.Windows.Visibility.Hidden;
                    AncestryPerson.Visibility = System.Windows.Visibility.Hidden;
                    GenTree.Visibility = System.Windows.Visibility.Hidden;

                }

            }
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            rem = true;
            PokazOsobe.Visibility = System.Windows.Visibility.Hidden;
            UsunOsobe.Visibility = System.Windows.Visibility.Hidden;
            EdytujOsobe.Visibility = System.Windows.Visibility.Hidden;
            HeirsPerson.Visibility = System.Windows.Visibility.Hidden;
            GenTree.Visibility = System.Windows.Visibility.Hidden;
            Drzewko.ItemsSource = null;
            Rodzina = db.GetPersonList;
            Drzewko.ItemsSource = Rodzina;
            rem = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db._Close();
        }

        private void EdytujOsobe_Click(object sender, RoutedEventArgs e)
        {
            if (Drzewko.Items.Count > 0)
            {
                UpdatePerson okno = new UpdatePerson(db, ActualPerson);
                okno.Show();
            }
        }

        private void UsunOsobe_Click(object sender, RoutedEventArgs e)
        {
            rem = true;
            if (Drzewko.Items.Count > 0)
            {
                Person person = db.GetPersonFromName(Rodzina[Drzewko.SelectedIndex].Imie);
                List<Person> ToUpdate = new List<Person>();
                ToUpdate = person.RemovePerson();
                db.DeletePerson(person);
                if (ToUpdate.Count > 0)
                {
                    foreach (var item in ToUpdate)
                    {
                        db.UpdatePerson(item);
                    }
                }

                Drzewko.ItemsSource = null;
                Rodzina = db.GetPersonList;
                Drzewko.ItemsSource = Rodzina;
                rem = false;
            }
        }

        private void PokazOsobe_Click(object sender, RoutedEventArgs e)
        {
            if (Drzewko.Items.Count > 0)
            {
                ShowPerson okno = new ShowPerson(ActualPerson);
                okno.Show();
            }
        }

        private void HeirsPerson_Click(object sender, RoutedEventArgs e)
        {
            string temp = string.Empty;
            foreach (var item in ActualPerson.Heirs(ActualPerson))
            {
                temp += item.Imie;
            }
            if (temp == string.Empty) MessageBox.Show("Brak spadkobierców");
            else MessageBox.Show(temp);
        }

        private void AncestryPerson_Click(object sender, RoutedEventArgs e)
        {
            Person first = new Person();
            Person second = new Person();
            int i = 0;
            foreach (var item in Drzewko.SelectedItems)
            {
                if (i == 0)
                {
                    first = item as Person;
                    i = 1;
                }
                else if (i == 1) second = item as Person;

            }
            List<Person> ancestors = db.CommonAncestors(
                    db.GetPersonFromName(first.Imie),
                    db.GetPersonFromName(second.Imie));
            string temp = string.Empty;
            foreach (var item in ancestors)
            {
                temp += item.Imie + "\n";
            }
            MessageBox.Show(temp);
        }

        private void GenTree_Click(object sender, RoutedEventArgs e)
        {
            GenTree gentree = new GenTree(ActualPerson);
            gentree.Show();
        }

    }

}
