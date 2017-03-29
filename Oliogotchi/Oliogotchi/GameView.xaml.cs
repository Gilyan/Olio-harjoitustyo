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
        private int vegeCounter = 0;
        private int meatCounter = 0;
        private int cleanliness = 50;
        private int hunger = 50;
        private int happiness = 50;
        private int habitatTrash = 0;
        private int habitatCleanliness = 100;


        public GameView()
        {
            InitializeComponent();
            CreateCreature();
            CreateHabitat();
            
        }

        public GameView(double x, double y)
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
        }
        public void CreateHabitat()
        {
            Habitat tausta = new Habitat();
            tausta.Cleanliness = habitatCleanliness;
            tausta.Trash = habitatTrash;
        }
        public void CreateCreature()
        {
            Creature olio = new Creature();
            olio.Age = 0;
            olio.Happiness = happiness;
            olio.Hunger = hunger;
            olio.Cleanliness = cleanliness;

            prbHappiness.DataContext = olio;
            prbHunger.DataContext = olio;
        }
        public void GiveFood()
        {

        }
        public void PlayRockPaperScissors()
        {

        }

        public void PlayFetchBall()
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
