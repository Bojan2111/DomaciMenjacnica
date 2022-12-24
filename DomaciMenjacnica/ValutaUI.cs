using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomaciMenjacnica
{
    internal class ValutaUI
    {
        internal static List<Valuta> listaValuta = new List<Valuta>();
        internal static void UcitajValuteIzFajla(string adresa)
        {
            if (File.Exists(adresa))
            {
                using (StreamReader sr = new StreamReader(adresa))
                {
                    string linija;
                    while ((linija = sr.ReadLine()) != null)
                    {
                        listaValuta.Add(Valuta.FromFileToObject(linija));
                    }
                }
                    
            }
            else
            {
                Console.WriteLine("Nepostojeci fajl ili neispravna putanja");
            }
        }

        internal static void ValuteMeni()
        {
            int odabir = -1;
            while (odabir != 0)
            {
                Console.Clear();
                Console.WriteLine("Dostupne Valute:");
                foreach (Valuta v in listaValuta)
                {
                    Console.WriteLine(v);
                }
                Console.WriteLine("\nOpcije:");
                Console.WriteLine("1 - Rad sa kursnim listama");
                Console.WriteLine("0 - Nazad");
                Console.WriteLine("Izbor:");
                odabir = int.Parse(Console.ReadLine());
                switch (odabir)
                {
                    case 0:
                        Console.WriteLine("Povratak na glavni meni . . .");
                        break;
                    case 1:
                        KursnaListaUI.KursneListeMeni();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda!\n\n");
                        break;
                }
            }
        }
    }
}
