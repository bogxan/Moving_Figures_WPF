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
    public class MyCircle<T> : MyFigure<T>
    {
        public MyCircle(int width, int height, int x, int y) : base(width, height, x, y)
        {
            this.BaseCircle = new();
        }

        public MyCircle() : base()
        {
            this.BaseCircle = new();
        }

        [NonSerialized]
        [XmlIgnore]
        public Ellipse BaseCircle;

        public override void Draw()
        {
            this.BaseCircle = new();
            this.BaseCircle.Width = this.Width;
            this.BaseCircle.Height = this.Height;
            this.BaseCircle.Stroke = new SolidColorBrush(Colors.Black);
            this.BaseCircle.StrokeThickness = 1;
            this.BaseCircle.Fill = new SolidColorBrush(Colors.Red);
        }

        public override void Move(Point sizeOfCanvas)
        {
            Random rnd = new();
            try
            {
                this.X = rnd.Next(Convert.ToInt32(sizeOfCanvas.X - this.BaseCircle.ActualWidth));
                this.Y = rnd.Next(Convert.ToInt32(sizeOfCanvas.Y - this.BaseCircle.ActualHeight));
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
                Canvas.SetLeft(this.BaseCircle, this.X);
                Canvas.SetTop(this.BaseCircle, this.Y);
            }
        }
    }
}
