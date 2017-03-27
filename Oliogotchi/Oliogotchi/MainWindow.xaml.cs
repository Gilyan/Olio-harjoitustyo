/* **********************************************************
Oliogotchin päävalikko.

Toiminta: Päävalikon kautta voi 
- jatkaa vanhaa peliä
- aloittaa uuden pelin
- lopettaa pelin
- mennä asetuksiin

Luotu 6.3.2017

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oliogotchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnJatka_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUusi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLopeta_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAsetukset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
