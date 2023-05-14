using System.Drawing;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Class used to draw triangle shapes in application.
    /// </summary>
    class Triangle : Shape
    {
        private Point[] points; // used to determine the points the triangle will be drawn at
        private new readonly bool filledIn = false;
        public Triangle(Point[] points, bool filledIn)
        {

            this.points = points;
            this.filledIn = filledIn;
        }

        /// <summary>
        /// Empty constructor used for Factory class.
        /// </summary>
        public Triangle() { }

        /// <summary>
        /// Used to set the triangle values when instantiating from Factory/Shape class
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        public override void setTriangle(int x, int y, Point[] points)
        {
            base.set(x, y);
            this.points = points;
        }

        /// <summary>
        /// Used to draw the triangle shape in application.
        /// </summary>
        /// <param name="g"> Graphics for the drawing panel</param>
        /// <param name="pen"> Drawing pen for drawing shape outlines</param>
        /// <param name="brush"> Drawing brush used for filling in shapes</param>
        public override void Draw(Graphics g, Pen pen, Brush brush)
        {
            if (filledIn)
            {
                g.FillPolygon(brush, points);
                return;
            }
            g.DrawPolygon(pen, points);
        }


    }
}
