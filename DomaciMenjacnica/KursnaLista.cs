using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomaciMenjacnica
{
    internal class KursnaLista
    {
        public string DatumListe { get; set; }
        public Dictionary<string, double[]> ListaValuta { get; set; }

        public KursnaLista(string datumListe, Dictionary<string, double[]> listaValuta)
        {
            this.DatumListe = datumListe;
            ListaValuta = listaValuta;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string[] nizDatuma = DatumListe.Split('-');

            sb.Append($"Kursna lista kreirana na dan {nizDatuma[2]}. {nizDatuma[1]}. {nizDatuma[0]}.\n" +
                $"Oznaka | Kupovni | Prodajni | Srednji\n");
            
                foreach(KeyValuePair<string, double[]> kl in ListaValuta)
                {
                    sb.Append($"  {kl.Key}  | {kl.Value[0]} | {kl.Value[1]} | {kl.Value[2]}\n");
                }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is KursnaLista))
                return false;
            KursnaLista kl = (KursnaLista)obj;
            return DatumListe.Equals(kl.DatumListe);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
