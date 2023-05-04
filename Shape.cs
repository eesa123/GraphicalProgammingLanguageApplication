using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Abstract class to define common attributes and methods for all shapes that can be drawn
    /// </summary>
    abstract class Shape
    {
        protected int x, y;
        public Shape(int x, int y)
        {

            this.x = x;
            this.y = y;

        }

        public Shape()
        {
        }
        /// <summary>
        /// Sets the x co ordinate and y co ordinate for the shape drawing start (pen position)
        /// </summary>
        /// <param name="list"></param>
        public virtual void set(params int[] list)
        {

            this.x = list[0];
            this.y = list[1];

        }
        /// <summary>
        /// Virtual method to set triangle parameters, this will be ovewritten in the triangle class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        public virtual void setTriangle(int x, int y, Point[] points)
        {
            this.x = x;
            this.y = y;

        }
        /// <summary>
        /// Abstract method to tell the child classes what arguments should be given for this method.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public abstract void Draw(Graphics g, Pen pen, Brush brush);
        /// <summary>
        /// Standard ToString Method.
        /// </summary>
        /// <returns>A String detailing object attribute values.</returns>
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
