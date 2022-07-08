namespace Task1.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    [Serializable]
    public abstract class MyFigure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMoving { get; set; }

        public Shape Shape { get; set; }

        public virtual void Draw()
        {
            Console.WriteLine("Drawing abstract figure!"); 
        }

        public virtual void Serialize(JsonSerializer serializer, JsonWriter writer)
        {
            Console.WriteLine("Serializing abstract figure!");
        }
    }
}
