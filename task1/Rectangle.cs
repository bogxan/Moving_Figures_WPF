namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class MyRectangle : MyFigure
    {
        public MyRectangle(int width, int height, int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.X1 = x;
            this.Y1 = y;
        }

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
    }
}
