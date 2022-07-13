﻿namespace Task1.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System;
    using System.Xml.Serialization;
    using JsonSubTypes;

    [Serializable]
    [XmlInclude(typeof(MyCircle))]
    [XmlInclude(typeof(MyRectangle))]
    [XmlInclude(typeof(MyTriangle))]
    [JsonSubtypes.KnownSubType(typeof(MyCircle), "Circle")]
    [JsonSubtypes.KnownSubType(typeof(MyRectangle), "Rectangle")]
    [JsonSubtypes.KnownSubType(typeof(MyTriangle), "Triangle")]
    public abstract class MyFigure
    {
        public MyFigure()
        {
            this.Id = -1;
            this.Name = string.Empty;
            this.IsMoving = false;
            this.Width = 0;
            this.Height = 0;
            this.X = 0;
            this.Y = 0;
        }

        public MyFigure(int width, int height, int x, int y)
        {
            this.Width = width;
            this.Height = height;
            this.X = x;
            this.Y = y;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMoving { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public virtual void Draw()
        {
            Console.WriteLine("Drawing abstract figure!");
        }

        public virtual void Move(Point sizeOfCanvas)
        {
            Console.WriteLine("Moving abstract figure!");
        }
    }
}
