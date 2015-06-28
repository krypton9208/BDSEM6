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
    /// Interaction logic for ShowPerson.xaml
    /// </summary>
    public partial class ShowPerson : Window
    {
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
                ComboOjcowie.Items.Add(person.Father.Imie);
                ComboOjcowie.SelectedItem = person.Father.Imie;
            }
            if (person.Mother != null)
            {
                ComboMatki.Items.Add(person.Mother.Imie);
                ComboMatki.SelectedItem = person.Mother.Imie;
            }
            if (person.Children.Count > 0)
            Dzieci.ItemsSource = person.Children;
            

        }
        public ShowPerson(Person person)
        {
            ActualPerson = person;
            InitializeComponent();
            LoadPerson(ActualPerson);
            DoEvents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPerson(ActualPerson);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
