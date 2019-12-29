using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiljeskeMaterial.KLASE
{
    public static class PretvoriDatumvrijeme
    {
        public static string PretvoriDatum(string datum)
        {
            string DatumPodsjecanja = string.Empty;
            if (datum != "")
            {
                DatumPodsjecanja = DateTime.Parse(datum.ToString()).ToString("dd.M.yyyy");
            }

            return DatumPodsjecanja;
        }
        public static string PretvoriVrijeme(string vrijeme)
        {
            string VrijemePodsjecanja = string.Empty;
            if (vrijeme != "")
            {
                VrijemePodsjecanja = DateTime.Parse(vrijeme.ToString()).ToString("HH:mm");
            }
            return VrijemePodsjecanja;

        }
            

        }
    }

