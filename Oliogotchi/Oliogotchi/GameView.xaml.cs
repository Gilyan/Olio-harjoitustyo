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


        public GameView()
        {
            InitializeComponent();
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
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }
    }
}
