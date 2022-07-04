namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class MyCircle : MyFigure
    {
        public MyCircle(int radius, int x, int y)
        {
            this.Radius = radius;
            this.X1 = x;
            this.Y1 = y;
        }

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
    }
}
