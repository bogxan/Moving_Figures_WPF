namespace Task1.Models
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
    public class MyFigure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMoving { get; set; }
        [NonSerialized]
        [XmlIgnore]
        public Shape Shape;

        public MyFigure()
        {
            this.Id = -1;
            this.Name = string.Empty;
            this.IsMoving = false;
        }

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
