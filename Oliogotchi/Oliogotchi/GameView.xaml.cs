/* **********************************************************
Oliogotchin pelisivu.

Toiminta: Pelisivun kautta voi 
- hoivata oliota pesemällä, silittämällä ja ruokkimalla sitä
- pääset minipeleihin
- pääset ohjeisiin ja päävalikkoon

Silitysääni: https://www.freesound.org/people/deleted_user_7146007/sounds/383266/
Ruokintaääni: https://www.freesound.org/people/Ondruska/sounds/360686/
Pesuääni: https://www.freesound.org/people/JohnsonBrandEditing/sounds/173930/
Evolveääni: https://www.freesound.org/people/newagesoup/sounds/347327/

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
using System.Windows.Media.Animation;

namespace Oliogotchi
{
    class Testi
    {
        // Tätä kautta kivipaperisaksetpelin pisteet välittyy olion iloisuuteen ja nälkäisyyteen
        static bool btnBackWasClicked = false;
        static bool whichGame = true;
        
        static int gamePoints;
        static int hungerPoints;
        public static bool WasClicked
        {
            get { return btnBackWasClicked; }
            set { btnBackWasClicked = value; }
        }
        public static bool WasGame
        {
            get { return whichGame; }
            set { whichGame = value; }
        }
        public static int GetPoints
        {
            get { return gamePoints; }
            set { gamePoints = value; }
        }
        public static int GetHunger
        {
            get { return hungerPoints; }
            set { hungerPoints = value; }
        }
    }
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        // Määritetään alkuarvoja muuttujille

        public static DispatcherTimer timer;
        private int easiness = 2;       // Timerin ajastinaika (s)
        private bool isNewGame;
        private bool gameIsPlayed = false;
        private bool isMeat;
        private bool whichAni = true;

        public string motivationText;

        Creature olio;
        Habitat tausta;
        Storyboard sb;
        Storyboard evo;

        string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        IFormatter formatter = new BinaryFormatter();           // Käytetään binäärimuotoa tallennuksessa

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

                    Animation();
                }
                catch (Exception ex)    // Jos tallennustiedostoa ei löydy, luodaan uusi peli alkuarvoilla
                {

                    MessageBox.Show("Couldn't find a save file. New creature created!");
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

            if (whichAni == true)
            { sb.Stop(); }
            else { evo.Stop(); }

            imgGrave.Visibility = System.Windows.Visibility.Visible;

            MessageBox.Show("Your pet died, so sad. Maybe you should get a rock?");

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
            Animation();

            prbHappiness.DataContext = olio.Happiness;
            prbHunger.DataContext = olio.Hunger;
            prbCleanliness.DataContext = olio.Cleanliness;
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

            CheckEvolve();
            ShowMotivationText();

            // Viedään arvot progress bareihin sekä footeriin
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness, DispatcherPriority.Background);
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness, DispatcherPriority.Background);
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger, DispatcherPriority.Background);

            // Testaamista varten olion statsi-info
            //txbFooter.Text = "olion ikä: " + olio.Age + ", onnellisuus: " + olio.Happiness + ", nälkä: " + olio.Hunger + ", puhtaus: " + olio.Cleanliness
            //                 + ", ympäristön puhtaus: " + tausta.Cleanliness + ", roskien määrä: " + tausta.Trash;

        }

        public void Animation()     // Täällä olion liikkuminen
        {
            try
            {
                imgGrave.Visibility = System.Windows.Visibility.Hidden;
                sb = this.FindResource(olio.Ani) as Storyboard;     // Luo Storyboradin, jossa on animaatio
                Storyboard.SetTarget(sb, this.imgOlio);
                sb.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Animation error: " + ex);
            }
        }

        public void CheckEvolve()
        {
            if (olio.Ani == "slimeAni")
            {
                if (olio.Happiness >= 80 && olio.Hunger >= 80 && olio.Cleanliness >= 80)
                {
                    btnEvolve.Visibility = System.Windows.Visibility.Visible;
                }

                else { btnEvolve.Visibility = System.Windows.Visibility.Hidden; }
            }

            else { btnEvolve.Visibility = System.Windows.Visibility.Hidden; }
        }

        public void ShowMotivationText() // Näyttää alapalkissa motivoivan (tai ei) tekstipätkän
        {
            if (olio.Hunger > 85 && olio.Happiness > 85 && olio.Cleanliness > 85)
            {
                txbFooter.Text = "Awesome! You'd make a fine parent for any creature!";
            }

            else if (olio.Hunger > 60 && olio.Happiness > 60 && olio.Cleanliness > 60)
            {
                txbFooter.Text = "Keep on clicking! You're doing a fine job!";
            }

            else if (olio.Hunger > 45 && olio.Happiness > 45 && olio.Cleanliness > 45)
            {
                txbFooter.Text = "Looks like your creature is hungry. Maybe you should give it a carrot or a piece of meat?";
            }

            else if (olio.Hunger > 30 && olio.Happiness > 30 && olio.Cleanliness > 30)
            {
                txbFooter.Text = "You could do better than this. Your creature's not gonna evolve if you keep this pace up.";
            }

            else if (olio.Hunger > 15 && olio.Happiness > 15 && olio.Cleanliness > 15)
            {
                txbFooter.Text = "Wake up, sleepy-eyes! Your pet needs your care!";
            }

            else if (olio.Hunger > 0 && olio.Happiness > 0 && olio.Cleanliness > 0)
            {
                txbFooter.Text = "WHAT'S THE MATTER WITH YOU?! YOUR CREATURE'S GONNA DIE SOON!";
            }
        }


        public void GetGamePoints() // Syöttää oliolle pisteet jos btnBackia on painettu PlayStoneViewissä
        {
            if (Testi.WasClicked)
            {
                Testi.WasClicked = false;
                olio.GamePoints(Testi.GetPoints);
                if (!Testi.WasGame)
                {
                    olio.HungerPoints(Testi.GetHunger); 
                }
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
                    GetGamePoints();
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

        private void btnEvolve_Click(object sender, RoutedEventArgs e)      // Evolvoituminen
        {     
            try
            {
                sb.Stop();      // Vanha Storyboard pysäytetään ennen poistamista
                olio.Evolve();

                evo = this.FindResource(olio.Ani) as Storyboard; // Korvataan vanha Storyboard uudella
                Storyboard.SetTarget(evo, this.imgOlio);
                evo.Begin();
                whichAni = false;

                mediaPlayer.Open(new Uri(@"../../Resources/sound/bam.mp3", UriKind.Relative));
                mediaPlayer.Play();
                mediaPlayer.Position = TimeSpan.Zero;   // Kelataan alkuun
            }
            catch (Exception ex)
            {
                MessageBox.Show("Evolve error: " + ex);
            }
            btnEvolve.Visibility = System.Windows.Visibility.Hidden;
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
                mediaPlayer.Position = TimeSpan.Zero;
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
