using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        //List<Kolcsonzes> kolcsonzesek = new();
        ObservableCollection<Kolcsonzes> kolcsonzesek = new();
        ObservableCollection<Kolcsonzes> vizenObservable = new();
        string filePath = @"..\..\..\src\kolcsonzes.txt";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            foreach (var item in File.ReadAllLines(filePath).Skip(1))
            {
                kolcsonzesek.Add(new Kolcsonzes(item));
            }

            osszesRekord.ItemsSource = kolcsonzesek;
            elvitelIdoCombobox.ItemsSource = kolcsonzesek;

            var vizenLevoHajok = kolcsonzesek.Where(k => k.vizenVanAHajo());
            vizenObservable = new(vizenLevoHajok);

            if (vizenObservable.Count() == 0)
            {
                MessageBox.Show("Nincs jelenleg vízen lévő hajó.");
            }
            else
            {
                vizesHajokCombobox.ItemsSource = vizenObservable;
            }
            foreach (var item in kolcsonzesek)
            {
                kolcsonzesSzamitottAdataiListbox.Items.Add(item.ToString());
            }

            azonCombobox.ItemsSource = kolcsonzesek.Select(k => k.HajoAzonosito).Distinct();
            tipusCombobox.ItemsSource = kolcsonzesek.Select(k => k.HajoTipus).Distinct();
            szemelyekSzamaCombobox.ItemsSource = kolcsonzesek.Select(k => k.SzemelyekSzama).Distinct();
        }

        private void napiBevetel_Click(object sender, RoutedEventArgs e)
        {
            napiBevetelLabel.Content = kolcsonzesek.Sum(k => k.hanyMegkezdettFelOra()) * 1500;
        }


        public bool valosHajo(byte hajoAzon, string hajoTipus, byte szemelyekSzama)
        {
            return kolcsonzesek.Any(k => k.HajoAzonosito == hajoAzon && k.HajoTipus == hajoTipus && k.SzemelyekSzama == szemelyekSzama);
        }

        private void felvitelGomb_Click(object sender, RoutedEventArgs e)
        {
            var nev = nevTextbox.Text;

            var azon = (byte)azonCombobox.SelectedItem;
            var tipus = tipusCombobox.SelectedItem.ToString();
            var szemelyekSzama = (byte)szemelyekSzamaCombobox.SelectedItem;

            var elvitelOraja = elvitelOrajaTextbox.Text;
            var elvitelPerce = elvitelPerceTextbox.Text;
            var visszahozatalOraja = visszahozatalOrajaTextbox.Text;
            var visszahozatalPerce = visszahozatalPerceTextbox.Text;

            if (nev.Contains(" ") && azon != null && tipus != null && szemelyekSzama != null && int.TryParse(elvitelOraja, out int _) && int.TryParse(elvitelPerce, out int _) && int.TryParse(visszahozatalOraja, out int _) && int.TryParse(visszahozatalPerce, out int _))
            {
                if (valosHajo(azon, tipus, szemelyekSzama))
                {
                    string sor = $"{nev};{azon};{tipus};{szemelyekSzama};{elvitelOraja};{elvitelPerce};{visszahozatalOraja};{visszahozatalPerce}";

                    using StreamWriter swKolcsonzes = new(filePath);
                    swKolcsonzes.WriteLine(sor);

                    Kolcsonzes felvittKolcsonzes = new(sor);

                    kolcsonzesek.Add(felvittKolcsonzes);
                    if (felvittKolcsonzes.vizenVanAHajo())
                    {
                        vizenObservable.Add(felvittKolcsonzes);
                    }
                }
                else
                {
                    MessageBox.Show("Az állományban ez a hajó nem szerepel.", "Hiba!");
                }
            }
            else
            {
                MessageBox.Show("Nem megfelelő formátumú valamennyi adat.", "Hiba!");
            }
        }

        private void serultHajoAllomanybaFelvitele_Click(object sender, RoutedEventArgs e)
        {

            serultFeedbackLabel.Content = string.Empty;

            var serultHajoAzonosito = serultHajoAzonTextbox.Text;
            if (serultHajoAzonosito == null || !int.TryParse(serultHajoAzonosito, out int serulHajoAzonParsed))
            {
                MessageBox.Show("A felvitt azonosító nem megfelelő formátumú", "Hiba!");
            }
            else
            {
                var serultHajo = kolcsonzesek.FirstOrDefault(k => k.HajoAzonosito == serulHajoAzonParsed);

                if (serultHajo == null)
                {
                    serultFeedbackLabel.Content = "A fájlbaírás sikertelen. Az állományban ez az azonosító nem szerepel.";
                }
                else
                {
                    using StreamWriter swRongalas = new($@"..\..\..\src\rongalt_hajok\rongalas_{serultHajo.HajoAzonosito}.txt");

                    swRongalas.WriteLine($"Lehetséges elkövető neve: {serultHajo.Nev}");
                    swRongalas.WriteLine($"Ettől eddig volt nála a hajó: {serultHajo.ElvitelIdoben} - {serultHajo.VisszahozatalIdoben}");
                    serultFeedbackLabel.Content = "A fájlbaírás sikeres.";
                    serultHajoAzonTextbox.Clear();
                }
            }
        }
    }
}