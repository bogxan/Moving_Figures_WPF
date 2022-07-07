namespace Task1
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public abstract class MyFigure
    {
        public string Name { get; set; }

        public bool IsMoving { get; set; }

        public Shape Shape { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public virtual void Draw()
        {
            Console.WriteLine("Drawing abstract figure!"); 
        }
    }
}
