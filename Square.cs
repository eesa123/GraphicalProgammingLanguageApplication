using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Square class that inherits from Rectangle as drawing methods are the same.
    /// </summary>
    class Square : Rectangle
    {
        readonly int length;
        /// <summary>
        /// Constructor uses readonly length attribute to set base width and height as the same value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="length"></param>
        public Square(int x, int y, int length) : base(x, y, length, length)
        {
            this.length = length;
        }
        public Square() { }

        public override void Draw(Graphics g, Pen pen, Brush brush)
        {
            base.Draw(g, pen, brush);
        }
    }
}
