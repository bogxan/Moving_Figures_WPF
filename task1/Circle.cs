using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task1
{
    public class MyCircle : MyFigure
    {
        public int radius { get; set; }
        public MyCircle(int radius, int x, int y)
        {
            this.radius = radius;
            base.x1 = x;
            base.y1 = y;
        }
        public override void Draw()
        {
            Ellipse circle = new Ellipse();
            circle.Width = radius * 2;
            circle.Height = radius * 2;
            circle.Stroke = new SolidColorBrush(Colors.Black);
            circle.StrokeThickness = 1;
            circle.Fill = new SolidColorBrush(Colors.Red);
            shape = circle;
        }
    }
}
