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
using Db4objects.Db4o;
using Projekty.Class;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Projekty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Osoba> _Ksiazka = new List<Osoba>();
        private bool rem = false;
        private readonly DbHandler baza = null;
        public MainWindow()
        {
            InitializeComponent();
            Tabelka.ItemsSource = _Ksiazka;
            baza = new DbHandler();
            _Ksiazka = baza.getListItems;
            Tabelka.ItemsSource = _Ksiazka;

        }

        private void ShowOsoba(Osoba temp)
        {
            TextImie.Text = temp.Imie;
            TextNaziwsko.Text = temp.Nazwisko;
            TextKraj.Text = temp.Adr.Kraj;
            TextWoj.Text = temp.Adr.Województwo;
            TextMiejsc.Text = temp.Adr.Miejscowość;
            TextUlica.Text = temp.Adr.Ulica;
            TextKod.Text = temp.Adr.Kod_Pocztowy;
            TextImie.IsReadOnly = true;
            TextNaziwsko.IsReadOnly = true;
        }

        private void ShowNumer(Telefon tel)
        {
            TextNumer.Text = tel.Numer.ToString();
            TextOperator.Text = tel.Operator.ToString();
            TextTyp.Text = tel.Rodzaj.ToString();
        }

        private bool DataOK()
        {
            if ((TextNumer.Text.Length >= 9) && (TextOperator.SelectedItem != null) && (TextTyp.SelectedItem != null) &&
                (TextKraj.Text.Length >= 2) && (TextWoj.Text.Length >= 5) && (TextMiejsc.Text.Length >= 3) &&
                (TextUlica.Text.Length >= 3) && (TextKod.Text.Length == 6) && (TextImie.Text.Length >= 3) && (TextNaziwsko.Text.Length >= 3))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Wprawdź wszystkie dane do formularza przed dodaniem kontaktu", "Data Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            return false;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (DataOK())
            {
                Telefon tel = new Telefon(TextNumer.Text, (Telefon.Operatorzy)TextOperator.SelectedItem, (Telefon.TypTelefonu)TextTyp.SelectedItem);
                Adres adr = new Adres(TextMiejsc.Text, TextUlica.Text, TextKod.Text, TextWoj.Text, TextKraj.Text);
                Osoba os = new Osoba(TextImie.Text, TextNaziwsko.Text, adr, tel);
                os.Tel[os.Tel.Count - 1].Numer = Convert.ToInt32(TextNumer.Text);                
                baza.AddOsoba(os);
                _Ksiazka = baza.getListItems;
                Tabelka.ItemsSource = null;
                Tabelka.ItemsSource = _Ksiazka;
                Tabelka.Items.Refresh();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DodajOsoba.Visibility = System.Windows.Visibility.Visible;
            UpdateOsoba.Visibility = System.Windows.Visibility.Hidden;
            List_Numerow.SelectedItem= null;
            List_Numerow.ItemsSource = null;
            ShowOsoba(new Osoba());
            ShowNumer(new Telefon());
            TextNaziwsko.IsReadOnly = false;
            TextImie.IsReadOnly = false;
        }


        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

          
        }

        private void Tabelka_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (!rem)
            {
                int i = Tabelka.SelectedIndex;
                if (i >= 0)
                {
                    DodajOsoba.Visibility = System.Windows.Visibility.Hidden;
                    UpdateOsoba.Visibility = System.Windows.Visibility.Visible;
                    List_Numerow.ItemsSource = _Ksiazka[i].GetTelList;
                    _Ksiazka[i].Atel = _Ksiazka[i].Tel[0];
                    ShowOsoba(_Ksiazka[i]);
                    ShowNumer(_Ksiazka[i].Atel);
                    List_Numerow.SelectedIndex = 0;
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            rem = true;
            Osoba os = Tabelka.SelectedItem as Osoba;
            Tabelka.SelectedItem = null;
            baza.Delete(os);
            _Ksiazka.Remove(os);
            Tabelka.Items.Refresh();
            if (_Ksiazka.Count > 0) ShowOsoba(_Ksiazka[0]);
            rem = false;
        }

        private void List_Numerow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!rem)
            {
                int i = Tabelka.SelectedIndex;
                if (_Ksiazka[i].Tel.Count > 0)
                {
                    int t = List_Numerow.SelectedIndex;
                    if (t >= 0) _Ksiazka[i].Atel = _Ksiazka[i].Tel[t];
                    else if (t < 0)
                        _Ksiazka[i].Atel = _Ksiazka[i].Tel[0];
                }
                ShowNumer(_Ksiazka[i].Atel);
                

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int i = Tabelka.SelectedIndex;
            Telefon tel = new Telefon(TextNumer.Text, (Telefon.Operatorzy)TextOperator.SelectedItem, (Telefon.TypTelefonu)TextTyp.SelectedItem);
            _Ksiazka[i].AddTelefon(tel);
            baza.Update(_Ksiazka[i]);
            List_Numerow.Items.Refresh();

            
        }

        private void UpdateOsoba_Click(object sender, RoutedEventArgs e)
        {
            if (DataOK())
            {
                Osoba osu = _Ksiazka[Tabelka.SelectedIndex];
                osu.Adr.Kraj = TextKraj.Text;
                osu.Adr.Województwo = TextWoj.Text;
                osu.Adr.Miejscowość = TextMiejsc.Text;
                osu.Adr.Ulica = TextUlica.Text;
                osu.Adr.Kod_Pocztowy = TextKod.Text;
                osu.Tel = List_Numerow.ItemsSource as List<Telefon>;
                baza.Update(osu);
                _Ksiazka = baza.getListItems;
                Tabelka.ItemsSource = null;
                Tabelka.ItemsSource = _Ksiazka;
                Tabelka.Items.Refresh();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            baza._Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int i = List_Numerow.SelectedIndex;
            int o = Tabelka.SelectedIndex;
            _Ksiazka[o].Tel[i].Numer = Convert.ToInt32(TextNumer.Text);
            _Ksiazka[o].Tel[i].Operator = (Telefon.Operatorzy)TextOperator.SelectedItem;
            _Ksiazka[o].Tel[i].Rodzaj = (Telefon.TypTelefonu)TextTyp.SelectedItem;
            baza.Update(_Ksiazka[o]);
            List_Numerow.Items.Refresh();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SImie.Text = "";
            SNazwisko.Text = "";
            _Ksiazka = baza.getListItems;
            Tabelka.ItemsSource = _Ksiazka;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            List<Osoba> temp = baza.getListItems.Where(x => x.Imie.Contains(SImie.Text) && x.Nazwisko.Contains(SNazwisko.Text)).ToList();
            Tabelka.ItemsSource = temp;
            Tabelka.Items.Refresh();
        }
    }


}
