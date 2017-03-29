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

using System.Diagnostics;
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
using System.Windows.Threading;

namespace Oliogotchi
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        // Määritetään alkuarvoja muuttujille
        private int vegeCounter = 0;
        private int meatCounter = 0;
        private int cleanliness = 50;
        private int hunger = 50;
        private int happiness = 50;
        private int habitatTrash = 0;
        private int habitatCleanliness = 100;
        private DispatcherTimer timer;
        private int easiness = 1; //timerin ajastin aika ms

        Creature olio = new Creature();

        //public GameView()
        //{
        //    InitializeComponent();
        //}

        public GameView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;

            CreateCreature();       // Luo uuden lemmikkiolion - TODO: tehtävä erillinen haku tallennetulle oliolle
            CreateHabitat();        // Luo uuden elinympäristön - TODO: tehtävä erillinen haku tallennetulle elinympäristölle

            //GiveFood(); // TESTIVAIHE
            //Clean();    // TESTIVAIHE

            //timerin alustaminen
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, easiness);
            timer.Tick += new EventHandler(timer_Tick);

            timer.Start();
        }
        public void CreateHabitat()         // Luo uuden elinympäristön
        {
            Habitat tausta = new Habitat();
            tausta.Cleanliness = habitatCleanliness;
            tausta.Trash = habitatTrash;
        }
        public void CreateCreature()        // Luo uuden lemmikkiolion
        {
            
            olio.Age = 0;
            olio.Happiness = happiness;
            olio.Hunger = hunger;
            olio.Cleanliness = cleanliness;

            prbHappiness.DataContext = olio;
            prbHunger.DataContext = olio;
            prbCleanliness.DataContext = olio;

            txbFooter.Text = "Uusi lemmikki luotu. Pidä siitä hyvää huolta!";
        }
        public void GiveFood()
        {
            olio.Hunger += 40;      // TESTIVAIHE
            olio.Happiness -= 30;   // TESTIVAIHE
        }
        public void PlayRockPaperScissors()
        {

        }

        public void PlayFetchBall()
        {

        }
        public void Clean()
        {
            olio.Cleanliness -= 40;     // TESTIVAIHE
            olio.Happiness -= 20;       // TESTIVAIHE
        }
        public void Evolve()
        {

        }
        public void Die()
        {
            txbFooter.Text = "Lemmikkisi kuoli. Voi kuinka surullista. Hanki seuraavaksi vaikka kivi.";
        }
        public void Living() //olio elää ja päivittää tietoja progress bariin ja footteriin lukuarvot
        {
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = happiness--, DispatcherPriority.Background);
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = cleanliness--, DispatcherPriority.Background);
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = hunger--, DispatcherPriority.Background);
            txbFooter.Text = "onnellisuus: " + olio.Happiness.ToString() + ", nälkä: " + olio.Hunger.ToString() + ", puhtaus: " + olio.Cleanliness.ToString();
        }

        private void timer_Tick(object sender, EventArgs e) // timeri missä tapahtuu niin sanottu pelin "eläminen", elää kunnes jokin 0
        {
            if (olio.Hunger > 0 && olio.Happiness > 0 && olio.Cleanliness > 0)
            {
                Living();
            }
            else Die();
            
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;
            MainWindow menu = new MainWindow(x, y);
            menu.Show();
            this.Close();
        }

        private void btnPlayGame_Click(object sender, RoutedEventArgs e)  //Anna ruokaa napin tapahtuma
        {
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = hunger++, DispatcherPriority.Background);
        }
    }
}
