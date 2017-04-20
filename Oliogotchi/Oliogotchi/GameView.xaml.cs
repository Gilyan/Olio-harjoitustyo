/* **********************************************************
Oliogotchin pelisivu.

Toiminta: Pelisivun kautta voi 
- pelata peliä
- tänne juttuja lisättävä

Silitysääni: https://www.freesound.org/people/deleted_user_7146007/sounds/383266/
Ruokintaääni: https://www.freesound.org/people/Ondruska/sounds/360686/
Pesuääni: https://www.freesound.org/people/JohnsonBrandEditing/sounds/173930/

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
            get { return btnBackWasClicked; }
            set { btnBackWasClicked = value; }
        }
        public static int GetPoints
        {
            get { return stonePoints; }
            set { stonePoints = value; }
        }
    }
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        // Määritetään alkuarvoja muuttujille

        public static DispatcherTimer timer;
        private int easiness = 1;       // Timerin ajastinaika (ms)
        private bool isNewGame;
        private bool gameIsPlayed = false;
        private bool isMeat;

        Creature olio;
        Habitat tausta;

        string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        IFormatter formatter = new BinaryFormatter();           // Käytetään binäärimuotoa

        private MediaPlayer mediaPlayer = new MediaPlayer();    // Äänisoitin

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
            CreateCreature();
            CreateHabitat();

            Tallenna();
        }
        public void CreateHabitat()         // Luo uuden elinympäristön
        {
            tausta = new Habitat();
            tausta.SetDefault();
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
        public void CreateCreature()
        {
            olio = new Creature();
            olio.FillDefault();

            prbHappiness.DataContext = olio.Happiness;
            prbHunger.DataContext = olio.Hunger;
            prbCleanliness.DataContext = olio.Cleanliness;

            txbFooter.Text = "Uusi lemmikki luotu. Pidä siitä hyvää huolta!";
        }
        public void HabitatLiving()     // Taustan arvojen säätöä
        {
            tausta.GetMessy();           // Taustan puhtaus laskee timerin mukaan
            if (tausta.Cleanliness < 10)    // Roskien lisääminen
            {
                olio.GetDirty();
                olio.GetSad();
                prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness, DispatcherPriority.Background);
                prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness, DispatcherPriority.Background);
                tausta.AddTrash();
            }
            else if (tausta.Cleanliness == 20)
            {
                tausta.AddTrash();
            }
            else if (tausta.Cleanliness == 30)
            {
                tausta.AddTrash();
            }
            else if (tausta.Cleanliness == 40)
            {
                tausta.AddTrash();
            }
            else if (tausta.Cleanliness == 50)
            {
                tausta.AddTrash();
            }
        }
        public void Living()        // Olion eläminen
        {
            olio.GetSad();          // Olion kaikki arvot laskevat timerin mukaan
            olio.GetDirty();
            olio.GetHungry();
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
                olio.StonePoints(Testi.GetPoints);
            }
        }
        public void timer_Tick(object sender, EventArgs e) // Timer, missä tapahtuu olion "eläminen". Olio elää kunnes jokin arvo = 0
        {
            if (olio.Hunger > 0 && olio.Happiness > 0 && olio.Cleanliness > 0)
            {
                Living();
                HabitatLiving();
                if (gameIsPlayed)   // Jos minipeliä on pelattu, käydään suorittamassa aliohjelman toiminnot
                {
                    GetStonePoints();
                    gameIsPlayed = false;       // Alustaa tiedon, onko peliä pelattu
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

        private void btnOhjeet_Click(object sender, RoutedEventArgs e)    // Asetuksiin siirtyminen
        {
            Tallenna();
            timer.Stop();
            double x = this.Left;
            double y = this.Top;
            SettingsView settings = new SettingsView(x, y);
            settings.Show();
            this.Close();
        }

        private void btnGiveVeggie_Click(object sender, RoutedEventArgs e)  // Ruokitaan oliolle kasviksia
        {
            isMeat = false;
            if (olio.Hunger <= 100)
            {
                olio.Feed(isMeat);

                mediaPlayer.Open(new Uri(@"../../Resources/sound/munch.mp3", UriKind.Relative));
                mediaPlayer.Play();
                mediaPlayer.Position = TimeSpan.Zero;   // Kelataan alkuun
            }
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);
        }
        private void btnGiveMeat_Click(object sender, RoutedEventArgs e)    // Ruokitaan oliolle lihaa
        {
            if (olio.Hunger <= 100)
            {
                isMeat = true;
                olio.Feed(isMeat);

                mediaPlayer.Open(new Uri(@"../../Resources/sound/munch.mp3", UriKind.Relative));
                mediaPlayer.Play();
                mediaPlayer.Position = TimeSpan.Zero;   // Kelataan alkuun
            }
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);
        }

        private void btnShower_Click(object sender, RoutedEventArgs e)      // Olion peseminen
        {
            if (tausta.Cleanliness > 50 && tausta.Trash > 0)
            {
                tausta.Clean();
            }
            if (tausta.Cleanliness <= 100)
            {
                tausta.Clean();
            }
            if (olio.Cleanliness <= 100)
            {
                olio.Wash();
            }

            mediaPlayer.Open(new Uri(@"../../Resources/sound/water.mp3", UriKind.Relative));
            mediaPlayer.Play();
            mediaPlayer.Position = TimeSpan.Zero;   // Kelataan alkuun

            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness, DispatcherPriority.Background);
        }

        private void btnPet_Click(object sender, RoutedEventArgs e)         // Olion silittäminen
        {
            if (olio.Happiness <= 100)
            {
                olio.Brush();

                mediaPlayer.Open(new Uri(@"../../Resources/sound/purr.mp3", UriKind.Relative));
                mediaPlayer.Play();
                mediaPlayer.Position = TimeSpan.Zero;   // Kelataan alkuun
            }
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness, DispatcherPriority.Background);
        }

        private void btnPlayGame_Click(object sender, RoutedEventArgs e)    // Olion kanssa pelattavien minipelien valikkoon siirtyminen
        {
            gameIsPlayed = true;
            timer.Stop();
            double x = this.Left;
            double y = this.Top;
            PlayGamesView pelit = new PlayGamesView(x, y);
            pelit.Show();
            // this.Close();
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

        private void Storyboard_Completed(object sender, EventArgs e)
        {

        }
    }
}
