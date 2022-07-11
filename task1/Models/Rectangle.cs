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
    public class MyRectangle : MyFigure
    {
        public MyRectangle(int width, int height, int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.X1 = x;
            this.Y1 = y;
        }

        public MyRectangle()
        {
            this.Width = 0;
            this.Height = 0;
            this.X1 = 0;
            this.Y1 = 0;
        }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public override void Draw()
        {
            Rectangle rectangle = new();
            rectangle.Width = this.Width;
            rectangle.Height = this.Height;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 1;
            rectangle.Fill = new SolidColorBrush(Colors.Green);
            this.Shape = rectangle;
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
                + this.X1 + " " + this.Y1 + " " + this.Width + " " + this.Height);
        }
    }
}
