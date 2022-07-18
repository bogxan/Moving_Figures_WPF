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
    public class MyTriangle<T> : MyFigure<T>
    {
        public MyTriangle(int width, int height, int x, int y) : base(width, height, x, y)
        {
            this.BaseTriangle = new();
        }

        public MyTriangle() : base()
        {
            this.BaseTriangle = new();
        }

        [NonSerialized]
        [XmlIgnore]
        public Polygon BaseTriangle;

        public override void Draw()
        {
            this.BaseTriangle = new();
            this.BaseTriangle.Points.Add(new System.Windows.Point { X = 50, Y = 150 });
            this.BaseTriangle.Points.Add(new System.Windows.Point { X = 150, Y = 50 });
            this.BaseTriangle.Points.Add(new System.Windows.Point { X = 250, Y = 150 });
            this.BaseTriangle.Width = this.Width;
            this.BaseTriangle.Height = this.Height;
            this.BaseTriangle.Stroke = new SolidColorBrush(Colors.Black);
            this.BaseTriangle.StrokeThickness = 1;
            this.BaseTriangle.Fill = new SolidColorBrush(Colors.Blue);
        }

        public override void Move(Point sizeOfCanvas)
        {
            Random rnd = new();
            try
            {
                this.X = rnd.Next(Convert.ToInt32(sizeOfCanvas.X - this.BaseTriangle.Width));
                this.Y = rnd.Next(Convert.ToInt32(sizeOfCanvas.Y - this.BaseTriangle.Height));
                if (this.X >= sizeOfCanvas.X || this.Y >= sizeOfCanvas.Y)
                {
                    throw new FigureException("Figure is out of canvas!");
                }
            }
            catch (FigureException)
            {
                this.X = 100;
                this.Y = 100;
            }
            finally
            {
                Canvas.SetLeft(this.BaseTriangle, this.X);
                Canvas.SetTop(this.BaseTriangle, this.Y);
            }
        }
    }
}
