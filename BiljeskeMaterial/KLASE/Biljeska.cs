using System.ComponentModel;

namespace BiljeskeMaterial.KLASE
{
    public class Biljeska : INotifyPropertyChanged
    {
        private int _id;
        private string _naslov;
        private string _sadrzaj;
        private string _datumPodsjecanja;
        private string _vrijemePodsjecanja;
        public Biljeska(int id, string naslov, string sadrzaj, string datum, string vrijeme)
        {
            _id = id;
            _naslov = naslov;
            _sadrzaj = sadrzaj;
            _datumPodsjecanja = datum;
            _vrijemePodsjecanja = vrijeme;
        }
        public int Id
        {
            get
            {
                return _id;
            }

        }
        public string Naslov
        {
            get
            {
                return _naslov;
            }
            set
            {
                if (_naslov != value)
                {
                    _naslov = value;
                    this.NotifyPropertyChanged("Naslov");
                }

            }

        }
        public string Sadrzaj
        {
            get
            {
                return _sadrzaj;
            }
            set
            {
                if (_sadrzaj != value)
                {
                    _sadrzaj = value;
                    this.NotifyPropertyChanged("Sadrzaj");
                }
            }


        }
        public string DatumPodsjecanja
        {
            get
            {
                return _datumPodsjecanja;
            }
            set
            {
                if (_datumPodsjecanja != value)
                {
                    _datumPodsjecanja = value;
                    this.NotifyPropertyChanged("DatumPodsjecanja");
                }
            }


        }
        public string VrijemePodsjecanja
        {
            get
            {
                return _vrijemePodsjecanja;
            }
            set
            {
                if (_vrijemePodsjecanja != value)
                {
                    _vrijemePodsjecanja = value;
                    this.NotifyPropertyChanged("VrijemePodsjecanja");
                }
            }


        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
