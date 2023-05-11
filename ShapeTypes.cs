using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// This class is used to determine the shape type that is being drawn
    /// </summary>
    class ShapeTypes
    {
        /// <summary>
        /// Allows program to evaluate the type of shape and draw the shape.
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        public static void draw(ArrayList shapes, System.Drawing.Graphics graphics, System.Drawing.Pen drawingPen, System.Drawing.Brush drawingBrush)
        {
            foreach (Shape shape in shapes) {
                shape.Draw(graphics, drawingPen, drawingBrush);
            }
        }
    }
}
