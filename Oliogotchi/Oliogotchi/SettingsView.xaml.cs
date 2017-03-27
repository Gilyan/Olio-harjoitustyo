/* **********************************************************
Oliogotchin asetussivu.

Toiminta: Asetuksien kautta voi 
- vaihtaa olion nimeä
- poistaa olion

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
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        public SettingsView(double x, double y)
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
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
