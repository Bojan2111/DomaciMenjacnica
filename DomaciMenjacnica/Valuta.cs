using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomaciMenjacnica
{
    internal class Valuta
    {
        public string OznakaValute { get; set; }
        public string NazivValute { get; set; }

        public Valuta(string oznakaValute, string nazivValute)
        {
            OznakaValute = oznakaValute;
            NazivValute = nazivValute;
        }

        public override string ToString()
        {
            return $"Valuta sa oznakom {OznakaValute} nosi naziv {NazivValute}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Valuta))
                return false;
            Valuta v = (Valuta)obj;
            return OznakaValute.Equals(v.OznakaValute);
        }

        public override int GetHashCode()
        {
            return OznakaValute.GetHashCode() ^ NazivValute.GetHashCode();
        }

        internal static Valuta FromFileToObject(string tekst)
        {
            string[] tokeni = tekst.Split(',');
            if (tokeni.Length != 2)
            {
                Console.WriteLine("Greska pri ocitavanju valute " + tekst);
                Environment.Exit(0);
            }

            return new Valuta(tokeni[0], tokeni[1]);
        }
    }
}
