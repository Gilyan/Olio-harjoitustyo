using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            btnAgain.Visibility = System.Windows.Visibility.Hidden;     // Uudelleenpelausnappula piiloon toistaiseksi
        }

        private void ball_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ball.CaptureMouse();
            p = e.GetPosition(ImageHolder);
        }

        private void ball_MouseMove(object sender, MouseEventArgs e)
        {
            Point x = e.GetPosition(ImageHolder);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(ball) + (x.X - p.X));
                Canvas.SetTop(ball, Canvas.GetTop(ball) + (x.Y - p.Y));
            }
            p = x;
        }

        private void ball_MouseUp(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            ball.ReleaseMouseCapture();
        }
        int i, k, t;

        private void btnAgain_Click(object sender, RoutedEventArgs e)       // Pelataan uudestaan!
        {
            // tänne pelin "resetointi"
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long ball1Pos = Convert.ToInt64(ball.GetValue(Canvas.TopProperty));
            if (ball1Pos < 485)
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                {
                    Canvas.SetTop(ball, ball1Pos + 5);
                }
            }
            long slime1Pos = Convert.ToInt64(slime.GetValue(Canvas.LeftProperty));
            if (slime1Pos >= 700)
            {
                Canvas.SetLeft(slime, 690);
                t = 1;
            }
            else if (slime1Pos <= -10)
            {
                Canvas.SetLeft(slime, 0);
                t = 0;
                i = 0;
            }
            else
            {
                if (i == 0)
                {
                    k = 0;
                }
                i += 10;
                if (i >= 600 || t == 1)
                {
                    k += 10;
                    Canvas.SetLeft(slime, slime1Pos - 8);
                    if (k == 400)
                    {
                        i = 0;
                    }
                }
                else
                {
                    Canvas.SetLeft(slime, slime1Pos + 8);
                }
            }
            txbFooter.Text = "i: " + i.ToString() + " k: " + k.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GameView.timer.Start();
            this.Close();
        }
    }
}
