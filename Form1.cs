using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private int x, y = 0;//default draw position
        private Color pencol = Color.Black;//universal colour for pen
        private List<string> variables = new List<string>();//used to store the variables defined
        private List<int> variableValues = new List<int>();//used to store the corresponding values to the defined variables
        private int counter = 0;// used to find the number of iterations for a for loop command
        private int currentLine = 0;// used to determine the current line
        private Graphics graphics;// used for graphics on drawing panel to draw shapes
        private Pen drawingPen;// used to draw shapes
        private Brush drawingBrush;// used to draw and fill shapes
        private bool fillIn = false;// used to track if brush is to be used or pen
        private int blockEndLine = -1;// used to determine if a block statement (for/if) is being run and what line that statement ends at
        OpenFileDialog openFile = new OpenFileDialog();// used for opening files

        /// <summary>
        /// Constructor initializing component and graphics and pen and brush
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            graphics = display.CreateGraphics();
            drawingPen = new Pen(Color.Black, 2);
            drawingBrush = new SolidBrush(Color.Black);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        /// <summary>
        /// Method that handles when run button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunButton(object sender, EventArgs e)
        {
            currentLine = 0;
            blockEndLine = -1;
            if (CommandPanel.Text != null && !CommandPanel.Text.Equals("")) // if command panel has text then take commands from there only.
            {
                CommandValidator validate = new CommandValidator(CommandPanel);
                if (!validate.DoesInvalidCommandExists())
                {
                    ParseCommands(CommandPanel);
                }

            }
            else if (CommandLine.Text != null && !CommandLine.Text.Equals("")) // if command panel has no text then run command line command.
            {
                CommandValidator validate = new CommandValidator(CommandLine);
                if (!validate.DoesInvalidCommandExists()) // if any invalid commands then no commands are run/parsed.
                {
                    ParseCommands(CommandLine);
                }
            }
        }
        /// <summary>
        /// Method that loads text files into command panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton(object sender, EventArgs e)
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
        private void ClearButton(object sender, EventArgs e)
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
        private void SyntaxButton(object sender, EventArgs e)
        {
            // Essentially the same as the run button but if the commands are valid displays a message telling them there are no errors.
            if (this.CommandPanel.Text != null && !this.CommandPanel.Text.Equals(""))
            {
                CommandValidator validate = new CommandValidator(CommandPanel);
                if (!validate.DoesInvalidCommandExists())
                {
                    MessageBox.Show("Coammnds have no errors.");
                }

            }
            else if (this.CommandLine.Text != null && !this.CommandLine.Text.Equals(""))
            {
                CommandValidator validate = new CommandValidator(CommandLine);
                if (!validate.DoesInvalidCommandExists())
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
        private void SaveButton(object sender, EventArgs e)
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
        /// <param name="commands">TextBox with commands entered by user</param>
        private void ParseCommands(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            // for each line in the text box, trim the line and run the command if it isn't empty
            for (int i = 0; i < numberOfLines; i++)
            {
                string oneLineCommand = commands.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals("") && blockEndLine < currentLine)// if command is not empty and not in a block statement then run command
                {
                    RunCommand(oneLineCommand, commands);
                }
                currentLine++;

            }
        }

        /// <summary>
        /// Gets the value of the variable given
        /// </summary>
        /// <param name="variable">Name of the variable to get the value of</param>
        /// <returns>The integer value of the variable passed in</returns>
        private int GetAssociatedVariableValue(string variable) //used to call the valuse of the assined varable if it is there
        {
            int number = -2;
            if (int.TryParse(variable, out _))// if the number can be parsed as an integer then just return the value as an int
            {
                number = int.Parse(variable);
                return number;
            }
            for (int i = 0; i < variables.Count; i++) // for each variable in the variables list find the variable that is the same as the variable passed in
            {
                if (variables[i].ToLower() == variable.ToLower())
                {

                    number = variableValues[i];// return the value assigned to the variable
                    break;
                }

            }
            return number;

        }

        /// <summary>
        /// Takes each command and runs it accordingly
        /// </summary>
        /// <param name="command">The command to run</param>
        /// <param name="commands">The textbox with all the user input</param>
        public void RunCommand(string command, TextBox commands)
        {
            string[] args = command.Split(' '); //split args
            //removing white spaces in between words
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }

            switch (args[0].ToLower())// switch between each keyword and do the appropriate action
            {
                case "circle":
                    int radius = GetAssociatedVariableValue(args[1]);// this method will get the value of the variable given or return the integer the user gave
                    new Circle(x, y, radius, fillIn).Draw(graphics, drawingPen, drawingBrush);// create new circle object with specified values and draw it
                    break;
                case "rectangle":
                    int width = GetAssociatedVariableValue(args[1]);
                    int height = GetAssociatedVariableValue(args[2]);
                    new Rectangle(x, y, width, height, fillIn).Draw(graphics, drawingPen, drawingBrush);// create rectangle object and draw
                    break;
                case "square":
                    int side = GetAssociatedVariableValue(args[1]);
                    new Square(x, y, side, fillIn).Draw(graphics, drawingPen, drawingBrush);// create square object and draw
                    break;
                case "drawto":
                    int point1 = GetAssociatedVariableValue(args[1]);
                    int point2 = GetAssociatedVariableValue(args[2]);
                    graphics.DrawLine(drawingPen, x, y, point1, point2);// use graphics draw line method to draw line
                    x = point1;// update x value 
                    y = point2;// update y value
                    break;
                case "colour":
                    try
                    {
                        int red = GetAssociatedVariableValue(args[1]);
                        int blue = GetAssociatedVariableValue(args[2]);
                        int green = GetAssociatedVariableValue(args[3]);
                        pencol = Color.FromArgb(red, blue, green);// get colour using RGB values
                        drawingPen.Color = pencol;// set pen colour
                        drawingBrush = new SolidBrush(pencol);// set brush to new object of new colour
                    }
                    catch
                    {
                        pencol = Color.Black;// in case of any exception when getting colour default to black
                        drawingPen.Color = pencol;
                        drawingBrush = new SolidBrush(pencol);
                    }
                    break;
                case "triangle":
                    int p1 = GetAssociatedVariableValue(args[1]);
                    int p2 = GetAssociatedVariableValue(args[2]);
                    int p3 = GetAssociatedVariableValue(args[3]);
                    Point pointa1 = new Point(p1, p2);// create new points from the values given in to draw the triangle from
                    Point pointb1 = new Point(p2, p3);
                    Point pointc1 = new Point(p3, p1);

                    Point[] points = { pointa1, pointb1, pointc1 };// use these points to draw the triangle
                    new Triangle(points, fillIn).Draw(graphics, drawingPen, drawingBrush); // create triangle object and draw
                    break;
                case "clear":
                    graphics.Clear(Color.LightCyan);// clear the drawing panel
                    break;
                case "reset":// reset pen position bac to top left
                    x = 0;
                    y = 0;
                    break;
                case "fillIn": // switch to using brush for shapes and filling in the shapes rather than just the outline
                    fillIn = true;
                    break;
                case "Unfill":// switch back to just drawing the outline and not filling in the shape
                    fillIn = false;
                    break;
                case "moveto":// move the pen postion to the new values
                    x = GetAssociatedVariableValue(args[1]);
                    y = GetAssociatedVariableValue(args[2]);                  
                    break;
                case "var":// add the new variable to the list
                    if (variables.Contains(args[1]))// if variable is already defined then can be reassigned
                    {
                        Tuple<int, int> valueAndPosition = getDefinedVariableValueAndPosition(args[1]);
                        variableValues[valueAndPosition.Item2] = int.Parse(args[3]); // set value in position found in list to new value 
                    }
                    else
                    {
                        variables.Add(args[1].ToLower());
                        variableValues.Add(int.Parse(args[3].Trim()));
                    }
                    break;
                case "factory":// create specified shape with random values
                    Factory shapeFactory = new Factory();
                    Shape shape = shapeFactory.GetShape(args[1]);
                    Random random = new Random();
                    if (args[1].ToLower().Trim() == "circle")
                    {
                        radius = random.Next(display.Width / 4);// using random object set a radius
                        shape.Set(x, y, radius);
                    }
                    else if (args[1].ToLower().Trim() == "rectangle" || args[1].ToLower().Trim() == "square")
                    {
                        width = random.Next(display.Width);// set random width and height
                        height = random.Next(display.Height);
                        if (args[1].ToLower().Trim() == "square") { height = width;}// if square shape is given then set width and height equal to each other
                        shape.Set(x, y, width, height);
                    }
                    else if (args[1].ToLower().Trim() == "triangle")
                    {
                        p1 = random.Next(display.Width);// set random points to draw the triangle with
                        p2 = random.Next(display.Width);
                        p3 = random.Next(display.Width);
                        Point pointa = new Point(p1, p2);
                        Point pointb = new Point(p2, p3);
                        Point pointc = new Point(p3, p1);
                        Point[] pnt = { pointa, pointb, pointc };
                        shape.SetTriangle(x, y, pnt);
                    }
                    shape.Draw(graphics, drawingPen, drawingBrush);// draw the shape that the factory class creates
                    break;
                case "for":
                    {
                        counter = GetAssociatedVariableValue(args[1]);// number of iterations of the for loop
                        int loopStartLine = (GetLoopStartLineNumber(commands));// get the loop start line
                        int loopEndLine = (GetLoopEndLineNumber(commands) - 1);// get the loop end line/ the line of the last command before endloop
                        blockEndLine = loopEndLine; // block started so this line is last command used to make sure we dont parse the command again
                        // for the number of iterations specified repeat the loop commands
                        for (int i = 0; i < counter; i++)
                        {
                            // for each line in the loop block call run command
                            for (int j = loopStartLine; j <= loopEndLine; j++)
                            {
                                string oneLineCommand = commands.Lines[j];
                                oneLineCommand = oneLineCommand.Trim();
                                if (!oneLineCommand.Equals(""))
                                {
                                    RunCommand(oneLineCommand, commands);
                                }
                            }
                        }
                    }
                    break;
                case "if":
                    // get the values from the condition and the operator used
                    int left = GetAssociatedVariableValue(args[1]);
                    int right = GetAssociatedVariableValue(args[3]);
                    string condition = args[2];
                    bool ifResult = IsIfStatementTrue(left, condition, right);// check if the condition is correct depending on the operator
                    int ifStartLine = (GetIfStartLineNumber(commands));
                    int ifEndLine = (GetEndifEndLineNumber(commands) - 1);
                    blockEndLine = ifEndLine; // get end of block so that we dont parse over the same lines again
                    if (ifResult == true)
                    {
                        // if the if statement is true then for each line if the if statement block run the command

                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand = commands.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                RunCommand(oneLineCommand, commands);
                            }
                        }
                        break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns the value and position of the variable in the list
        /// </summary>
        /// <param name="variable">The variable name string that is being checked</param>
        /// <returns>Returns the integer value of the variable and the position it is stored at in the list</returns>
        public Tuple<int, int> getDefinedVariableValueAndPosition(string variable)
        {
            int value = -10000;
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].ToLower().Equals(variable.ToLower()))
                {
                    return new Tuple<int, int>(variableValues[i], i);
                }

            }//this should never be reached as this is only called if the variable is already defined
            return new Tuple<int, int>(value, -1);
        }

        /// <summary>
        /// Get the start line of the if statement
        /// </summary>
        /// <param name="commands">The user input from the textbox</param>
        /// <returns>Integer value of the start line of the if block</returns>
        private int GetIfStartLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            // go through each line in the commands and find the line where the first word is 'if'
            for (int lineNum = currentLine; lineNum < numberOfLines; lineNum++)
            {
                string oneLineCommand = commands.Lines[lineNum];
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
                    return lineNum + 1;// need to add 1 so that we start in the block not at the if statement

                }
            }
            return 0;
        }

        /// <summary>
        /// Get the end line of the if statement
        /// </summary>
        /// <param name="commands">The user input from the textbox</param>
        /// <returns>Integer value of the last command in the if block</returns>
        private int GetEndifEndLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;

            for (int lineNum = currentLine; lineNum < numberOfLines; lineNum++)
            {
                string oneLineCommand = commands.Lines[lineNum];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    return lineNum + 1;

                }
            }
            return 0;
        }

        /// <summary>
        /// Check that the if statement condition is true or false
        /// </summary>
        /// <param name="left"> integer/variable given before the operator</param>
        /// <param name="condition"> the chosen conditional operator i.e. '!='</param>
        /// <param name="right"> integer/variable given after the operator</param>
        /// <returns>Boolean value of whether the if statement condition is true or not</returns>
        private bool IsIfStatementTrue(int left, string condition, int right) 
        {
            bool ifResult = false;
            switch (condition)// switch case to go through each potential operator case and perform the necessary check
            {

                case "==":
                    if (left == right)
                    {
                        ifResult = true;
                    }
                    break;
                case ">":
                    if (left > right)
                    {
                        ifResult = true;
                    }
                    break;
                case "<":
                    if (left < right)
                    {
                        ifResult = true;
                    }
                    break;
                case ">=":
                    if (left >= right)
                    {
                        ifResult = true;
                    }
                    break;
                case "<=":
                    if (left <= right)
                    {
                        ifResult = true;
                    }
                    break;
                case "!=":
                    if (left != right)
                    {
                        ifResult = true;
                    }
                    break;
            }
            return ifResult;
        }

        /// <summary>
        /// Get the end line of the for loop statement
        /// </summary>
        /// <param name="commands">The user input from the textbox</param>
        /// <returns>Integer value of the end line of the for block</returns>
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

        /// <summary>
        /// Get the start line of the for loop statement
        /// </summary>
        /// <param name="commands">The user input from the textbox</param>
        /// <returns>Integer value of the start line of the for block</returns>
        private int GetLoopStartLineNumber(TextBox commands)
        {
            int numberOfLines = commands.Lines.Length;
            int lineNum = 0;
            // for each line in the command check that it starts with for and return the line number + 1
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
                if (firstWord.Equals("for"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }
    }
}
