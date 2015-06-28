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
        private bool rem {get;set;}
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
                Rodzina = db.GetPersonList;
                ActualPerson = Rodzina[Drzewko.SelectedIndex ];
                
            }
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            rem = true;
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
            UpdatePerson okno = new UpdatePerson(db, ActualPerson);
            okno.Show();
        }

        private void UsunOsobe_Click(object sender, RoutedEventArgs e)
        {
            rem = true;
            Person person = db.GetPersonFromName( Rodzina[Drzewko.SelectedIndex].Imie);
            List<Person> ToUpdate = new List<Person>();
            ToUpdate = person.RemovePerson();
            db.DeletePerson(person);
            if ( ToUpdate.Count > 0)
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

        private void PokazOsobe_Click(object sender, RoutedEventArgs e)
        {
            ShowPerson okno = new ShowPerson(ActualPerson);
            okno.Show();
        }
        
    }

}
