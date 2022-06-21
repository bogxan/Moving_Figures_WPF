using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task1
{
    public class MyTriangle : MyFigure
    {
        public int x2 { get; set; }
        public int y2 { get; set; }
        public int x3 { get; set; }
        public int y3 { get; set; }
        public MyTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            base.x1 = x1;
            base.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
        }
        public override void Draw()
        {
            Polygon triangle = new Polygon();
            triangle.Points.Add(new System.Windows.Point { X = x1, Y = y1 });
            triangle.Points.Add(new System.Windows.Point { X = x2, Y = y2 });
            triangle.Points.Add(new System.Windows.Point { X = x3, Y = y3 });
            triangle.Stroke = new SolidColorBrush(Colors.Black);
            triangle.StrokeThickness = 1;
            triangle.Fill = new SolidColorBrush(Colors.Blue);
            shape = triangle;
        }
    }
}
