using System;
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
        /// Allows program to evaluate the type of shape and return a new object of the give type.
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        public Shape getShape(String shapeType)
        {
            shapeType = shapeType.ToLower().Trim();
            if (shapeType.Equals("rectangle"))
            {
                return new Rectangle();

            }
            return new Square();// This is simply just placeholder once other shapes are added there will be more
            //TODO: Return error message for when shape type is incorrect.
        }
    }
}
