using System.Drawing;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Circle class used to draw circle shapes in application.
    /// </summary>
    class Circle : Shape
    {
        int radius;// radius size of circle object

        public Circle(int x, int y, int radius, bool filledIn) : base(x, y, filledIn)
        {

            this.radius = radius;
        }

        /// <summary>
        /// Empty constructor used for Factory class.
        /// </summary>
        public Circle(){}

        /// <summary>
        /// Draws the circle shape in the application.
        /// </summary>
        /// <param name="g"> Graphics for the drawing panel</param>
        /// <param name="pen"> Drawing pen for drawing shape outlines</param>
        /// <param name="brush"> Drawing brush used for filling in shapes</param>
        public override void Draw(Graphics g, Pen pen, Brush brush)
        {
            if (filledIn)
            {
                g.FillEllipse(brush, x, y, radius * 2, radius * 2);
                return;
            }
            g.DrawEllipse(pen, x, y, radius * 2, radius * 2);

        }

        /// <summary>
        /// Sets the radius and x, y co-ordinates of the shape object used for Factory and Shape class instantiating.
        /// </summary>
        /// <param name="list"></param>
        public override void set(params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(list[0], list[1]);
            this.radius = list[2];

        }

        /// <summary>
        /// Override parent class ToString to include radius. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "  " + this.radius;
        }
    }
}

