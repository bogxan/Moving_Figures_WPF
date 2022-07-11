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
    public class MyTriangle : MyFigure
    {
        public MyTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            this.X3 = x3;
            this.Y3 = y3;
        }

        public MyTriangle()
        {
            this.X1 = 0;
            this.Y1 = 0;
            this.X2 = 0;
            this.Y2 = 0;
            this.X3 = 0;
            this.Y3 = 0;
        }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }

        public int X3 { get; set; }

        public int Y3 { get; set; }

        public override void Draw()
        {
            Polygon triangle = new();
            triangle.Points.Add(new System.Windows.Point { X = this.X1, Y = this.Y1 });
            triangle.Points.Add(new System.Windows.Point { X = this.X2, Y = this.Y2 });
            triangle.Points.Add(new System.Windows.Point { X = this.X3, Y = this.Y3 });
            triangle.Stroke = new SolidColorBrush(Colors.Black);
            triangle.StrokeThickness = 1;
            triangle.Fill = new SolidColorBrush(Colors.Blue);
            this.Shape = triangle;
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

        public override void SerializeJSON(JsonSerializer serializer, JsonWriter writer)
        {
            serializer.Serialize(writer, this.Id + " " + this.Name + " " + this.IsMoving + " " 
                + this.X1 + " " + this.Y1 + " " + this.X2 + " " + this.Y2 + " " + this.X3 + " " + this.Y3);
        }
    }
}
