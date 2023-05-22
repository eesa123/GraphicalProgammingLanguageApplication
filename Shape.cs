using System.Drawing;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Abstract class to define common attributes and methods for all shapes that can be drawn
    /// </summary>
    public abstract class Shape
    {
        protected int x, y;// x and y co-ordinates in the drawing panel and the pen position.
        protected bool filledIn; // Used to determine if shape is filled in using brush or not.
        public Shape(int x, int y, bool filledIn)
        {

            this.x = x;
            this.y = y;
            this.filledIn = filledIn;
        }

        /// <summary>
        /// Used to allow empty constructors for child classes.
        /// </summary>
        public Shape(){}

        /// <summary>
        /// Sets the x co ordinate and y co ordinate for the shape drawing start (pen position)
        /// </summary>
        /// <param name="list"></param>
        public virtual void Set(params int[] list)
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
        public virtual void SetTriangle(int x, int y, Point[] points)
        {
            this.x = x;
            this.y = y;

        }

        /// <summary>
        /// Abstract method to tell the child classes what arguments should be given for this method.
        /// </summary>
        /// <param name="g"> Graphics for the drawing panel</param>
        /// <param name="pen"> Drawing pen for drawing shape outlines</param>
        /// <param name="brush"> Drawing brush used for filling in shapes</param>
        public abstract void Draw(Graphics g, Pen pen, Brush brush);
    }
}
