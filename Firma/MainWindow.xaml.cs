using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Firma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Pracownik> listaPracownikow;
        public int ID = 0;

        public MainWindow()
        {
            InitializeComponent();
            stack1.Visibility = Visibility.Hidden;
            stack2.Visibility = Visibility.Hidden;
            i1.Visibility = Visibility.Hidden;
            i2.Visibility = Visibility.Hidden;
            addPanel.Visibility = Visibility.Hidden;
            comboBox.Visibility = Visibility.Hidden;
            button.Visibility = Visibility.Hidden;
            listaPracownikow = new ObservableCollection<Pracownik>();

            listView.Items.Clear();
            Pracownik p1 = new Programista(1, "Adrian", "Nidzgorski", Pracownik.Plec.Mezczyzna, 22, 5000.0, Programista.Specjalnosc.C);
            Pracownik p2 = new Programista(2, "Darek", "Kowalski", Pracownik.Plec.Mezczyzna, 32, 15000.0, Programista.Specjalnosc.JAVA);
            Pracownik p3 = new Ksiegowy(3, "Anna", "Nowak", Pracownik.Plec.Kobieta, 28, 18000.0, Ksiegowy.Stanowisko.GlownyKsiegowy);

            listaPracownikow.Add(p1);
            listaPracownikow.Add(p2);
            listaPracownikow.Add(p3);
            this.listView.ItemsSource = listaPracownikow;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e){
            addPanel.Visibility = Visibility.Hidden;
            comboBox.Visibility = Visibility.Hidden;
            comboBox2.Visibility = Visibility.Hidden;
            button.Visibility = Visibility.Hidden;
            stack1.Visibility = Visibility.Visible;
            stack2.Visibility = Visibility.Visible;
            i1.Visibility = Visibility.Visible;
            i2.Visibility = Visibility.Visible;
            image1.Visibility = Visibility.Visible;
          
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                string tekst = item.ToString();
                tekst = Regex.Replace(tekst, "[^0-9]+", string.Empty);
                int indeks = Int32.Parse(tekst);
                indeks--;

                Pracownik osoba = listaPracownikow[indeks];

                label4.Content = osoba.Imie;
                label5.Content = osoba.Nazwisko;
                label6.Content = osoba.Wiek;
                label7.Content = osoba.pensjaRoczna();

                if (osoba is Ksiegowy)
                {
                    Ksiegowy k = (Ksiegowy)osoba;
                    label7.Content = k.pensjaRoczna() + k.oszacujPremie();
                }                  

                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();

                if (osoba._Plec == Pracownik.Plec.Kobieta)
                {                   
                    bi3.UriSource = new Uri("Images/woman.png", UriKind.Relative);
                    bi3.EndInit();
                    image1.Stretch = Stretch.Fill;
                    image1.Source = bi3;
                }  
                else
                {
                    bi3.UriSource = new Uri("Images/man.png", UriKind.Relative);
                    bi3.EndInit();
                    image1.Stretch = Stretch.Fill;
                    image1.Source = bi3;
                }

                if(osoba is Programista)
                {
                    Programista o = (Programista)osoba;
                    label8.Content = "Specjalność";
                    label10.Content = o._Specjalnosc;

                }
                else if (osoba is Ksiegowy)
                {
                    Ksiegowy o = (Ksiegowy)osoba;
                    label8.Content = "Stanowisko";
                    label10.Content = o._Stanowisko;
                }
            }
        }//private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)

        private void listView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stack1.Visibility = Visibility.Hidden;
            stack2.Visibility = Visibility.Hidden;
            i1.Visibility = Visibility.Hidden;
            i2.Visibility = Visibility.Hidden;
            image1.Visibility = Visibility.Hidden;
            addPanel.Visibility = Visibility.Hidden;
            comboBox.Visibility = Visibility.Hidden;
            button.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            stack2.Visibility = Visibility.Hidden;
            i1.Visibility = Visibility.Hidden;
            i2.Visibility = Visibility.Hidden;
            image1.Visibility = Visibility.Hidden;

            stack1.Visibility = Visibility.Visible;
            addPanel.Visibility = Visibility.Visible;
            comboBox.Visibility = Visibility.Visible;
            button.Visibility = Visibility.Visible;
            comboBox2.Visibility = Visibility.Visible;

            textBox.Text = " ";
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            comboBox.SelectedIndex = 0;

            addPanel.Margin = new System.Windows.Thickness(106, 124, 0, 0);
            comboBox2.Margin = new System.Windows.Thickness(47, 318, 0, 0);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                comboBox2.ItemsSource = new List<string> { "C", "JAVA", "JavaScript" };
            }
            else
            {
                comboBox2.ItemsSource = new List<string> { "GlownyKsiegowy", "KontrolerFinansowy", "DyrektorFinansowy" };
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text) &&
                !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrWhiteSpace(comboBox.Text) &&
                !string.IsNullOrWhiteSpace(comboBox2.Text)
                )
            {
                int lastID = listaPracownikow[listaPracownikow.Count - 1].ID;
                lastID++;
                string kto = comboBox.Text;

                if(kto.Equals("Programista"))
                {
                    Programista.Specjalnosc status = (Programista.Specjalnosc)Enum.Parse(typeof(Programista.Specjalnosc), comboBox2.Text, true);
                    try
                    {
                        if (textBox.Text[textBox.Text.Length-1] == 'a')
                            listaPracownikow.Add(new Programista(lastID, textBox.Text, textBox1.Text, Programista.Plec.Kobieta, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), status));
                        else
                            listaPracownikow.Add(new Programista(lastID, textBox.Text, textBox1.Text, Programista.Plec.Mezczyzna, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), status));
                    }
                    catch (FormatException excep)
                    {
                        MessageBox.Show("Wprowadzono zły format danych.", "Wyjatek FormatException");
                    }                                              
                }
                else if (kto.Equals("Ksiegowy"))
                {
                    Ksiegowy.Stanowisko status = (Ksiegowy.Stanowisko)Enum.Parse(typeof(Ksiegowy.Stanowisko), comboBox2.Text, true);
                    try
                    {
                        if (textBox.Text[textBox.Text.Length - 1] == 'a')
                            listaPracownikow.Add(new Ksiegowy(lastID, textBox.Text, textBox1.Text, Programista.Plec.Kobieta, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), status));
                        else
                            listaPracownikow.Add(new Ksiegowy(lastID, textBox.Text, textBox1.Text, Programista.Plec.Mezczyzna, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), status));
                    }
                    catch (FormatException excep)
                    {
                        MessageBox.Show("Wprowadzono zły format danych.", "Wyjatek FormatException");
                    }
                }
            }
            else
                MessageBox.Show("Wprowadź wszystkie dane!");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
