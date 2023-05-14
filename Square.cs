using System.Drawing;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Square class that inherits from Rectangle as drawing methods are the same.
    /// </summary>
    class Square : Rectangle // As Sqaures are essentially rectangles with the same width and height we can use it as the parent class.
    {
        readonly int length;
        /// <summary>
        /// Constructor uses readonly length attribute to set base width and height as the same value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="length"></param>
        public Square(int x, int y, int length, bool filledIn) : base(x, y, length, length, filledIn)
        {
            this.length = length;
        }
    }
}
