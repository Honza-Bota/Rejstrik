using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RejstrikConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region pozdrav a načtení jména souboru pro tvorbu rejstříku
            Console.WriteLine("Vítejte v programu pro tvorbu rejstříku z textu");
            Console.WriteLine("--------------------------------------------------");
            string file;
            do
            {            
                Console.Write("Zadejte jméno souboru pro načtení textu <name.txt>: ");
                file = Console.ReadLine();

            } while (!File.Exists(file));

            Console.Clear();
            Console.WriteLine("Probíhá tvorba rejstříku....");
            #endregion

            #region načtení souboru a tvorba rejstříku
            Dictionary<string, Stack<int>> rejstrik = new Dictionary<string, Stack<int>>();
            string[] slova;
            int radek = 1;

            StreamReader sr = new StreamReader(file);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.ToLower().Replace(".", " ").Replace(",", " ").
                    Replace("'", " ").Replace("/", " ").Replace(":", " ").
                    Replace(")", " ").Replace("(", " ").Replace(";", " ").
                    Replace("[", " ").Replace("]", " ").Replace("*", " ").
                    Replace("_", " ").Replace("@", " ").Replace("#", " ");
                slova = line.Split(' ');

                foreach (var slovo in slova)
                {
                    if (slovo.Length <= 2 || slovo == "and" || slovo == "the" || slovo == "and") continue;

                    if (!rejstrik.ContainsKey(slovo))
                    {
                        rejstrik.Add(slovo, new Stack<int>());
                        rejstrik[slovo].Push(radek);
                    }
                    else if (rejstrik.ContainsKey(slovo))
                    {
                        rejstrik[slovo].Push(radek);
                    }

                }
                radek++;
            }
            sr.Close();
            #endregion

            #region příprava pro výpis rejstříku
            StringBuilder sb = new StringBuilder();
            StringBuilder nb = new StringBuilder();

            foreach (var item in rejstrik)
            {
                foreach (var item2 in item.Value)
                {
                    nb.Append(item2 + "; ");
                }
                nb.Remove(nb.Length - 2, 1);

                sb.AppendLine($"~ {item.Key} - {nb}");
                sb.AppendLine();
                nb.Clear();
            }
            #endregion

            #region uložení
            Console.Clear();

            Console.Write("Zadejte jméno souboru pro uložení rejstříku <name.txt>: ");
            string saveFile = Console.ReadLine();

            File.WriteAllText(saveFile, sb.ToString());

            Console.WriteLine("Rejstřík byl vytvořen a uložen. Pro ukončení stiskni Enter.");
            Console.ReadLine();
            #endregion
        }
    }
}
