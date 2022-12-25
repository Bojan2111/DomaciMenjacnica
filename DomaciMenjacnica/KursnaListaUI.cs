using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomaciMenjacnica
{
    internal class KursnaListaUI
    {
        internal static List<KursnaLista> kursneListe = new List<KursnaLista>();
        internal static void KursneListeMeni()
        {
            Console.Clear();
            int odabir = -1;
            while (odabir != 0)
            {
                
                Console.WriteLine("\nOpcije:");
                IspisOpcija();
                odabir = int.Parse(Console.ReadLine());
                switch (odabir)
                {
                    case 0:
                        Console.WriteLine("Povratak na glavni meni . . .");
                        break;
                    case 1:
                        IspisSvihKursnihLista();
                        break;
                    case 2:
                        PrikazJedneKursneListe();
                        break;
                    case 3:
                        UnosNoveKursneListe();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda!\n\n");
                        break;
                }
            }
        }

        private static void UnosNoveKursneListe()
        {
            Console.Clear();
            Console.WriteLine("Sledece kursne liste vec postoje u bazi i ne mogu se menjati:");
            IspisSvihKursnihLista();
            //string danas = DateTime.Now.ToString("yyyy-MM-dd");
            Console.WriteLine("Unesite zeljeni datum u formatu GGGG-MM-DD: ");
            string unos = Console.ReadLine();
            
            Dictionary<string, double[]> noviDict = new Dictionary<string, double[]>();
            foreach (Valuta v in ValutaUI.listaValuta)
            {
                double[] kursevi = new double[3];
                Console.WriteLine($"Unesite KUPOVNI kurs za valutu {v.OznakaValute}: ");
                kursevi[0] = double.Parse(Console.ReadLine());
                Console.WriteLine($"Unesite PRODAJNI kurs za valutu {v.OznakaValute}: ");
                kursevi[1] = double.Parse(Console.ReadLine());
                Console.WriteLine($"Unesite SREDNJI kurs za valutu {v.OznakaValute}: ");
                kursevi[2] = double.Parse(Console.ReadLine());
                noviDict[v.OznakaValute] = kursevi;

            }
            KursnaLista novaKL = new KursnaLista(unos, noviDict);
            kursneListe.Add(novaKL);
            Console.WriteLine("Kursna lista uspesno dodata.");
            
        }

        private static void PrikazJedneKursneListe()
        {
            Console.WriteLine("Unesite datum kreiranja kursne liste u formatu GGGG-MM-DD:");
            string datumListe = Console.ReadLine();
            
            foreach (KursnaLista kl in kursneListe)
            {
                if (datumListe == kl.DatumListe)
                {
                    Console.WriteLine(kl);
                    break;
                }
            }
        }

        private static void IspisSvihKursnihLista()
        {
            Console.WriteLine("Ispis svih snimljenih kursnih lista");
            foreach (KursnaLista kl in kursneListe)
            {
                Console.WriteLine(kl.DatumListe);
            }
        }

        internal static void IspisOpcija()
        {
            Console.WriteLine("1 - Ispis svih kursnih lista po datumima");
            Console.WriteLine("2 - Prikaz odredjene kursne liste");
            Console.WriteLine("3 - Unos nove kursne liste");
            Console.WriteLine("0 - NAZAD");
        }

        internal static void SacuvajKursneListe(string adresa)
        {
            foreach (KursnaLista kl in kursneListe)
            {
                string punaPutanja = adresa + kl.DatumListe + ".csv";
                if (Directory.Exists(adresa))
                {
                    using (StreamWriter writer = new StreamWriter(punaPutanja, false, Encoding.UTF8))
                    {
                        foreach (KeyValuePair<string,double[]> dict in kl.ListaValuta)
                        {
                            writer.WriteLine($"{dict.Key},{dict.Value[0]},{dict.Value[1]},{dict.Value[2]}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Datoteka ne postoji ili putanja nije ispravna.");
                }
            }
        }

        internal static void UcitajFajloveKursnihLista(string adresa)
        {
            if (Directory.Exists(adresa))
            {
                string[] listaFajlova = Directory.GetFiles(adresa, "*.csv");
                foreach (string fajl in listaFajlova)
                {
                    string tempDatum = fajl.Substring(fajl.Length - 14, 10);
                    Dictionary<string, double[]> tempDict = new Dictionary<string, double[]>();
                    
                    using (StreamReader sr = new StreamReader(fajl))
                    {
                        string linija;
                        while ((linija = sr.ReadLine()) != null)
                        {
                            string[] tempLinija = linija.Split(',');
                            tempDict[tempLinija[0]] = new double[] { double.Parse(tempLinija[1]), double.Parse(tempLinija[2]), double.Parse(tempLinija[3]) };
                        }
                    }
                    kursneListe.Add(new KursnaLista(tempDatum, tempDict));
                }
            }
            else
            {
                Console.WriteLine("Nepostojeci direktorijum ili nedostaju fajlovi");
            }
            
        }
    }
}
