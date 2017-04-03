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
        bool kumpiPeli = true;
        public PlayGamesView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
            btnGoPlay.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnPlayStone_Click(object sender, RoutedEventArgs e)
        {
            kumpiPeli = true;
            btnGoPlay.Visibility = System.Windows.Visibility.Visible;
            txbGameInfo.Text = "Choose rock, paper or scissors. Rules are as always:\n- Rock beats scissors \n- Scissors bear paper \n- Paper beats rock \n\nIf you win, Olio loses 5 happiness. If Olio wins, Olio gains 20 happiness.";
        }

        private void btnPlayBall_Click(object sender, RoutedEventArgs e)
        {
            kumpiPeli = false;
            btnGoPlay.Visibility = System.Windows.Visibility.Visible;
            txbGameInfo.Text = "Tänne pallon heitto -pelin ohjeet";
        }

        private void btnGoPlay_Click(object sender, RoutedEventArgs e)
        {
            if (kumpiPeli)      // Kivi-sakset-paperi
            {
                double x = this.Left;
                double y = this.Top;
                PlayStoneView kiviSaksetPaperi = new PlayStoneView(x, y);
                kiviSaksetPaperi.Show();
                this.Close();
            }
            else                // Pallonheitto
            {
                double x = this.Left;
                double y = this.Top;
                PlayBallView haePallo = new PlayBallView(x, y);
                haePallo.Show();
                this.Close();
            }
        }
    }
}
