using System.Windows.Forms;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Factory class used to instanstiate objects based on shape type parameter passed in.
    /// </summary>
    public class Factory
    {
        public bool showMessage = true;
        /// <summary>
        /// Static method which returns given shape object depending on parameter passed in.
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        public Shape GetShape(string shapeType)
        {
            shapeType = shapeType.ToLower().Trim();


            if (shapeType.Equals("circle"))
            {
                return new Circle();

            }
            else if (shapeType.Equals("rectangle") || (shapeType.Equals("square")))
            {
                return new Rectangle(); // As square inherits from shape it's easier to return the parent class and set width and height as the same.

            }
            else if (shapeType.Equals("triangle"))
            {
                return new Triangle();
            }

            if (showMessage) { MessageBox.Show("Shape type does not exist."); } // Incase the given type doesn't exist in practice this will never be hit as the validator will pick it up first.
            return null;
        }
    }
}
