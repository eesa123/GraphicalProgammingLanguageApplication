using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Class which will be used to parse commands and validate them accordingly.
    /// </summary>
    class CommandValidator
    {
        private int loopEnd; //used in loop to determine loop end line value
        private int loopStart;//used in loop to determine current loop iteration
        private int ifStart;//used in if to determine current if iteration
        private int ifEnd;//used in if to determine if end line value

        private int lineNumber = 0;// used to show what lines is currently being validated

        private bool hasLoop = false;//used to determine if commands contain a for loop
        private bool hasEndLoop = false;//used to determine if commands contain an endloop
        private bool hasIf = false;//used to determine if commands contain an if statement
        private bool hasEndIf = false;//used to determine if commands contain an endif

        private bool invalidCommandExists = false;//used to check if an invalid command was given
        private bool isValid = true;//used to determine if the current command is valid
        private TextBox commandText;//the commands inputted by the user into the textbox that is passed into the class

        private List<string> variables = new List<string>();//used to store the variables defined
        private List<int> variableValues = new List<int>();//used to store the corresponding values to the defined variables
        private string errorMessages = "";//used to store the error messages


        /// <summary>
        /// Constructor which takes Textbox object and validates the commands within it.
        /// </summary>
        /// <param name="commands">Textbox from Form class which contains text commands.</param>
        public CommandValidator(TextBox commands) {
            this.commandText = commands;
            // For each line in the text the line is taken and trimmed and then validated
            int numberOfLines = commandText.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                string command = commandText.Lines[i];
                command = command.Trim();
                if (!command.Equals(""))//check command is not empty
                {
                    CheckLineValidation(command);//validate command
                    lineNumber = (i + 1);//as we start at 0 and the line numbr starts at 1 to the user we add 1
                    if (!isValid)
                    {
                        MessageBox.Show("Error in line " + lineNumber + "\nErrors include: \n"+errorMessages);// for each error the line number is given as well as the errors found so far
                        isValid = true;//reset back to true so next line can be validated
                    }
                }

            }
            CheckLoopAndIfValidation();// check the loop and if statements are properly done with an if/for statement having a corresponding end statement after.
            if (!isValid)
            {
                invalidCommandExists = true;
            }
        }

        /// <summary>
        /// Check the textbox and see if it has an if or for loop statement and then
        /// </summary>
        public void CheckLoopAndIfValidation()
        {
            int numberOfLines = commandText.Lines.Length;


            for (int i = 0; i < numberOfLines; i++)
            {
                // go through each line and fine the for or if statement
                string oneLineCommand = commandText.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    hasLoop = Regex.IsMatch(oneLineCommand.ToLower(), @"\bfor\b");// using regex to see if statement starts with for
                    if (hasLoop)
                    {
                        loopStart = (i + 1);// as we start at 0 in the for loop iteration the linenumber is actually 0 so add 1 to i for loop line start
                    }
                    hasEndLoop = oneLineCommand.ToLower().Equals("endloop");// regex is not needed as statement should be by itself
                    if (hasEndLoop) 
                    { 
                        loopEnd = (i + 1);
                    }
                    hasIf = Regex.IsMatch(oneLineCommand.ToLower(), @"\bif\b");// using regex to see if statement starts with if
                    if (hasIf)
                    {
                        ifStart = (i + 1);
                    }
                    hasEndIf = oneLineCommand.ToLower().Equals("endif");
                    if (hasEndIf)
                    {
                        ifEnd = (i + 1);
                    }
                }
            }
            if (loopStart > 0 && lineNumber <= loopEnd && loopEnd > 0)// checking that loop start is on line 1 or further and that loop end exists and is passed the current line number
            {
                hasLoop = true;
                hasEndLoop = true;
            }
            else if (loopStart > 0 && loopEnd <=0)// if loop start exists but no loop end then end if is not given
            {
                hasLoop = true;
                hasEndLoop = false;
            }
            if (ifStart > 0 && lineNumber <= ifEnd && ifEnd > 0)// same checks for is start and end
            {
                hasIf = true;
                hasEndIf = true;
            }
            else if (ifStart > 0 && ifEnd <=0)
            {
                hasIf = true;
                hasEndIf = false;
            }
            if (hasLoop)//if there's a loop we check that there's an endloop and that the end loop is not on the same line
            {
                if (hasEndLoop)
                {
                    if (loopStart >= loopEnd)
                    {
                        isValid = false;
                        MessageBox.Show("'ENDLOOP' must be after loop start");
                    }
                }
                else
                {
                    isValid = false;
                    MessageBox.Show("Loop Not Ended with 'ENDLOOP'");
                }
            }
            if (hasIf)// same check for if statement
            {
                if (hasEndIf)
                {
                    if (ifStart >= ifEnd)
                    {
                        isValid = false;
                        MessageBox.Show("'ENDIF' must be after IF");
                    }
                }
                else
                {
                    isValid = false;
                    MessageBox.Show("IF Not Ended with 'ENDIF'");
                }
            }
        }

        /// <summary>
        /// Validates each line in the commands passed in.
        /// </summary>
        /// <param name="command">This is each line passed in from the textbox on its own as a string object</param>
        public void CheckLineValidation(string command)
        {
            // using keywords to validate the commands passed and make sure they start with one of the valid keywords
            string[] keyword = { "factory", "circle", "rectangle", "triangle", "square", "drawto", "moveto", "for", "if", "endif", "endloop", "var", "reset", "colour", "fillIn", "unfill", "clear" };
            // define drawable shapes to validate them separately
            string[] shapes = { "circle", "rectangle", "triangle", "square" };
            // define operators to ensure if statement uses correct conditionals
            string[] operators = { "==", ">", "<", ">=", "<=", "!=" };
            command = Regex.Replace(command, @"\s+", " ");// taking multiple spaces in the command and replacing them with a single space
            string[] args = command.Split(' ');// splitting commmand by space

            //removing white spaces in between words
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }
            string firstWord = args[0].ToLower();
            bool firstWordIsKeyword = keyword.Contains(firstWord);// check if command is one of valid keywords
            if (firstWordIsKeyword)
            {
                bool firstWordIsShape = shapes.Contains(args[0].ToLower());// check if first word is a shape
                if (firstWordIsShape)
                {
                    if (args[0].ToLower().Equals("circle"))
                    {
                        if (args.Length == 2) // circle command should only be followed by radius
                        {
                            bool isInt = int.TryParse(args[1], out _); // check if radius is an integer
                            if (!isInt)
                            {
                                //if radius is not an integer then check it is a variable that is defined
                                bool isVariable = variables.Contains(args[1].ToLower());
                                if (isVariable)
                                {
                                    CheckIfVariableDefined(args[1]);
                                }
                            }
                            // if radius is an integer check that it is greater than 0
                            else if (isInt && int.Parse(args[1]) <= 0)
                            {
                                errorMessages += "Circle radius must be greater than 0. \n";// adding error message to be displayed in user interface
                                isValid = false;
                            }
                        }
                        else
                        {
                            errorMessages += "Circle command must be followed by a postive integer/variable. \n";
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("rectangle"))
                    {
                        string subCommand = command.Substring(9, (command.Length - 9));// remove rectangle from the command so that the width and height can be validated on their own
                        string[] parms = subCommand.Trim().Split(' ');

                        if (parms.Length == 2)
                        {
                            bool isInt = false;
                            for (int i = 0; i < parms.Length; i++)// go through each parameter (width and height) and validate it as either an integer or defined variable
                            {
                                parms[i] = parms[i].Trim();
                                isInt = int.TryParse(args[i], out _);
                                if (!isInt)
                                {
                                    //if it is not a variables value being used then invalid
                                    bool isVariable = variables.Contains(parms[i].ToLower());
                                    if (isVariable)
                                    {
                                        CheckIfVariableDefined(parms[i]);
                                    }
                                }
                                else if (isInt && int.Parse(parms[i]) <= 0)// check the value is greater than 0
                                {
                                    errorMessages += "Rectangle width and heigh must be greater than 0. \n";
                                    isValid = false;
                                }
                            }
                        }
                        else
                        {
                            errorMessages += "Rectangle command must be followed by 2 positive integers/variables. \n";
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("triangle"))// same checks for triangle validating the values
                    {
                        string subCommand = command.Substring(8, (command.Length - 8));
                        string[] parms = subCommand.Trim().Split(' ');

                        if (parms.Length == 3)
                        {
                            bool isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = int.TryParse(args[i], out _);
                                if (!isInt)
                                {
                                    CheckIfVariableDefined(parms[i]);
                                }
                                else if (isInt && int.Parse(parms[i]) <= 0)
                                {
                                    errorMessages += "Triangle point values must be positive integers/variables. \n";
                                    isValid = false;
                                }
                            }
                        }
                        else
                        {
                            errorMessages += "Triangle command must be followed by 3 integers or variables. \n";
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("square"))// value checks for square
                    {
                        if (args.Length == 2)
                        {
                            bool isInt = int.TryParse(args[1], out _);
                            if (!isInt)
                            {
                                //check if variable already defined with value

                                bool isVariable = variables.Contains(args[1].ToLower());
                                if (isVariable)
                                {
                                    CheckIfVariableDefined(args[1]);
                                }
                            }
                            else if (isInt && int.Parse(args[1]) <= 0)
                            {
                                errorMessages += "Square length value must be greater than 0. \n";
                                isValid = false;
                            }
                        }
                        else
                        {
                            errorMessages += "Square command must be followed by a positive integer or variable. \n";
                            isValid = false;
                        }
                    }
                }
                else if (args[0].ToLower().Equals("colour"))
                {
                    string subCommand = command.Substring(6, (command.Length - 6));
                    string[] parms = subCommand.Trim().Split(' ');

                    if (parms.Length == 3)
                    {
                        bool isInt = false;
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            isInt = int.TryParse(args[i], out _);
                            if (!isInt)
                            {
                                //if it is not a variables value being used then invalid

                                bool isVariable = variables.Contains(parms[i].ToLower());
                                if (isVariable)
                                {
                                    CheckIfVariableDefined(parms[i]);
                                }
                            }
                            else if (isInt && (int.Parse(parms[i]) < 0 || int.Parse(parms[i]) > 255))// validating colour values are within range of 0 to 255
                            {
                                errorMessages += "Colour values must be between 0 and 255. \n";
                                isValid = false;
                            }
                        }
                    }
                    else
                    {
                        errorMessages += "Colour command should be followed by 3 postive integers/variables with values between 0 and 255. \n";
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("factory"))
                {
                    if (args.Length == 2)
                    {
                        if (!shapes.Contains(args[1].ToLower().Trim()))// validate that after factory command a shape keyword is given
                        {
                            errorMessages += "Factory command shape given is invalid, it should be one of: 'Rectangle', 'Square', 'Circle', 'Triangle'. \n";
                            isValid = false;
                        }
                    }
                    else
                    {
                        errorMessages += "Factory command length is invalid, it should be followed by one of: 'Rectangle', 'Square', 'Circle', 'Triangle'. \n";
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("fillIn") || firstWord.Equals("fill"))
                // if fillin or fill is given validate that it is either on its own or followed by in in the case of fill
                {
                    if (args.Length == 2)
                    {
                        if (!args[1].ToLower().Equals("in"))
                        {
                            errorMessages += "Fill command should be followed by 'in'. \n";
                            isValid = false;
                        }
                    }
                    else if (firstWord.Equals("fillIn") && args.Length > 1)
                    {
                        errorMessages += "fillIn command should have nothing after it. \n";
                        isValid = false;
                    }
                }
                // unfill, reset, clear should all be by themselves
                else if (firstWord.Equals("unfill") && args.Length > 1)
                    {
                    errorMessages += "Unfill command should have nothing after it. \n";
                    isValid = false;
                    }
                else if (firstWord.Equals("reset") && args.Length > 1)
                {
                    errorMessages += "Reset command should have nothing after it. \n";
                    isValid = false;
                }
                else if (firstWord.Equals("clear") && args.Length > 1)
                {
                    errorMessages += "clear command should have nothing after it. \n";
                    isValid = false;
                }
                else if (firstWord.Equals("for"))
                {
                    if (args.Length == 2)// for loop should be followed by iteration value
                    {
                        bool isInt = int.TryParse(args[1], out _);
                        if (!isInt)
                        {
                            bool firstWordIsVariable = variables.Contains(args[1].ToLower());
                            if (firstWordIsVariable) // allow variables for iteration value
                            {
                                CheckIfVariableDefined(args[1]);
                            }
                            else
                            {
                                errorMessages += "For loop must use integer for iteration value'. \n";
                                isValid = false;
                            }
                        }
                        else if (isInt && int.Parse(args[1]) <= 0)
                        {
                            errorMessages += "For loop must use positive integer above 0 for iteration value'. \n";
                            isValid = false;
                        }
                    }
                    else
                    {
                        errorMessages += "For loop must follow pattern 'for (int)'. \n";
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    if (args.Length == 5)
                    {
                        if (variables.Contains(args[1].ToLower()) || args[1].All(char.IsDigit)) // can allow variable and integer conditions for if
                        {
                            if (operators.Contains(args[2].ToLower()))// check that condition has valid operator 
                            {
                                bool isInt = int.TryParse(args[3], out _);
                                if (isInt || variables.Contains(args[3].ToLower()))
                                {
                                    if (args[4].ToLower().Equals("then"))// if statement should be followed by then
                                    {
                                        isValid = true;

                                    }
                                    else { isValid = false; errorMessages += "If statement must end with 'then'. \n";}
                                }
                                else { isValid = false; errorMessages += "If statement condition must use defined variables or integers'. \n";}
                            }
                            else { isValid = false; errorMessages += "If statement condition must use one of these operators: '==', '<', '>', '!=', '<=', '>='. \n"; }
                        }
                        else { isValid = false; errorMessages += "If statement condition must use defined variables or integers'. \n"; }
                    }
                    else
                    {
                        isValid = false; errorMessages += "If statement length is not valid, must follow pattern: 'if (int) (condition) (int) then'. \n";
                    }

                }
                else if (firstWord.Equals("endif") || firstWord.Equals("endloop"))// end statement validations are the same
                {
                    if (args.Length != 1)
                    {
                        isValid = false;
                        errorMessages += "End commands should have nothing come after them. \n";                    }
                }
                else if (firstWord.Equals("drawto") || firstWord.Equals("moveto"))// move and draw to commands have the same input to be validated
                {
                    string subCommand = command.Substring(6, (command.Length - 6)).Trim();
                    string[] parms = subCommand.Split(' ');

                    if (parms.Length == 2)
                    {
                        bool isInt = false;
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            isInt = int.TryParse(parms[i], out _);
                            if (!isInt)
                            {
                                CheckIfVariableDefined(parms[i]);
                            }
                            else if (isInt && int.Parse(parms[i]) < 0)
                            {
                                isValid = false;
                                errorMessages += "Move/draw command requires two positive integers. \n";

                            }
                        }
                    }
                    else
                    {
                        isValid = false;
                        errorMessages += "Move/draw command does not have the right amount of parameters, two positive integers are needed. \n";
                    }
                }
                else if (firstWord.Equals("var"))
                {
                    string subCommand = command.Substring(3, (command.Length - 3));
                    string[] parms = subCommand.Trim().Split('=');// split command by assignment 

                    if (parms.Length == 2)
                    {
                        bool isInt = false;
                        parms[0] = parms[0].Trim();
                        isInt = int.TryParse(parms[1], out _);
                        if (variables.Contains(parms[0]))// if variable is already defined then can be reassigned
                        {
                            Tuple<int, int> valueAndPosition = getDefinedVariableValueAndPosition(parms[0]);
                            if (int.Parse(parms[1]) < 0)
                            {
                                isValid = false;
                                errorMessages += "Variable " + parms[0] + " cannot have a negative value. \n";
                            }
                            else
                            {
                                variableValues[valueAndPosition.Item2] = int.Parse(parms[1]); // set value in position found in list to new value 
                            }
                        }
                        else if (isInt && int.Parse(parms[1].Trim()) < 0)
                        {
                            isValid = false;
                            errorMessages += "Variable " + parms[0] + " cannot have a negative value. \n";
                        }
                    }
                    else
                    {
                        isValid = false;
                        errorMessages += "Var command must follow pattern 'var (string) = (int)' and the value must be positive. \n";
                    }
                    if (isValid)
                    {
                        variables.Add(parms[0].ToLower());// add variable to list
                        variableValues.Add(int.Parse(parms[1].Trim()));// add assigned value to list
                    }
                }
            }
            else { isValid = false; errorMessages += "Command " + args[0] + " is not a valid command. \n"; } // if command is not a keyword then it is invalid
            if (!isValid)
            {
                invalidCommandExists = true;
            }

        }

        /// <summary>
        /// Check if variable string given is defined and return value
        /// </summary>
        /// <param name="variable">The variable name string that is being checked</param>
        public void CheckIfVariableDefined(string variable)
        {
            int value = -10000;
            if (variables.Count == 0) { isValid = false; errorMessages += "No variables have been defined. \n"; return; } // Length is 0 therefore no variables defined.
            for (int i = 0; i<variables.Count; i++)  
            {
                if (variables[i].ToLower().Equals(variable.ToLower()))
                {
                    value = variableValues[i];
                    if (value < 0)// if value is assigned no value or a negative value then error displayed
                    {
                        errorMessages += "Variable " + variable + " has not been assigned a value. \n";
                        isValid = false;
                        return;
                    }
                }

            }// is variable is not defined then invalid
            if (value < 0)
            {
                errorMessages += "Variable " + variable + " has not been defined. \n";
                isValid = false;
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
            if (variables.Count == 0)
            {
                isValid = false; errorMessages += "No variables have been defined. \n"; return new Tuple<int, int>(value, -1);
            } // Length is 0 therefore no variables defined.
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].ToLower().Equals(variable.ToLower()))
                {
                    return new Tuple<int, int>(variableValues[i], i);
                }

            }// is variable is not defined then invalid
            if (value < 0)
            {
                errorMessages += "Variable " + variable + " has not been defined. \n";
                isValid = false;
            }
            return new Tuple<int, int>(value, -1);
        }

        /// <summary>
        /// Returns if there was an invalid command entered when validating the commands
        /// </summary>
        /// <returns>Boolean value of invalidCommandExists attribute detailing if an invalid command was in the textbox object that was validated</returns>
        public bool DoesInvalidCommandExists()
        {
            return this.invalidCommandExists;
        }

    }
}
