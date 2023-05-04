using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Form Class used to control application actions and events.
    /// </summary>
    public partial class Form1 : Form
    {

        private int x, y = 0;//default draw position
        private int errorcount = 0;//used to determine number of errors generated

        private Color pencol = Color.Black;//universal colour for pen
        private Color brushcol = Color.Cyan;// universal colour for brush
        private Random rnd = new Random();//used to make random number for colour changes and factory shapes

        private Shape shape1; // used in producing shapes
        private ShapeTypes factory = new ShapeTypes();// used to determine shape type of command
        public Form1()
        {
            InitializeComponent();
            this.display.Image = new Bitmap(Size.Width, Size.Height);
        }

        private void runButton(object sender, EventArgs e)
        {
            Console.WriteLine("Run");
        }

        private void loadButton(object sender, EventArgs e)
        {
            Console.WriteLine("Load");
        }
        private void clearButton(object sender, EventArgs e)
        {
            this.CommandPanel.ResetText();
            this.CommandLine.ResetText();
        }
        private void syntaxButton(object sender, EventArgs e)
        {
            Console.WriteLine("Syntax");
        }
        private void saveButton(object sender, EventArgs e)
        {
            Console.WriteLine("Save");
        }
    }
}
