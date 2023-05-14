using System.Drawing;

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
        /// <summary>
        /// Empty constructor used for Factory class.
        /// </summary>
        public Rectangle() { }
        /// <summary>
        /// Sets x and y co ordinates as well as width and height attributes.
        /// </summary>
        /// <param name="list"></param>
        public override void set(params int[] list)
        {
            base.set(list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
        }
        /// <summary>
        /// Calls FillRectangle and DrawRectangle to draw rectangle shape at specified co-ordinates.
        /// </summary>
        /// <param name="g"> Graphics for the drawing panel</param>
        /// <param name="pen"> Drawing pen for drawing shape outlines</param>
        /// <param name="brush"> Drawing brush used for filling in shapes</param>
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
