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

    [Serializable]
    public abstract class MyFigure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMoving { get; set; }
        [NonSerialized]
        public Shape Shape;

        public virtual void Draw()
        {
            Console.WriteLine("Drawing abstract figure!"); 
        }

        public virtual void Move(Point sizeOfCanvas)
        {
            Console.WriteLine("Moving abstract figure!");
        }

        public virtual void SerializeJSON(JsonSerializer serializer, JsonWriter writer)
        {
            Console.WriteLine("JSON serializing abstract figure!");
        }
    }
}
