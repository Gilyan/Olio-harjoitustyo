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
        public int happiness = 50;
        private int habitatTrash = 0;
        private int habitatCleanliness = 100;
        public static DispatcherTimer timer;
        private int easiness = 1;       // Timerin ajastinaika (ms)
        private bool isNewGame;

        Creature olio;
        Habitat tausta;

        string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        IFormatter formatter = new BinaryFormatter();           // Käytetään binäärimuotoa

        public GameView(double x, double y, bool isnewgame)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;

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
                olio = new Creature();
                tausta = new Habitat();

                CreateCreature();               // Luo uuden lemmikkiolion alkuarvoilla
                CreateHabitat();                // Luo uuden elinympäristön alkuarvoilla
            }
            else          // Ladataan tallennetut arvot tiedostosta
            {
                //Stream lueTiedostosta;

                //lueTiedostosta = new FileStream(myDocPath + @"olio.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                //olio = (Creature)formatter.Deserialize(lueTiedostosta);       // Luetaan tiedostosta ja muunnetaan objektiksi
                //lueTiedostosta.Close();         // Suljetaan tiedosto

                //lueTiedostosta = new FileStream(myDocPath + @"tausta.bin", FileMode.Open, FileAccess.Read, FileShare.None);
                //tausta = (Habitat)formatter.Deserialize(lueTiedostosta);
                //lueTiedostosta.Close();

                olio = new Creature();
                tausta = new Habitat();

                CreateCreature();               // Luo uuden lemmikkiolion alkuarvoilla
                CreateHabitat();                // Luo uuden elinympäristön alkuarvoilla
            }
        }
        public void CreateHabitat()         // Luo uuden elinympäristön
        {
            tausta.Cleanliness = habitatCleanliness;
            tausta.Trash = habitatTrash;
        }
        public void CreateCreature()        // Luo uuden lemmikkiolion
        {
            
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
        public void Die()
        {
            txbFooter.Text = "Lemmikkisi kuoli. Voi kuinka surullista. Hanki seuraavaksi vaikka kivi.";
        }
        public void HabitatLiving()
        {
            tausta.Cleanliness--;
            if (tausta.Cleanliness < 10)
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
        public void Living()        // Olio elää ja päivittää tietoja progress bariin ja footteriin lukuarvot
        {
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = olio.Happiness--, DispatcherPriority.Background);
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = olio.Cleanliness--, DispatcherPriority.Background);
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = olio.Hunger--, DispatcherPriority.Background);
            txbFooter.Text = "onnellisuus: " + olio.Happiness + ", nälkä: " + olio.Hunger + ", puhtaus: " + olio.Cleanliness
                             + ", ympäristön puhtaus: " + tausta.Cleanliness + ", roskien määrä: " + tausta.Trash;
        }

        public void timer_Tick(object sender, EventArgs e) // Timer, missä tapahtuu olion "eläminen", elää kunnes jokin arvo = 0
        {
            if (olio.Hunger > 0 && olio.Happiness > 0 && olio.Cleanliness > 0)
            {
                Living();
                HabitatLiving();
            }
            else Die();  
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Tallenna();
            double x = this.Left;
            double y = this.Top;
            MainWindow menu = new MainWindow(x, y);
            menu.Show();
            this.Close();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Tallenna();
            double x = this.Left;
            double y = this.Top;
            SettingsView settings = new SettingsView(x, y);
            settings.Show();
            this.Close();
        }

        private void btnGiveVeggie_Click(object sender, RoutedEventArgs e)
        {
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = hunger++, DispatcherPriority.Background);
            vegeCounter++;
        }
        private void btnGiveMeat_Click(object sender, RoutedEventArgs e)  //Anna ruokaa napin tapahtuma
        {
            prbHunger.Dispatcher.Invoke(() => prbHunger.Value = hunger++, DispatcherPriority.Background);
            meatCounter++;
        }

        private void btnShower_Click(object sender, RoutedEventArgs e)
        {
            prbCleanliness.Dispatcher.Invoke(() => prbCleanliness.Value = cleanliness++, DispatcherPriority.Background);
        }

        private void btnPet_Click(object sender, RoutedEventArgs e)
        {
            prbHappiness.Dispatcher.Invoke(() => prbHappiness.Value = happiness++, DispatcherPriority.Background);
        }

        private void btnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            double x = this.Left;
            double y = this.Top;
            PlayGamesView pelit = new PlayGamesView(x, y);
            pelit.Show();
            //this.Close();
        }

        private void Tallenna()
        {
            Stream tallennaTiedostoon;

            tallennaTiedostoon = new FileStream(myDocPath + @"olio.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(tallennaTiedostoon, olio);      // Kirjoitetaan olio tiedostoon
            tallennaTiedostoon.Close();                         // Suljetaan tiedosto

            tallennaTiedostoon = new FileStream(myDocPath + @"tausta.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(tallennaTiedostoon, tausta);    // Kirjoitetaan tausta tiedostoon
            tallennaTiedostoon.Close();
        }
    }
}
