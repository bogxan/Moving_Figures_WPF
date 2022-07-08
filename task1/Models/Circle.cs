namespace Task1.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
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

        public override void Serialize(JsonSerializer serializer, JsonWriter writer)
        {
            serializer.Serialize(writer, this.Id + " " + this.Name + " " + this.IsMoving 
                + " " + this.X1 + " " + this.Y1 + " " + this.Radius);
        }
    }
}
