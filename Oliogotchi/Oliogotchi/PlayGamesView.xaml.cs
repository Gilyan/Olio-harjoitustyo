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
    /// Interaction logic for PlayGamesView.xaml
    /// </summary>
    public partial class PlayGamesView : Window
    {
        //public PlayGamesView()
        //{
        //    InitializeComponent();
        //}
        public PlayGamesView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
            btnGoPlay.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnPlayStone_Click(object sender, RoutedEventArgs e)
        {
            btnGoPlay.Visibility = System.Windows.Visibility.Visible;
            txbGameInfo.Text = "Tänne kivi-sakset-paperi -pelin ohjeet";
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            btnGoPlay.Visibility = System.Windows.Visibility.Visible;
            txbGameInfo.Text = "Tänne pallon heitto -pelin ohjeet";
        }
    }
}
