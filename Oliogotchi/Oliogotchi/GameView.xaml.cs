/* **********************************************************
Oliogotchin pelisivu.

Toiminta: Pelisivun kautta voi 
- pelata peliä
- tänne juttuja lisättävä

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
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private int clean;
        private int hunger;
        private int happiness;
        private int habitatTrash;
        private int habitatCleanliness;
        private int muuttuja;


        public GameView()
        {
            InitializeComponent();
        }

        public GameView(double x, double y)
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
        }
        public void GiveFood()
        {

        }
        public void Play()
        {

        }
        public void Clean()
        {

        }
        public void Evolve()
        {

        }
        public void Die()
        {

        }
        public void ChangeName()
        {

        }
        public void Living()
        {

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
