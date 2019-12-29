using BiljeskeMaterial.KLASE;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace BiljeskeMaterial.KONTROLE
{
    public partial class MojeBiljeske : UserControl
    {
        private ObservableCollection<Biljeska> biljeske;
        public MojeBiljeske()
        {
            InitializeComponent();
            Baza db = new Baza();
            biljeske = new ObservableCollection<Biljeska>(db.DohvatiBiljeske());
            mojeBiljeske.ItemsSource = biljeske;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(mojeBiljeske.ItemsSource);
            view.Filter = biljeskeFilter;
        }
        private bool biljeskeFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Biljeska).Naslov.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private void Filter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(mojeBiljeske.ItemsSource).Refresh();
        }
        private void Ukloni(object sender, RoutedEventArgs e)
        {
            if (mojeBiljeske.SelectedItem!=null) {
                Baza db = new Baza();
                Biljeska odabrana = (Biljeska)mojeBiljeske.SelectedItem;
                db.Izbrisi(odabrana.Id);
                biljeske.Remove(odabrana);
            }        
        }

   private void Spremi(object sender, RoutedEventArgs e)
        {
            if (mojeBiljeske.SelectedItem != null)
            {
                if ((datum.SelectedDate != null && vrijeme.SelectedTime == null) || (datum.SelectedDate == null && vrijeme.SelectedTime != null))
                {
                    MessageBox.Show("Potrebno je unijeti vrijeme i datum!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (datum.SelectedDate < DateTime.Now.Date)
                {
                    MessageBox.Show("Nije moguće zakazati prošle datume!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if((datum.SelectedDate == DateTime.Now.Date) && vrijeme.SelectedTime < DateTime.Now.AddMinutes(2))
                {
                    MessageBox.Show("Najranije podsjećanje moguće je za 2 minute!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Biljeska odabrana = (Biljeska)mojeBiljeske.SelectedItem;
                    odabrana.Naslov = detaljnoNaslov.Text;
                    odabrana.Sadrzaj = detaljnoSadrzaj.Text;
                    odabrana.DatumPodsjecanja = PretvoriDatumvrijeme.PretvoriDatum(datum.SelectedDate.ToString());
                    odabrana.VrijemePodsjecanja = PretvoriDatumvrijeme.PretvoriVrijeme(vrijeme.SelectedTime.ToString());
                    Baza db = new Baza();
                    db.Azuriraj(odabrana.Id, odabrana.Naslov, odabrana.Sadrzaj, odabrana.DatumPodsjecanja, odabrana.VrijemePodsjecanja);
                    MessageBox.Show("Bilješka spremljena!", "Unos", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }      
        } 
    }
}


