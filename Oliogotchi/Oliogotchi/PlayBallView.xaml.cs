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
    /// Interaction logic for PlayBallView.xaml
    /// </summary>
    public partial class PlayBallView : Window
    {
        Point p = new Point();
        DispatcherTimer timer = new DispatcherTimer();


        public PlayBallView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
            this.Left = x;
            this.Top = y;
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
            Img.CaptureMouse();
            p = e.GetPosition(ImageHolder);
        }

        private void Img_MouseMove(object sender, MouseEventArgs e)
        {
            Point x = e.GetPosition(ImageHolder);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(Img, Canvas.GetLeft(Img) + (x.X - p.X));
                Canvas.SetTop(Img, Canvas.GetTop(Img) + (x.Y - p.Y));
            }
            p = x;
        }

        private void Img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            Img.ReleaseMouseCapture();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            long ball1Pos = Convert.ToInt64(Img.GetValue(Canvas.TopProperty));
            if (ball1Pos > 485)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
                Canvas.SetTop(Img, ball1Pos + 5);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
