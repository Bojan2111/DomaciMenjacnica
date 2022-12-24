using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomaciMenjacnica
{
    internal class Program
    {
        private static readonly string dataDir = "data";
        private static readonly string klDir = "kursneListe";
        private static readonly string valuteFajl = "valute.csv";
        private static readonly char sep = Path.DirectorySeparatorChar;
        private static string PodesiPutanju()
        {
            DirectoryInfo dir = new DirectoryInfo($".{sep}..{sep}..{sep}{dataDir}{sep}");
            return dir.FullName;
        }
        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;

            string putanjaDataDir = PodesiPutanju();
            ValutaUI.UcitajValuteIzFajla(putanjaDataDir + valuteFajl);
            KursnaListaUI.UcitajFajloveKursnihLista($"{putanjaDataDir}{klDir}{sep}");

            int odluka = -1;
            while (odluka != 0)
            {
                //Console.Clear();
                GlavniMeni();
                Console.WriteLine("Izaberi opciju:");
                odluka = int.Parse(Console.ReadLine());
                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        ValutaUI.ValuteMeni();
                        break;
                    case 2:
                        KursnaListaUI.KursneListeMeni();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda!\n\n");
                        break;
                }
            }

            KursnaListaUI.SacuvajKursneListe($"{putanjaDataDir}{klDir}{sep}");
            Console.WriteLine("Pritisnite bilo koji taster za izlaz . . .");
            Console.ReadKey(true);
        }

        private static void GlavniMeni()
        {
            string naslov = "M E NJ A C N I C A";
            int razmak = (Console.WindowWidth - naslov.Length) / 2;
            Console.WriteLine($"{new string(' ',razmak)}{naslov}");
            Console.WriteLine("1 - Lista dostupnih valuta");
            Console.WriteLine("2 - Rad sa kursnim listama");
            Console.WriteLine("0 - IZLAZ");
        }
    }
}
