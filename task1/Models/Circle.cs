namespace Task1.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    [Serializable]
    public class MyCircle : MyFigure
    {
        public MyCircle(int radius, int x, int y)
        {
            this.Radius = radius;
            this.X1 = x;
            this.Y1 = y;
        }

        public MyCircle()
        {
            this.Radius = -1;
            this.X1 = this.Y1 = 0;
        }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int Radius { get; set; }

        public override void Draw()
        {
            Ellipse circle = new();
            circle.Width = this.Radius * 2;
            circle.Height = this.Radius * 2;
            circle.Stroke = new SolidColorBrush(Colors.Black);
            circle.StrokeThickness = 1;
            circle.Fill = new SolidColorBrush(Colors.Red);
            this.Shape = circle;
        }

        public override void Move(Point sizeOfCanvas)
        {
            Random rnd = new Random();
            X1 = rnd.Next(Convert.ToInt32(sizeOfCanvas.X - Shape.ActualWidth));
            Y1 = rnd.Next(Convert.ToInt32(sizeOfCanvas.Y - Shape.ActualHeight));
            DoubleAnimation animLeft = new(Canvas.GetLeft(Shape), X1, new Duration(TimeSpan.FromSeconds(0.5)));
            DoubleAnimation animTop = new(Canvas.GetTop(Shape), Y1, new Duration(TimeSpan.FromSeconds(0.5)));
            animLeft.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            animTop.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            Shape.BeginAnimation(Canvas.LeftProperty, animLeft, HandoffBehavior.SnapshotAndReplace);
            Shape.BeginAnimation(Canvas.TopProperty, animTop, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
