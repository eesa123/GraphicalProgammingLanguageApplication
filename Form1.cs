using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Form Class used to control application actions and events.
    /// </summary>
    public partial class Form1 : Form
    {

        private int x, y = 1;//default draw position
        private Color pencol = Color.Black;//universal colour for pen
        private string[] variables = new string[100]; //to store variable names
        private int[] variableValues = new int[100];//to store variable values
        private int varCounter = 0;//to store the current postion of the variables array/ how many variables have been defined
        private int counter = 0;// used to find the number of iterations for a for loop command
        private Graphics graphics;// used for graphics on drawing panel to draw shapes
        private Pen drawingPen;// used to draw shapes
        private Brush drawingBrush;// used to draw and fill shapes
        private bool fillIn = false;// used to track if brush is to be used or pen
        OpenFileDialog openFile = new OpenFileDialog();// used for opening files

        public Form1()
        {
            InitializeComponent();
            graphics = display.CreateGraphics();
            drawingPen = new Pen(Color.Black, 2);
            drawingBrush = new SolidBrush(Color.Black);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
        /// <summary>
        /// Method that handles when run button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton(object sender, EventArgs e)
        {
            if (this.CommandPanel.Text != null && !this.CommandPanel.Text.Equals("")) // if command panel has text then take commands from there only.
            {
                CommandValidator validate = new CommandValidator(CommandPanel);
                if (!validate.doesInvalidCommandExists())
                {
                    parseCommands(CommandPanel);
                }

            }
            else if (this.CommandLine.Text != null && !this.CommandLine.Text.Equals("")) // if command panel has no text then run command line command.
            {
                CommandValidator validate = new CommandValidator(CommandLine);
                if (!validate.doesInvalidCommandExists()) // if any invalid commands then no commands are run/parsed.
                {
                    parseCommands(CommandLine);
                }
            }
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
                this.CommandPanel.Clear();// make sure command panel has no text before loading.
                string line = "";
                StreamReader sr = new StreamReader(openFile.FileName);
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        this.CommandPanel.Text += line + "\r\n"; // add text to command panel and after every line add a new line.
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
            // clears text in both command panel and command line and refreshs the display and sets pen position back to starting position.
            this.CommandPanel.ResetText();
            this.CommandLine.ResetText();
            this.display.Refresh();
            this.x = this.y = 0;
        }
        /// <summary>
        /// Method that will check user input and the syntax and display any errors.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void syntaxButton(object sender, EventArgs e)
        {
            // Essentially the same as the run button but if the commands are valid displays a message telling them there are no errors.
            if (this.CommandPanel.Text != null && !this.CommandPanel.Text.Equals(""))
            {
                CommandValidator validate = new CommandValidator(CommandPanel);
                if (!validate.doesInvalidCommandExists())
                {
                    MessageBox.Show("Coammnds have no errors.");
                }

            }
            else if (this.CommandLine.Text != null && !this.CommandLine.Text.Equals(""))
            {
                CommandValidator validate = new CommandValidator(CommandLine);
                if (!validate.doesInvalidCommandExists())
                {
                    MessageBox.Show("Command has no errors.");
                }
            }
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
                using (Stream s = File.Open(saveFileDialog.FileName, FileMode.CreateNew)) // creates a new file and saves command panel text into it
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(this.CommandPanel.Text); // using streamwriter command panel text is written into new file.
                }
                MessageBox.Show("Your File has been saved Sucessfully");
            }
        }

        /// <summary>
        /// Takes each command from textbox and and runs the command
        /// </summary>
        /// <param name="commands"></param>
        private void parseCommands(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            // while the loopcount is less than the number of lines it will run the commands line by line.
            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    runCommand(oneLineCommand, commands);
                }

            }
        }
        private int getAssociatedVariableValue(string variable) //used to call the valuse of the assined varable if it is there
        {
            int number = -2;
            if (int.TryParse(variable, out _))
            {
                number = int.Parse(variable);
                return number;
            }
            for (int i = 0; i < variable.Length; i++)
            {
                if (variables[i] == null) { break; } // reached null objects meaning no more variables so break
                if (variables[i] == variable)
                {

                    number = variableValues[i];
                    break;
                }

            }
            return number;

        }

        public void runCommand(string command, TextBox commands)
        {
            string[] args = command.Split(' '); //split args
            //removing white spaces in between words
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }

            switch (args[0].ToLower())
            {
                case "circle":
                    int radius;
                    radius = getAssociatedVariableValue(args[1]);
                    new Circle(x, y, radius, fillIn).Draw(graphics, drawingPen, drawingBrush);
                    break;
                case "rectangle":
                    int width;
                    int height;
                    width = getAssociatedVariableValue(args[1]);
                    height = getAssociatedVariableValue(args[2]);
                    new Rectangle(x, y, width, height, fillIn).Draw(graphics, drawingPen, drawingBrush);
                    break;
                case "square":
                    int side;
                    side = getAssociatedVariableValue(args[1]);
                    new Square(x, y, side, fillIn).Draw(graphics, drawingPen, drawingBrush);
                    break;
                case "drawto":
                    int point1, point2;
                    point1 = getAssociatedVariableValue(args[1]);
                    point2 = getAssociatedVariableValue(args[2]);
                    graphics.DrawLine(drawingPen, x, y, point1, point2);
                    break;
                case "colour":
                    try
                    {
                        int red;
                        int blue;
                        int green;
                        red = getAssociatedVariableValue(args[1]);
                        blue = getAssociatedVariableValue(args[2]);
                        green = getAssociatedVariableValue(args[3]);
                        pencol = Color.FromArgb(red, blue, green);
                        drawingPen.Color = pencol;
                        drawingBrush = new SolidBrush(pencol);
                    }
                    catch
                    {
                        pencol = Color.Black;
                        drawingPen.Color = pencol;
                        drawingBrush = new SolidBrush(pencol);
                    }
                    break;
                case "triangle":
                    int p1, p2, p3;
                    p1 = getAssociatedVariableValue(args[1]);
                    p2 = getAssociatedVariableValue(args[2]);
                    p3 = getAssociatedVariableValue(args[3]);
                    Point pointa1 = new Point(p1, p2);
                    Point pointb1 = new Point(p2, p3);
                    Point pointc1 = new Point(p3, p1);

                    Point[] pnt1 = { pointa1, pointb1, pointc1 };
                    new Triangle(pnt1, fillIn).Draw(graphics, drawingPen, drawingBrush);
                    break;
                case "clear":
                    graphics.Clear(Color.Transparent);
                    graphics.Dispose();
                    break;
                case "reset":
                    x = 0;
                    y = 0;
                    break;
                case "fillIn":
                    fillIn = true;
                    break;
                case "Unfill":
                    fillIn = false;
                    break;
                case "moveto":
                    x = getAssociatedVariableValue(args[1]);
                    y = getAssociatedVariableValue(args[2]);                  
                    break;
                case "var":
                    variables[varCounter] = args[1].ToLower();
                    variableValues[varCounter] = int.Parse(args[3].Trim());
                    varCounter++;
                    break;
                case "factory":
                    Shape shape = Factory.getShape(args[1]);
                    Random random = new Random();
                    if (args[1].ToLower().Trim() == "circle")
                    {
                        radius = random.Next(display.Width / 4);
                        shape.set(x, y, radius);
                    }
                    else if (args[1].ToLower().Trim() == "rectangle" || args[1].ToLower().Trim() == "square")
                    {
                        width = random.Next(display.Width);
                        height = random.Next(display.Height);
                        if (args[1].ToLower().Trim() == "square") { height = width;}
                        shape.set(x, y, width, height);
                    }
                    else if (args[1].ToLower().Trim() == "triangle")
                    {
                        p1 = random.Next(display.Width);
                        p2 = random.Next(display.Width);
                        p3 = random.Next(display.Width);
                        Point pointa = new Point(p1, p2);
                        Point pointb = new Point(p2, p3);
                        Point pointc = new Point(p3, p1);
                        Point[] pnt = { pointa, pointb, pointc };
                        shape.setTriangle(x, y, pnt);
                    }
                    shape.Draw(graphics, drawingPen, drawingBrush);
                    break;
                case "for":
                    {
                        counter = getAssociatedVariableValue(args[1]);
                        int loopStartLine = (GetLoopStartLineNumber(commands));
                        int loopEndLine = (GetLoopEndLineNumber(commands) - 1);
                        for (int i = 0; i < counter; i++)
                        {
                            for (int j = loopStartLine; j <= loopEndLine; j++)
                            {
                                string oneLineCommand = commands.Lines[j];
                                oneLineCommand = oneLineCommand.Trim();
                                if (!oneLineCommand.Equals(""))
                                {
                                    runCommand(oneLineCommand, commands);
                                }
                            }
                        }
                    }
                    break;
                case "if":
                    int left;
                    int right;
                    string condition;
                    left = getAssociatedVariableValue(args[1]);
                    right = getAssociatedVariableValue(args[3]);
                    condition = args[2];
                    bool ifResult = isIfStatementTrue(left, condition, right);
                    if (ifResult == true)
                    {

                        int ifStartLine = (GetIfStartLineNumber(commands));
                        int ifEndLine = (GetEndifEndLineNumber(commands) - 1);
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand = commands.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                runCommand(oneLineCommand, commands);
                            }
                        }
                        break;
                    }
                    break;
            }
        }

        private int GetIfStartLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');

                //removing white spaces in between words

                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                string firstWord = words[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetEndifEndLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }


        private bool isIfStatementTrue(int left, string condition, int right) 
        {
            bool ifResult = false;
            switch (condition)
            {

                case "==":
                    if (left == right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }
                    break;
                case ">":
                    if (left > right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }

                    break;
                case "<":
                    if (left < right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }
                    break;
                case ">=":
                    if (left >= right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }

                    break;
                case "<=":
                    if (left <= right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }
                    break;
                case "!=":
                    if (left != right)
                    {
                        ifResult = true;
                    }
                    else
                    {
                        ifResult = false;
                    }
                    break;
            }
            return ifResult;
        }

        private int GetLoopEndLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endloop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        private int GetLoopStartLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] words = oneLineCommand.Split(' ');

                //removing white spaces in between words
                for (int j = 0; j < words.Length; j++)
                {
                    words[j] = words[j].Trim();
                }
                string firstWord = words[0].ToLower();
                if (firstWord.Equals("for") || firstWord.Equals("while"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
    }
}
