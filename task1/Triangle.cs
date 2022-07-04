namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Shapes;

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
    }
}
