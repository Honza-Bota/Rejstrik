using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Rejstrik
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonRead_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if((bool)openFileDialog.ShowDialog()) Text.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Stack<int>> rejstrik = new Dictionary<string, Stack<int>>();
            string text = Text.Text.ToLower();
            text = text.Replace(".", "");
            text = text.Replace(",", "");
            text = text.Replace("-", "");
            string[] slova = text.Split(' ');
            int radek = 0;

            foreach (var item in slova)
            {
                if (item == "\n") radek++;
                if (item.Length < 2) continue;

                if (!rejstrik.ContainsKey(item))
                {
                    rejstrik.Add(item, new Stack<int>());
                    rejstrik[item].Push(radek);
                }
                else if (rejstrik.ContainsKey(item))
                {
                    rejstrik[item].Push(radek);
                }

            }

            StringBuilder sb = new StringBuilder();
            StringBuilder nb = new StringBuilder();

            foreach (var item in rejstrik)
            {
                foreach (var item2 in item.Value)
                {
                    nb.Append(item2 + ",");
                }

                sb.AppendLine($"~ {item.Key} - {nb}");
                nb.Clear();
            }

            Rejstrik.Text = sb.ToString();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if ((bool)saveFileDialog.ShowDialog()) File.WriteAllText(saveFileDialog.FileName, Rejstrik.Text);
        }
    }
}
