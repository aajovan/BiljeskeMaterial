using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace BiljeskeMaterial.KLASE
{
    public class Baza
    {
        private static readonly string BAZA_PUTANJA = Directory.GetCurrentDirectory() + "/baza.sqlite";
        private static readonly string CONNECTION_STRING = "Data Source=" + BAZA_PUTANJA;
        private static readonly string SQL_INSERT = "INSERT INTO biljeske (naslov, sadrzaj, datum, vrijeme) VALUES ('{0}', '{1}', '{2}','{3}');";
        private static readonly string SQL_UPDATE = "UPDATE biljeske SET naslov = '{0}', sadrzaj = '{1}', datum = '{2}', vrijeme = '{3}' WHERE id = {4};";
        private static readonly string SQL_DELETE = "DELETE FROM biljeske WHERE id = {0};";
        private static readonly string SQL_SELECT_ALL = "SELECT * FROM biljeske";
        private static readonly string SQL_STVORI = "CREATE TABLE biljeske (" +
                                                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                                                    "naslov TEXT," +
                                                    "sadrzaj TEXT," +
                                                     "datum TEXT," +
                                                    "vrijeme TEXT)";
        private static readonly string STUPAC_ID = "id";
        private static readonly string STUPAC_NASLOV = "naslov";
        private static readonly string STUPAC_SADRZAJ = "sadrzaj";
        private static readonly string STUPAC_DATUM = "datum";
        private static readonly string STUPAC_VRIJEME = "vrijeme";
        SQLiteConnection _veza;

        public Baza()
        {
            if (!System.IO.File.Exists(BAZA_PUTANJA))
            {
                StvoriBazu();
            }
        }
        private void StvoriBazu()
        {
            SQLiteConnection.CreateFile(BAZA_PUTANJA);

            try
            {
                OtvoriVezu();
                SQLiteCommand naredba = new SQLiteCommand(SQL_STVORI, _veza);
                naredba.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod stvaranja baze " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Unesi(string naslov, string sadrzaj, string datum, string vrijeme)
        {
            OtvoriVezu();

            try
            {
                SQLiteCommand naredba = new SQLiteCommand(String.Format(SQL_INSERT, naslov, sadrzaj, datum, vrijeme), _veza);
                naredba.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod unosa u bazu " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ZatvoriVezu();
        }
        public void Azuriraj(int id, string naslov, string sadrzaj, string datum, string vrijeme)
        {
            OtvoriVezu();

            try
            {
                SQLiteCommand naredba = new SQLiteCommand(String.Format(SQL_UPDATE, naslov, sadrzaj, datum, vrijeme, id), _veza);
                naredba.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod azuriranja u bazu " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZatvoriVezu();
        }
        public void Izbrisi(int id)
        {
            OtvoriVezu();

            try
            {
                SQLiteCommand naredba = new SQLiteCommand(String.Format(SQL_DELETE, id), _veza);
                naredba.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod brisanja iz baze " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZatvoriVezu();
        }
        public List<Biljeska> DohvatiBiljeske()
        {
            OtvoriVezu();
            List<Biljeska> biljeske = new List<Biljeska>();

            try
            {
                SQLiteCommand naredba = new SQLiteCommand(SQL_SELECT_ALL, _veza);
                SQLiteDataReader reader = naredba.ExecuteReader();
                while (reader.Read())
                {
                    Biljeska biljeska = new Biljeska(
                        Int32.Parse(reader[STUPAC_ID].ToString()),
                        reader[STUPAC_NASLOV].ToString(),
                        reader[STUPAC_SADRZAJ].ToString(),
                        reader[STUPAC_DATUM].ToString(),
                        reader[STUPAC_VRIJEME].ToString()

                        );

                    biljeske.Add(biljeska);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod dohvaćanja podataka iz baze " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZatvoriVezu();
            return biljeske;
        }
        public List<Biljeska> DohvatiDanasnjeBiljeske()
        {
            OtvoriVezu();
            List<Biljeska> biljeske = new List<Biljeska>();

            try
            {
                SQLiteCommand naredba = new SQLiteCommand(SQL_SELECT_ALL, _veza);
                SQLiteDataReader reader = naredba.ExecuteReader();
                while (reader.Read())
                {
                    Biljeska biljeska = new Biljeska(
                        Int32.Parse(reader[STUPAC_ID].ToString()),
                        reader[STUPAC_NASLOV].ToString(),
                        reader[STUPAC_SADRZAJ].ToString(),
                        reader[STUPAC_DATUM].ToString(),
                        reader[STUPAC_VRIJEME].ToString()

                        );


                    DateTime datumPostoji;
                    DateTime.TryParse(biljeska.DatumPodsjecanja, out datumPostoji);
                    bool danas = datumPostoji.Date == DateTime.Now.Date;
                    if (danas) { biljeske.Add(biljeska); }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod dohvaćanja podataka iz baze " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZatvoriVezu();
            return biljeske;
        }
        private void OtvoriVezu()
        {
            try
            {
                _veza = new SQLiteConnection(CONNECTION_STRING);
                _veza.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod uspostavljanja veze sa bazom " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ZatvoriVezu()
        {
            try
            {
                _veza.Close();
                _veza.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogreška kod uspostavljanja veze sa bazom " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}

