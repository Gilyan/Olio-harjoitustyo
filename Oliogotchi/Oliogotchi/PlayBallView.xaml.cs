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
        }

        private void ball_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
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
            //timer.Start();
            ball.ReleaseMouseCapture();
        }
        int i, k, t;
        void timer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            long ball1Pos = Convert.ToInt64(ball.GetValue(Canvas.TopProperty));
            long ball2Pos = Convert.ToInt64(ball.GetValue(Canvas.LeftProperty));
            long slime1Pos = Convert.ToInt64(slime.GetValue(Canvas.TopProperty));
            long slime2Pos = Convert.ToInt64(slime.GetValue(Canvas.LeftProperty));
            if (ball1Pos > 500 || ball2Pos > 770 || ball2Pos < -10)
            {
                Canvas.SetTop(ball, 130);
                Canvas.SetLeft(ball, 300);
            }
            if (ball1Pos < 485)
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                {
                    Canvas.SetTop(ball, ball1Pos + 8);
                }
            }
            
            if (slime2Pos >= 700)
            {
                Canvas.SetLeft(slime, 690);
                t = 1;
            }
            else if (slime2Pos <= -10)
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
                    Canvas.SetLeft(slime, slime2Pos - 8);
                    if (k == 400)
                    {
                        i = 0;
                    }
                }
                else
                {
                    Canvas.SetLeft(slime, slime2Pos + 8);
                }
            }
            var x1 = Canvas.GetLeft(ball);
            var y1 = Canvas.GetTop(ball);
            Rect r1 = new Rect(x1, y1, ball.ActualWidth - 40, ball.ActualHeight - 40);


            var x2 = Canvas.GetLeft(slime);
            var y2 = Canvas.GetTop(slime);
            Rect r2 = new Rect(x2, y2, slime.ActualWidth - 40, slime.ActualHeight - 40);

            if (r1.IntersectsWith(r2))
            {
                timer.Stop();
            }


            txbFooter.Text = "i: " + i.ToString() + " k: " + k.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
