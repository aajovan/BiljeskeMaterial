using BiljeskeMaterial.KLASE;
using BiljeskeMaterial.KONTROLE;
using System.Windows;
using System.Windows.Controls;

namespace BiljeskeMaterial
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MojeBiljeske.IsSelected = true;
            UpraviteljNotifikacija upravitelj = new UpraviteljNotifikacija();
        }

        private void GumbOtvoriIzbornik_Click(object sender, RoutedEventArgs e)
        {
            GumbZatvoriIzbornik.Visibility = Visibility.Visible;
            GumbOtvoriIzbornik.Visibility = Visibility.Collapsed;
        }

        private void GumbZatvoriIzbornik_Click(object sender, RoutedEventArgs e)
        {
            GumbZatvoriIzbornik.Visibility = Visibility.Collapsed;
            GumbOtvoriIzbornik.Visibility = Visibility.Visible;
        }
        private void PromjeniStavkuIzbornika(object sender, SelectionChangedEventArgs e)
        {
            sadrzaj.Children.Clear();
            UserControl odabranaKontrola = null;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)

            {
                case "MojeBiljeske":
                    odabranaKontrola = new MojeBiljeske();
                    sadrzaj.Children.Add(odabranaKontrola);
                    break;
                case "DodajBiljesku":
                    odabranaKontrola = new DodajBiljesku();
                    sadrzaj.Children.Add(odabranaKontrola);
                    break;

                default:
                    break;
            }
        }
    }
}
