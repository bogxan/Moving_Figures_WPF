using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task1
{
    public abstract class MyFigure
    {
        public Shape shape { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public virtual void Draw() {
            Console.WriteLine("Drawing abstract figure!");
        }
    }
}
