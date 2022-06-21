using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task1
{
    public class MyRectangle: MyFigure
    {
        public int width { get; set; }
        public int height { get; set; }
        public MyRectangle(int width, int height, int x, int y)
        {
            this.width = width;
            this.height = height;
            base.x1 = x;
            base.y1 = y;
        }
        public override void Draw()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Stroke = new SolidColorBrush(Colors.Black);
            rectangle.StrokeThickness = 1;
            rectangle.Fill = new SolidColorBrush(Colors.Green);
            shape = rectangle;
        }
    }
}
