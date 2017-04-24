/* **********************************************************
Oliogotchin ohjesivu.

Toiminta: Täällä on ohjeet pelille.
Main Menu -napista pääsee takaisin päävalikkoon.

Luotu 27.3.2017

Minttu Mäkäläinen K8517
Kioto Hiirola K8252
Joona Hautamäki K1647 
@ JAMK 
********************************************************** */

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
using System.Windows.Shapes;

namespace Oliogotchi
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        //public SettingsView()
        //{
        //    InitializeComponent();
        //}

        public SettingsView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;

            txbOhjeet.Text = ("Main goal of this game is to take care of your Oliogotchi." +
                "\r\nIf you care for your critter well enough, you can evolve it to" +
                "\r\nsomething amazing.");

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;
            MainWindow menu = new MainWindow(x, y);
            menu.Show();
            this.Close();
        }
    }
}
