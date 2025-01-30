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

            azonCombobox.ItemsSource = kolcsonzesek.Select(k => k.HajoAzonosito).Distinct();
            tipusCombobox.ItemsSource = kolcsonzesek.Select(k => k.HajoTipus).Distinct();
            szemelyekSzamaCombobox.ItemsSource = kolcsonzesek.Select(k => k.SzemelyekSzama).Distinct();
            //asd.Content = valosHajo(1, "kajak", 3);
        }

        private void napiBevetel_Click(object sender, RoutedEventArgs e)
        {
            napiBevetelLabel.Content = kolcsonzesek.Sum(k => k.hanyMegkezdettFelOra()) * 1500;
        }


        public bool valosHajo(byte hajoAzon, string hajoTipus, byte szemelyekSzama)
        {
            return kolcsonzesek.Exists(k => k.HajoAzonosito == hajoAzon && k.HajoTipus == hajoTipus && k.SzemelyekSzama == szemelyekSzama);
        }

        private void felvitelGomb_Click(object sender, RoutedEventArgs e)
        {
            var nev = nevTextbox.Text;

            var azon = azonCombobox.SelectedItem;
            var tipus = tipusCombobox.SelectedItem;
            var szemelyekSzama = szemelyekSzamaCombobox.SelectedItem;

            var elvitelOraja = elvitelOrajaTextbox.Text;
            var elvitelPerce = elvitelPerceTextbox.Text;
            var visszahozatalOraja = visszahozatalOrajaTextbox.Text;
            var visszahozatalPerce = visszahozatalPerceTextbox.Text;
        }
    }
}