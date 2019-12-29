using BiljeskeMaterial.KLASE;
using System;
using System.Windows;
using System.Windows.Controls;


namespace BiljeskeMaterial.KONTROLE
{
    public partial class DodajBiljesku : UserControl
    {
        public DodajBiljesku()
        {
            InitializeComponent();
        }

        private void Spremi(object sender, RoutedEventArgs e)
        {
            if ((datum.SelectedDate != null && vrijeme.SelectedTime == null) || (datum.SelectedDate == null && vrijeme.SelectedTime != null))
            {
                MessageBox.Show("Potrebno je unijeti vrijeme i datum!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else if (datum.SelectedDate < DateTime.Now.Date)
            {
                MessageBox.Show("Nije moguće zakazati prošle datume!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if ((datum.SelectedDate == DateTime.Now.Date) && vrijeme.SelectedTime < DateTime.Now.AddMinutes(2))
            {
                MessageBox.Show("Najranije podsjećanje moguće je za 2 minute!", "Unos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Baza db = new Baza();
                string datumPodsjecanja = PretvoriDatumvrijeme.PretvoriDatum(datum.SelectedDate.ToString());
                string vrijemePodsjecanja = PretvoriDatumvrijeme.PretvoriVrijeme(vrijeme.SelectedTime.ToString());
                db.Unesi(txtNaziv.Text, txtSadrzaj.Text, datumPodsjecanja, vrijemePodsjecanja);
                txtNaziv.Text = String.Empty;
                txtSadrzaj.Text = String.Empty;
                datum.Text = String.Empty;
                vrijeme.Text = String.Empty;
                MessageBox.Show("Bilješka unesena!", "Unos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
