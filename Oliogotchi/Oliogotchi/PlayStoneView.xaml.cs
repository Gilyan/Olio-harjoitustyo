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
    public enum M
    {
        rock,
        paper,
        scissor
    }
    /// <summary>
    /// Interaction logic for PlayStoneView.xaml
    /// </summary>
    public partial class PlayStoneView : Window
    {
        public PlayStoneView(double x, double y)       // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;         
        }

        class Computer
        {
            Random rand = new Random();

            public M Choice()
            {
                M element = (M)rand.Next(3);
                return element;
            }

        }
        public void Test(M plaChoice, M comChoice)
        {
            string txt;
            int luku;
            if (plaChoice == comChoice)
            {
                txt = "Tasapeli! Olio valitsi saman esineen kuin sinä!";
                luku = 0;
                Run(txt, luku);
            }
            switch (plaChoice)
            {
                case M.rock:
                    if (plaChoice == M.rock && comChoice == M.scissor)
                    {
                        txt = "Voitit pelin! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus laskee 5 pistettä";
                        luku = 5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.rock && comChoice == M.paper)
                    {
                        txt = "Hävisit! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus nousee 20 pistettä";
                        luku = 20;
                        Run(txt, luku);
                    }
                    break;
                case M.paper:
                    if (plaChoice == M.paper && comChoice == M.rock)
                    {
                        txt = "Voitit pelin! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus laskee 5 pistettä";
                        luku = 5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.paper && comChoice == M.scissor)
                    {
                        txt = "Hävisit! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus nousee 20 pistettä";
                        luku = 20;
                        Run(txt, luku);
                    }
                    break;
                case M.scissor:
                    if (plaChoice == M.scissor && comChoice == M.paper)
                    {
                        txt = "Voitit pelin! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus laskee 5 pistettä";
                        luku = 5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.scissor && comChoice == M.rock)
                    {
                        txt = "Hävisit! Olio valtsi " + comChoice.ToString() + ", nyt olion iloisuus nousee 20 pistettä";
                        luku = 20;
                        Run(txt, luku);
                    }
                    break;
                default:
                    break;
            }
        }
        public void Run(string teksti, int luku)
        {
            txbGameInfo.Text = teksti;
            if (MessageBox.Show("Haluatko jatkaa?", "Oliogotchi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                btnRock.Visibility = System.Windows.Visibility.Visible;
                btnPaper.Visibility = System.Windows.Visibility.Visible;
                btnScissor.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.Close();
            }
        }
        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            btnPaper.Visibility = System.Windows.Visibility.Hidden;
            btnScissor.Visibility = System.Windows.Visibility.Hidden;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.rock;
            Test(playerChoice, computerChoice);
        }
        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            btnRock.Visibility = System.Windows.Visibility.Hidden;
            btnScissor.Visibility = System.Windows.Visibility.Hidden;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.paper;
            Test(playerChoice, computerChoice);


        }
        private void btnScissor_Click(object sender, RoutedEventArgs e)
        {
            btnRock.Visibility = System.Windows.Visibility.Hidden;
            btnPaper.Visibility = System.Windows.Visibility.Hidden;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.scissor;
            Test(playerChoice, computerChoice);

        }
    }
}
