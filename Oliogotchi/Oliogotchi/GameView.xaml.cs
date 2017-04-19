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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Oliogotchi
{
    // Tätä kautta kivipaperisaksetpelin pisteet välittyy olion iloisuuteen
    class Testi
    {
        static bool btnBackWasClicked = false;
        static int stonePoints;
        public static bool WasClicked
        {
            get
            {
                return btnBackWasClicked;
            }
            set
            {
                btnBackWasClicked = value;
            }
        }
        public static int GetPoints
        {
            get
            {
                return stonePoints;
            }
            set
            {
                stonePoints = value;
            }
        }
    }
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    { 
        // Määritetään alkuarvoja muuttujille
        private int vegeCounter;
        private int meatCounter;
        private int cleanliness;
        private int hunger;
        public int happiness;
        private int habitatTrash;
        private int habitatCleanliness;
        public static DispatcherTimer timer;
        private int easiness = 1;       // Timerin ajastinaika (ms)
        private bool isNewGame;
        private bool gameIsPlayed = false;

        Creature olio;
        Habitat tausta;

        string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        IFormatter formatter = new BinaryFormatter();           // Käytetään binäärimuotoa

        public GameView(double x, double y, bool isnewgame)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;

            btnEvolve.Visibility = System.Windows.Visibility.Hidden;    // Piilotetaan kehitysnappi toistaiseksi

            isNewGame = isnewgame;      // Tuodaan tieto napin painalluksesta MainWindowin puolelta paikalliseen muuttujaan

            CheckIfNew();
           
            timer = new DispatcherTimer();          // Timerin alustaminen
            timer.Interval = new TimeSpan(0, 0, 0, easiness);
            timer.Tick += new EventHandler(timer_Tick);

            timer.Start();
        }

        private void CheckIfNew()       // Tarkistetaan onko valittu uusi peli, vai ladataanko vanha tiedostosta
        {
            if (isNewGame)               // Luodaan uusi peli
            {
                CreateNewGame();        
            }
            else          // Ladataan tallennetut arvot tiedostosta
            {
                Stream lueTiedostosta;

                try
                {
                    lueTiedostosta = new FileStream(myDocPath + @"olio.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                    olio = (Creature)formatter.Deserialize(lueTiedostosta);       // Luetaan tiedostosta ja muunnetaan objektiksi
                    lueTiedostosta.Close();         // Suljetaan tiedosto

                    lueTiedostosta = new FileStream(myDocPath + @"tausta.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                    tausta = (Habitat)formatter.Deserialize(lueTiedostosta);
                    lueTiedostosta.Close();
                }
                catch (Exception ex)    // Jos tallennustiedostoa ei löydy, luodaan uusi peli alkuarvoilla
                {

                    MessageBox.Show("Tallennustiedostoa ei löytynyt, luotu uusi peli!");
                    CreateNewGame();
                }
            }
        }

        public void CreateNewGame()         // Luo uuden olion alkuarvoilla
        {
            olio = new Creature();
            tausta = new Habitat();

            CreateCreature();
            CreateHabitat();

            Tallenna();
        }
        public void CreateHabitat()         // Luo uuden elinympäristön
        {
            habitatTrash = 0;               // Alustetaan arvot
            habitatCleanliness = 100;

            tausta.Cleanliness = habitatCleanliness;
            tausta.Trash = habitatTrash;
        }
        public void CreateCreature()        // Luo uuden lemmikkiolion
        {
            vegeCounter = 0;                // Alustetaan arvot
            meatCounter = 0;
            cleanliness = 50;
            hunger = 50;
            happiness = 50;

            olio.Age = 0;
            olio.Happiness = happiness;
            olio.Hunger = hunger;
            olio.Cleanliness = cleanliness;
            olio.Image = "Resources/slime/";

            prbHappiness.DataContext = olio;
            prbHunger.DataContext = olio;
            prbCleanliness.DataContext = olio;

            txbFooter.Text = "Uusi lemmikki luotu. Pidä siitä hyvää huolta!";
        }
        public void Die()   // Lemmikki kuolee, footeriin infoa asiasta
        {
            timer.Stop();
            MessageBox.Show("Lemmikkisi kuoli.Voi kuinka surullista. Hanki seuraavaksi vaikka kivi.");

            double x = this.Left;
            double y = this.Top;
            MainWindow menu = new MainWindow(x, y);
            menu.Show();
            this.Close();
        }
        public void HabitatLiving()     // Taustan arvojen säätöä
        {
            tausta.Cleanliness--;           // Taustan puhtaus laskee timerin mukaan
            if (tausta.Cleanliness < 10)    // Roskien lisääminen
            {
                prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = cleanliness--, DispatcherPriority.Background);
                prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = happiness--, DispatcherPriority.Background);
                tausta.Trash++;
            }
            else if (tausta.Cleanliness == 20)
            {
                tausta.Trash++;
            }
            else if (tausta.Cleanliness == 30)
            {
                tausta.Trash++;
            }
            else if (tausta.Cleanliness == 40)
            {
                tausta.Trash++;
            }
            else if (tausta.Cleanliness == 50)
            {
                tausta.Trash++;
            }
        }
        public void Living()        // Olio elää ja päivittää tietoja progress bariin ja footteriin syötetään lukuarvot
        {
            olio.Happiness--;       // Olion kaikki arvot laskevat timerin mukaan
            olio.Cleanliness--;
            olio.Hunger--;
            olio.Age++;
            // Viedään arvot progress bareihin sekä footeriin
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness, DispatcherPriority.Background);
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness, DispatcherPriority.Background);
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);
            txbFooter.Text = "olion ikä: " + olio.Age + ", onnellisuus: " + olio.Happiness + ", nälkä: " + olio.Hunger + ", puhtaus: " + olio.Cleanliness
                             + ", ympäristön puhtaus: " + tausta.Cleanliness + ", roskien määrä: " + tausta.Trash;
        }
        public void GetStonePoints() // Syöttää oliolle pisteet jos btnBackia on painettu PlayStoneViewissä
        {
            if (Testi.WasClicked)
            {
                Testi.WasClicked = false;
                if (olio.Happiness + Testi.GetPoints > 0 && olio.Happiness + Testi.GetPoints < 100)
                {
                    olio.Happiness += Testi.GetPoints;
                }
                else if (olio.Happiness + Testi.GetPoints >= 100)
                {
                    olio.Happiness = 100;
                }
                else
                {
                    olio.Happiness = 0;
                }
            }
        }
        public void timer_Tick(object sender, EventArgs e) // Timer, missä tapahtuu olion "eläminen". Olio elää kunnes jokin arvo = 0
        {
            if (olio.Hunger > 0 && olio.Happiness > 0 && olio.Cleanliness > 0)
            {
                Living();
                HabitatLiving();
                if (gameIsPlayed) //jos peliä on pelattu käy aliohjelmalta suorittamassa toiminnot ja alustaa tiedon onko peliä pelattu
                {
                    GetStonePoints();
                    gameIsPlayed = false;
                }
            }
            else Die();  
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)    // Päävalikkoon siirtyminen
        {
            Tallenna();
            timer.Stop();
            double x = this.Left;
            double y = this.Top;
            MainWindow menu = new MainWindow(x, y);
            menu.Show();
            this.Close();
        }

        //private void btnSettings_Click(object sender, RoutedEventArgs e)    // Asetuksiin siirtyminen
        //{
        //    Tallenna();
        //    timer.Stop();
        //    double x = this.Left;
        //    double y = this.Top;
        //    SettingsView settings = new SettingsView(x, y);
        //    settings.Show();
        //    this.Close();
        //}

        private void btnGiveVeggie_Click(object sender, RoutedEventArgs e)  // Ruokitaan oliolle kasviksia
        {
            if (olio.Hunger <= 100)
            {
                olio.Hunger++;
                vegeCounter++;
            }
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);
        }
        private void btnGiveMeat_Click(object sender, RoutedEventArgs e)    // Ruokitaan oliolle lihaa
        {
            if (olio.Hunger <= 100)
            {
                olio.Hunger++;
                meatCounter++;
            }
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);
        }

        private void btnShower_Click(object sender, RoutedEventArgs e)      // Olion peseminen
        {
            if (tausta.Cleanliness > 50 && tausta.Trash > 0)
            {
                tausta.Trash--;
            }
            if (tausta.Cleanliness <= 100)
            {
                tausta.Cleanliness++;
            }
            if (olio.Cleanliness <= 100)
            {
                olio.Cleanliness += 2;
            }
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness, DispatcherPriority.Background);
        }

        private void btnPet_Click(object sender, RoutedEventArgs e)         // Olion silittäminen
        {
            if (olio.Happiness <= 100)
            {
                olio.Happiness++;
            }
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness, DispatcherPriority.Background);
        }

        private void btnPlayGame_Click(object sender, RoutedEventArgs e)    // Olion kanssa pelattavien pelien valikkoon siirtyminen
        {
            gameIsPlayed = true;
            timer.Stop();
            double x = this.Left;
            double y = this.Top;
            PlayGamesView pelit = new PlayGamesView(x, y);
            pelit.Show();
            //this.Close();
        }

        private void Tallenna()     // Tallennetaan olion sekä taustan tiedot binääritiedostoon
        {
            Stream tallennaTiedostoon;

            tallennaTiedostoon = new FileStream(myDocPath + @"olio.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(tallennaTiedostoon, olio);
            tallennaTiedostoon.Close();     // Suljetaan tiedosto

            tallennaTiedostoon = new FileStream(myDocPath + @"tausta.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(tallennaTiedostoon, tausta); 
            tallennaTiedostoon.Close();
        }
    }
}
