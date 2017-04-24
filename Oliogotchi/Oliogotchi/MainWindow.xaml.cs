/* **********************************************************
Oliogotchin päävalikko.

Toiminta: Päävalikon kautta voi 
- jatkaa vanhaa peliä (load)
- aloittaa uuden pelin (start)
- lopettaa pelin (quit)
- mennä ohjesivulle (instructions)

Luotu 6.3.2017

Minttu Mäkäläinen K8517
Kioto Hiirola K8252
Joona Hautamäki K1647
@ JAMK 
********************************************************** */

using System;
using System.Collections.Generic;
using System.IO;
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

namespace Oliogotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(double x, double y)       // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
        }

        private void btnJatka_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;
            GameView game = new GameView(x, y, false);
            game.Show();
            this.Close();
        }

        void btnUusi_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;
            GameView game = new GameView(x, y, true);
            game.Show();
            this.Close();
        }

        private void btnLopeta_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOhjeet_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;
            SettingsView settings = new SettingsView(x, y);
            settings.Show();
            this.Close();
        }
    }
}
