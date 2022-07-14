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
    using System.Xml.Serialization;
    using Task1.Exceptions;

    [Serializable]
    public class MyRectangle<T> : MyFigure<T>
    {
        public MyRectangle(int width, int height, int x, int y) : base(width, height, x, y)
        {
            this.BaseRectangle = new();
        }

        public MyRectangle() : base()
        {
            this.BaseRectangle = new();
        }

        [NonSerialized]
        [XmlIgnore]
        public Rectangle BaseRectangle;

        public override void Draw()
        {
            this.BaseRectangle = new();
            this.BaseRectangle.Width = this.Width;
            this.BaseRectangle.Height = this.Height;
            this.BaseRectangle.Stroke = new SolidColorBrush(Colors.Black);
            this.BaseRectangle.StrokeThickness = 1;
            this.BaseRectangle.Fill = new SolidColorBrush(Colors.Green);
        }

        public override void Move(Point sizeOfCanvas)
        {
            Random rnd = new();
            try
            {
                this.X = rnd.Next(Convert.ToInt32(sizeOfCanvas.X - this.BaseRectangle.ActualWidth));
                this.Y = rnd.Next(Convert.ToInt32(sizeOfCanvas.Y - this.BaseRectangle.ActualHeight));
                if (this.X >= sizeOfCanvas.X || this.Y >= sizeOfCanvas.Y)
                {
                    throw new FigureException("Figure is out of canvas!");
                }
            }
            catch (FigureException)
            {
                this.X = 300;
                this.Y = 300;
            }
            finally
            {
                Canvas.SetLeft(this.BaseRectangle, this.X);
                Canvas.SetTop(this.BaseRectangle, this.Y);
            }
        }
    }
}
