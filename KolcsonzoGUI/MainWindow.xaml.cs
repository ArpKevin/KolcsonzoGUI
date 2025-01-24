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
        public MainWindow()
        {
            InitializeComponent();

            List<Kolcsonzes> kolcsonzesek = new();
            foreach (var item in File.ReadAllLines(@"..\..\..\src\kolcsonzes.txt").Skip(1))
            {
                kolcsonzesek.Add(new Kolcsonzes(item));
            }

            osszesRekord.ItemsSource = kolcsonzesek;

            asd.Content = string.Format("{0:HH:mm:ss}", DateTime.Now);
        }
    }
}