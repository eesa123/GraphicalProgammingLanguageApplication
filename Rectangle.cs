using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Defines the Rectangle shape as a class with width and height attributes.
    /// </summary>
    class Rectangle : Shape
    {
        private int width;
        private int height;
        /// <summary>
        /// Constructor to set attributes.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(int x, int y, int width, int height, bool filledIn) : base(x, y, filledIn)
        {

            this.width = width;
            this.height = height;
        }
        public Rectangle() { }
        /// <summary>
        /// Sets x and y co ordinates as well as width and height attributes.
        /// </summary>
        /// <param name="list"></param>
        public override void set(params int[] list)
        {
            base.set(list[0], list[1]);
        }
        /// <summary>
        /// Calls FillRectangle and DrawRectangle to draw rectangle shape at specified co-ordinates.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public override void Draw(Graphics g, Pen pen, Brush brush)
        {
            if (filledIn)
            {
               g.FillRectangle(brush, x, y, width, height);
                return;
            }
            g.DrawRectangle(pen, x, y, width, height);
        }
    }
}
