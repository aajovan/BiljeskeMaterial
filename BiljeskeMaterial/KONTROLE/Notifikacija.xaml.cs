using System.Windows;

namespace BiljeskeMaterial.KONTROLE
{
    public partial class Notifikacija
    {
        public Notifikacija(string naslov, string sadrzaj)
        {

            InitializeComponent();
            notifikacija_naslov.Text = naslov;
            notifikacija_sadrzaj.Text = sadrzaj;
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 20;

        }
        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
