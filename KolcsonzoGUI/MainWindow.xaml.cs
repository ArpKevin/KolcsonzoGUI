using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KolcsonzoGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kolcsonzes> kolcsonzesek = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            foreach (var item in File.ReadAllLines(@"..\..\..\src\kolcsonzes.txt").Skip(1))
            {
                kolcsonzesek.Add(new Kolcsonzes(item));
            }

            osszesRekord.ItemsSource = kolcsonzesek;
            elvitelIdoCombobox.ItemsSource = kolcsonzesek;

            var vizenLevoHajok = kolcsonzesek.Where(k => k.vizenVanAHajo());

            if (vizenLevoHajok.Count() == 0)
            {
                MessageBox.Show("Nincs jelenleg vízen lévő hajó.");
            }
            else
            {
                vizesHajokCombobox.ItemsSource = vizenLevoHajok;
            }
            foreach (var item in kolcsonzesek)
            {
                kolcsonzesSzamitottAdataiListbox.Items.Add(item.ToString());
            }

            var felviteliLista = kolcsonzesek.Select(k => new { Azonosito = k.HajoAzonosito, Hajotipus = k.HajoTipus, SzemelyekSzama = k.SzemelyekSzama, ElvitelOraja = k.ElvitelOraja, ElvitelPerce = k.ElvitelPerce, VisszahozatalOraja = k.VisszahozatalOraja, VisszahozatalPerce = k.VisszahozatalPerce });


        }

        private void napiBevetel_Click(object sender, RoutedEventArgs e)
        {
            napiBevetelLabel.Content = kolcsonzesek.Sum(k => k.hanyMegkezdettFelOra()) * 1500;
        }
    }
}