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
        public static void Test(M plaChoice, M comChoice)
        {
            string txt;
            if (plaChoice == comChoice)
            {
                txt = "Tasapeli!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.paper && comChoice == M.rock)
            {
                txt = "Voitit pelin!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.paper && comChoice == M.scissor)
            {
                txt = "Hävisit!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.rock && comChoice == M.scissor)
            {
                txt = "Voitit pelin!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.rock && comChoice == M.paper)
            {
                txt = "Hävisit!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.scissor && comChoice == M.paper)
            {
                txt = "Voitit pelin!";
                MessageBox.Show(txt);
            }
            else if (plaChoice == M.scissor && comChoice == M.rock)
            {
                txt = "Hävisit!";
                MessageBox.Show(txt);
            }

        }
        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.rock;
            Test(playerChoice, computerChoice);

        }
        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.paper;
            Test(playerChoice, computerChoice);

        }
        private void btnScissor_Click(object sender, RoutedEventArgs e)
        {
            Computer Comp = new Computer();
            M computerChoice = Comp.Choice();
            M playerChoice = M.scissor;
            Test(playerChoice, computerChoice);

        }
    }
}
