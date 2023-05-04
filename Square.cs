using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgammingLanguage
{
    internal class Square : Rectangle
    {
        readonly int length;
        public Square(int x, int y, int length) : base(x, y, length, length)
        {
            this.length = length;
        }


        public override void Draw(Graphics g, Pen pen, Brush brush)
        {
            base.Draw(g, pen, brush);
        }
    }
}
