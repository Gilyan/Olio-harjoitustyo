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
        public int points = 0;
        public int Pisteet
        {
            get { return points; }
            set { points = value; }
        }
        public int Hunger
        {
            get { return hunger; }
            set { hunger = value; }
        }
        int hunger = 0;
        Point p = new Point();
        DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);
        Random rnd = new Random();
        bool keyPressed = true;

        private MediaPlayer mediaPlayer = new MediaPlayer();    // Äänisoitin

        public PlayBallView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
            this.Left = x;
            this.Top = y;
            this.KeyDown += new KeyEventHandler(slime_KeyDown);

            btnAgain.Visibility = System.Windows.Visibility.Hidden;     // Uudelleenpelausnappula piiloon toistaiseksi
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
            timer.Start();
            ball.ReleaseMouseCapture();
        } 

        private void timer_Tick(object sender, EventArgs e)
        {
            long ball1Pos = Convert.ToInt64(ball.GetValue(Canvas.TopProperty));
            long ball2Pos = Convert.ToInt64(ball.GetValue(Canvas.LeftProperty));
            long slime1Pos = Convert.ToInt64(slime.GetValue(Canvas.TopProperty));
            long slime2Pos = Convert.ToInt64(slime.GetValue(Canvas.LeftProperty));
            InsideTester(ball1Pos, ball2Pos);
            BallGravity(ball1Pos, ball2Pos);
            MoveSlime(slime2Pos);
            CollisionTester();
            txbFooter.Text = "points: " + points.ToString()  + ", hunger" + hunger.ToString();

        }
        private void BallGravity(long ballTop, long ballLeft)
        {
            if (ballTop < 485)
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed)
                {
                    Canvas.SetTop(ball, ballTop + 8);
                }
            }
            if (ballTop > 480)
            {
                hunger += -2;
                points += -5;
                Canvas.SetLeft(ball, rnd.Next(10, 734));
                Canvas.SetTop(ball, 130);
            }
        }
        private void slime_KeyDown(object sender, KeyEventArgs e)
        {
            long slime2Pos = Convert.ToInt64(slime.GetValue(Canvas.LeftProperty));
            if (e.Key == Key.Left)
            {
                Canvas.SetLeft(slime, slime2Pos - 10);
                keyPressed = true;
            }
            if (e.Key == Key.Right)
            {
                Canvas.SetLeft(slime, slime2Pos + 10);
                keyPressed = false;
            }
        }
        private void MoveSlime(long slimeLeft)
        {
            if (slimeLeft > 700)
            {
                keyPressed = true;
            }
            else if (slimeLeft < -10)
            {
                keyPressed = false;
            }
            if (keyPressed)
            {
                Canvas.SetLeft(slime, slimeLeft - 7);
            }
            if (!keyPressed)
            {
                Canvas.SetLeft(slime, slimeLeft + 7); ;
            }
            #region
            //if (slimeLeft >= 700)
            //{
            //    Canvas.SetLeft(slime, 690);
            //    t = 1;
            //}
            //else if (slimeLeft <= -10)
            //{
            //    Canvas.SetLeft(slime, 0);
            //    t = 0;
            //    i = 0;
            //}
            //else
            //{
            //    if (i == 0)
            //    {
            //        k = 0;
            //    }
            //    i += 10;
            //    if (i >= 600 || t == 1)
            //    {
            //        k += 10;
            //        Canvas.SetLeft(slime, slimeLeft - 8);
            //        if (k == 400)
            //        {
            //            i = 0;
            //        }
            //    }
            //    else
            //    {
            //        Canvas.SetLeft(slime, slimeLeft + 8);
            //    }
            //}
            #endregion
        }
        private void InsideTester(long ballTop, long ballLeft)
        {
            if (ballTop > 500 || ballLeft > 770 || ballLeft < -10)
            {
                Canvas.SetTop(ball, 130);
                Canvas.SetLeft(ball, 300);
            }
        }
        private void CollisionTester()
        {
            var x1 = Canvas.GetLeft(ball);
            var y1 = Canvas.GetTop(ball);
            Rect r1 = new Rect(x1, y1, ball.ActualWidth - 30, ball.ActualHeight - 30);


            var x2 = Canvas.GetLeft(slime);
            var y2 = Canvas.GetTop(slime);
            Rect r2 = new Rect(x2, y2, slime.ActualWidth - 30, slime.ActualHeight - 30);

            if (r1.IntersectsWith(r2))
            {
                points += 20;
                ball.Visibility = Visibility.Hidden;
                timer.Stop();
                btnAgain.Visibility = Visibility.Visible;
                ball.Visibility = Visibility.Hidden;

                mediaPlayer.Open(new Uri(@"../../Resources/sound/hihi.mp3", UriKind.Relative));
                mediaPlayer.Play();
                mediaPlayer.Position = TimeSpan.Zero;
            }
        }
        private void btnAgain_Click(object sender, RoutedEventArgs e)       // Pelataan uudestaan!
        {
            ball.Visibility = Visibility.Visible;
            btnAgain.Visibility = Visibility.Hidden;
            Canvas.SetTop(ball, 130);
            Canvas.SetLeft(ball, rnd.Next(10, 75));
            timer.Start();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Testi.WasClicked = true;    // Palauttaa tiedon, että back-nappia on painettu
            Testi.GetPoints = points;  // Vie oliolle peliltä saadut pisteet
            Testi.GetHunger = hunger;
            GameView.timer.Start();
            this.Close();
        }
    }
}
