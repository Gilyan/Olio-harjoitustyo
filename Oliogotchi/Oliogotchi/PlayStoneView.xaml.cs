/* **********************************************************
Kivi-sakset-paperi -peli.

Toiminta: Voit pelata kivi-sakset-paperi -peliä olion kanssa.
Klikkaa haluamaasi vaihtoehtoa.
Olion valinta random-generaattorin mukaan.

Jos Olio voittaa -> arvoa "luku" nostetaan 20:llä.
Jos Olio häviää -> arvoa "luku" lasketaan 5:llä.
"luku" lisätään pääpelin arvoon Happiness joka kierroksen lopussa.

Luotu 3.4.2017

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
    public enum M       // Määritetään kontrollit
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

            btnAgain.Visibility = System.Windows.Visibility.Hidden;     // Piilotetaan valikkonapit (Again, Back)
            btnBack.Visibility = System.Windows.Visibility.Hidden;
        }

        class Computer
        {
            Random rand = new Random();

            public M Choice()               // Randomgeneraattori Olion valintaa varten
            {
                M element = (M)rand.Next(3);
                return element;
            }

        }
        public void Test(M plaChoice, M comChoice)
        {
            string txt;
            int luku;
            if (plaChoice == comChoice)     // Tasapeli
            {
                txt = "DRAW! \n\nOlio chose the same as you!";
                luku = 0;
                Run(txt, luku);
            }
            switch (plaChoice)
            {
                case M.rock:        // Valittu vaihtoehto : kivi
                    if (plaChoice == M.rock && comChoice == M.scissor)
                    {
                        txt = "YOU WON! \n\nOlio chose " + comChoice.ToString() + ", Olio loses 5 happiness points.";
                        luku = -5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.rock && comChoice == M.paper)
                    {
                        txt = "YOU LOST! \n\nOlio chose " + comChoice.ToString() + ", Olio gains 20 happiness points.";
                        luku = 20;
                        Run(txt, luku);
                    }
                    break;
                case M.paper:       // Valittu vaihtoehto : paperi
                    if (plaChoice == M.paper && comChoice == M.rock)
                    {
                        txt = "YOU WON! \n\nOlio chose " + comChoice.ToString() + ", Olio loses 5 happiness points.";
                        luku = -5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.paper && comChoice == M.scissor)
                    {
                        txt = "YOU LOST! \n\nOlio chose " + comChoice.ToString() + ", Olio gains 20 happiness points.";
                        luku = 20;
                        Run(txt, luku);
                    }
                    break;
                case M.scissor:     // Valittu vaihtoehto : sakset
                    if (plaChoice == M.scissor && comChoice == M.paper)
                    {
                        txt = "YOU WON! \n\nOlio chose " + comChoice.ToString() + ", Olio loses 5 happiness points.";
                        luku = -5;
                        Run(txt, luku);
                    }
                    else if (plaChoice == M.scissor && comChoice == M.rock)
                    {
                        txt = "YOU LOST! \n\nOlio chose " + comChoice.ToString() + ", Olio gains 20 happiness points.";
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
            points += luku;
            txbGameInfo.Text = teksti;
            btnAgain.Visibility = System.Windows.Visibility.Visible;    // Valikkonapit aktiiviseksi (Again, Back)
            btnBack.Visibility = System.Windows.Visibility.Visible;
        }
        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            btnPaper.Visibility = System.Windows.Visibility.Hidden;     // Piilotetaan ei-valitut vaihtoehdot
            btnScissor.Visibility = System.Windows.Visibility.Hidden;
            btnPaper.IsEnabled = false;
            btnRock.IsEnabled = false;
            btnScissor.IsEnabled = false;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.rock;
            Test(playerChoice, computerChoice);
        }
        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            btnRock.Visibility = System.Windows.Visibility.Hidden;      // Piilotetaan ei-valitut vaihtoehdot
            btnScissor.Visibility = System.Windows.Visibility.Hidden;
            btnPaper.IsEnabled = false;
            btnRock.IsEnabled = false;
            btnScissor.IsEnabled = false;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.paper;
            Test(playerChoice, computerChoice);


        }
        private void btnScissor_Click(object sender, RoutedEventArgs e)
        {
            btnRock.Visibility = System.Windows.Visibility.Hidden;      // Piilotetaan ei-valitut vaihtoehdot
            btnPaper.Visibility = System.Windows.Visibility.Hidden;
            btnPaper.IsEnabled = false;
            btnRock.IsEnabled = false;
            btnScissor.IsEnabled = false;
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.scissor;
            Test(playerChoice, computerChoice);

        }

        private void btnAgain_Click(object sender, RoutedEventArgs e)
        {
            btnPaper.IsEnabled = true;
            btnRock.IsEnabled = true;
            btnScissor.IsEnabled = true;
            btnRock.Visibility = System.Windows.Visibility.Visible;     // Kaikki vaihtoehdot näkyville valintaa varten
            btnPaper.Visibility = System.Windows.Visibility.Visible;
            btnScissor.Visibility = System.Windows.Visibility.Visible;
            btnAgain.Visibility = System.Windows.Visibility.Hidden;     // Piilotetaan valikkonapit (Again, Back)
            btnBack.Visibility = System.Windows.Visibility.Hidden;
            txbGameInfo.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)    // Siirrytään takaisin pääpelin puolelle
        {
            GameView.timer.Start();
            this.Close();
        }
    }
}
