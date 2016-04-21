using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Elka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();

        string toys = File.ReadAllText("toys.txt");

        public MainWindow()
        {
            InitializeComponent();

            UpdatePercentLabel();
        }

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            int height;

            if (!int.TryParse(Height_TextBox.Text, out height))
                return;

            if (height < 2)
                return;

            var elko_File = String.Format("Ёлка высота = {0}.txt", height);

            File.WriteAllText(elko_File, GetElka(height));

            MessageBox.Show(String.Format("Сохранено в файл \"{1}\"", height, elko_File));
        }

        string GetElka(int height)
        {
            var elka = "★".PadLeft(height) + Environment.NewLine;

            for (int i = height - 1; i > 0; i--)
            {
                var starsCount = (height - i + 1) * 2 - 1;
                var levelWidth = height + height - i;

                var newLevel = "*".PadLeft(starsCount, '*').PadLeft(levelWidth) + Environment.NewLine;
                elka += PutToys(newLevel);
            }
                

            return elka;
        }

        string PutToys(string level)
        {
            var toyedLevel = "";

            //var toys = "@J҉ዕ፠ᐑO";
            var toysPercent = (int)ToysPercent_Slider.Value;          

            foreach(char ch in level)
            {
                if(ch == '*')
                {
                    if (toysPercent >= rnd.Next(0, 101))
                    {
                        var newChar = toys[rnd.Next(0, toys.Length)];

                        toyedLevel += newChar.ToString();
                    }
                    else
                    {
                        toyedLevel += "*";
                    }                    
                }
                else
                {
                    toyedLevel += ch.ToString();
                }
            }

            return toyedLevel;
        }

        void UpdatePercentLabel()
        {
            if (Percent_Label == null)
                return;

            var percent = (int)ToysPercent_Slider.Value;

            //var text = String.Format("{0}% игрушек", percent);

            Percent_Label.Content = String.Format("{0}% игрушек", percent);
        }

        private void ToysPercent_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdatePercentLabel();
        }
    }
}
