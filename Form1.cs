using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        OpenFileDialog openFile = new OpenFileDialog();// used for opening files
        public Form1()
        {
            InitializeComponent();
            this.display.Image = new Bitmap(Size.Width, Size.Height);
        }
        /// <summary>
        /// Method that handles when run button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton(object sender, EventArgs e)
        {
            Console.WriteLine("Run");
        }
        /// <summary>
        /// Method that loads text files into command panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.CommandPanel.Clear();
                String line = "";
                StreamReader sr = new StreamReader(openFile.FileName);
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        this.CommandPanel.Text += line;
                        this.CommandPanel.Text += "\r\n";
                    }
                }
            }
        }
        /// <summary>
        /// Method that resets command panel and command line to empty text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton(object sender, EventArgs e)
        {
            this.CommandPanel.ResetText();
            this.CommandLine.ResetText();
        }
        /// <summary>
        /// Method that will check user input and the syntax and display any errors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syntaxButton(object sender, EventArgs e)
        {
            Console.WriteLine("Syntax");
        }
        /// <summary>
        /// Saves current text in command panel to a text file in a location chosen by the user. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(this.CommandPanel.Text);
                }
                MessageBox.Show("Your File has been saved Sucessfully");
            }
        }
    }
}
