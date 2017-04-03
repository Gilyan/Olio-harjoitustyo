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
    /// Interaction logic for PlayBallView.xaml
    /// </summary>
    public partial class PlayBallView : Window
    {
        //public PlayBallView()
        //{
        //    InitializeComponent();
        //}

        public PlayBallView(double x, double y)     // Ylikuormitetaan, että saadaan ikkunan paikka oikein
        {
            InitializeComponent();
            this.Left = x;
            this.Top = y;
        }

 //    Image img = Resource/ball.png;
 //
 //    Canvas.SetLeft(img, Left); // Default Left when adding the image = 0
 //    Canvas.SetTop(img, Top); // Default Top when adding the image = 0
 //
 //    MyCanvas.Children.Add(img);
 //    OnPropertyChanged("MyCanvas");
 //
 //    img.AllowDrop = true;
 //    img.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
 //    img.PreviewMouseMove += this.MouseMove;
 //    img.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;
 //
 //
 //    private object movingObject;
 //    private double firstXPos, firstYPos;
 //    private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
 //    {
 //        // In this event, we get the current mouse position on the control to use it in the MouseMove event.
 //        Image img = sender as Image;
 //        Canvas canvas = img.Parent as Canvas;
 //
 //        firstXPos = e.GetPosition(img).X;
 //        firstYPos = e.GetPosition(img).Y;
 //
 //        movingObject = sender;
 //
 //        // Put the image currently being dragged on top of the others
 //        int top = Canvas.GetZIndex(img);
 //        foreach (Image child in canvas.Children)
 //            if (top < Canvas.GetZIndex(child))
 //                top = Canvas.GetZIndex(child);
 //        Canvas.SetZIndex(img, top + 1);
 //    }
 //    private void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
 //    {
 //        Image img = sender as Image;
 //        Canvas canvas = img.Parent as Canvas;
 //
 //        movingObject = null;
 //
 //        // Put the image currently being dragged on top of the others
 //        int top = Canvas.GetZIndex(img);
 //        foreach (Image child in canvas.Children)
 //            if (top > Canvas.GetZIndex(child))
 //                top = Canvas.GetZIndex(child);
 //        Canvas.SetZIndex(img, top + 1);
 //    }
 //    private void MouseMove(object sender, MouseEventArgs e)
 //    {
 //        if (e.LeftButton == MouseButtonState.Pressed && sender == movingObject)
 //        {
 //            Image img = sender as Image;
 //            Canvas canvas = img.Parent as Canvas;
 //
 //            double newLeft = e.GetPosition(canvas).X - firstXPos - canvas.Margin.Left;
 //            // newLeft inside canvas right-border?
 //            if (newLeft > canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth)
 //                newLeft = canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth;
 //            // newLeft inside canvas left-border?
 //            else if (newLeft < canvas.Margin.Left)
 //                newLeft = canvas.Margin.Left;
 //            img.SetValue(Canvas.LeftProperty, newLeft);
 //
 //            double newTop = e.GetPosition(canvas).Y - firstYPos - canvas.Margin.Top;
 //            // newTop inside canvas bottom-border?
 //            if (newTop > canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight)
 //                newTop = canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight;
 //            // newTop inside canvas top-border?
 //            else if (newTop < canvas.Margin.Top)
 //                newTop = canvas.Margin.Top;
 //            img.SetValue(Canvas.TopProperty, newTop);
 //        }
 //    }
     }
}
