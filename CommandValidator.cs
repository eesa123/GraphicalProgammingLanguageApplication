using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgammingLanguage
{
    /// <summary>
    /// Class which will be used to parse commands and validate them accordingly.
    /// </summary>
    class CommandValidator
    {
        private int loopmax;//used in loop to evaluate maximum loop instances
        private int loopstart;//used in loop to determine loop start value
        private int loopend; //used in loop to determine loop end value
        private int loopcount;//used in loop to determine loop current iteration value
        private int ifCount;
        private int ifEnd;

        private string[] element; //used to see each part of the commandes inputted in the checker
        private int lineNumber = 0;    // used to show what lines the errors are on

        private bool hasLoop = false;//used in loop
        private bool hasEndLoop = false;//used in loop
        private bool hasIf = false;//used in loop
        private bool hasEndIf = false;//used in loop
        private bool ifResult = false;//used in the if checker
        private bool iffaslse = false;//used to skip the line not used in the if
        private bool check = false;//used in syntax check

        private Boolean invalidCommandExists = false;
        private Boolean isValid = true;
        private TextBox commandText;
        private string[] variables = new string[100];
        private int[] variableValues = new int[100];
        private int varCounter = 0;


        /// <summary>
        /// Constructor which takes Textbox object and validates the commands within it.
        /// </summary>
        /// <param name="commands"></param>
        public CommandValidator(TextBox commands) {
            this.commandText = commands;

            int numberOfLines = commandText.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                String command = commandText.Lines[i];
                command = command.Trim();
                if (!command.Equals(""))
                {
                    checkLineValidation(command);
                    lineNumber = (i + 1);
                    if (!isValid)
                    {
                        MessageBox.Show("Error in line " + lineNumber);
                        isValid = true;
                    }
                }

            }
            checkLoopAndIfValidation();
            if (!isValid)
            {
                invalidCommandExists = true;
            }
        }

        public void checkLoopAndIfValidation()
        {
            int numberOfLines = commandText.Lines.Length;


            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = commandText.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    hasLoop = Regex.IsMatch(oneLineCommand.ToLower(), @"\for\b") || Regex.IsMatch(oneLineCommand.ToLower(), @"\while\b");
                    if (hasLoop)
                    {
                        loopcount = (i + 1);
                    }
                    hasEndLoop = oneLineCommand.ToLower().Contains("endloop");
                    if (hasEndLoop)
                    {
                        loopend = (i + 1);
                    }
                    hasIf = Regex.IsMatch(oneLineCommand.ToLower(), @"\bif\b");
                    if (hasIf)
                    {
                        ifCount = (i + 1);
                    }
                    hasEndIf = oneLineCommand.ToLower().Contains("endif");
                    if (hasEndIf)
                    {
                        ifEnd = (i + 1);
                    }
                }
            }
            if (loopcount > 0)
            {
                hasLoop = true;
            }
            if (loopend > 0)
            {
                hasEndLoop = true;
            }
            if (ifCount > 0)
            {
                hasIf = true;
            }
            if (loopcount > 0)
            {
                hasEndIf = true;
            }
            if (hasLoop)
            {
                if (hasEndLoop)
                {
                    if (loopcount >= loopend)
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
            if (hasIf)
            {
                if (hasEndIf)
                {
                    if (ifCount >= ifEnd)
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
        /// <param name="command"></param>
        public void checkLineValidation(string command)
        {
            String[] keyword = { "circle", "rectangle", "triangle", "square", "drawto", "moveto", "for", "while", "if", "endif", "endloop", "var", "colour" };
            String[] shapes = { "circle", "rectangle", "triangle", "square" };
            String[] operators = { "==", ">", "<", ">=", "=<", "!=" };
            command = Regex.Replace(command, @"\s+", " ");
            string[] args = command.Split(' ');

            //removing white spaces in between words

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }
            String firstWord = args[0].ToLower();
            Boolean firstWordIsKeyword = keyword.Contains(firstWord);
            if (firstWordIsKeyword)
            {
                Boolean firstWordIsShape = shapes.Contains(args[0].ToLower());
                if (firstWordIsShape)
                {
                    if (args[0].ToLower().Equals("circle"))
                    {
                        if (args.Length == 2)
                        {
                            Boolean isInt = args[1].All(char.IsDigit);
                            if (!isInt)
                            {
                                //check if variable already defined with value

                                Boolean isVariable = variables.Contains(args[1].ToLower());
                                if (isVariable)
                                {
                                   checkIfVariableDefined(args[1]);
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("colour"))
                    {
                        String subCommand = command.Substring(6, (command.Length - 6));
                        String[] parms = subCommand.Split(',');

                        if (parms.Length == 3)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    //if it is not a variables value being used then invalid

                                    Boolean isVariable = variables.Contains(parms[i].ToLower());
                                    if (isVariable)
                                    {
                                        checkIfVariableDefined(args[1]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("rectangle"))
                    {
                        String subCommand = command.Substring(9, (command.Length - 9));
                        String[] parms = subCommand.Split(',');

                        if (parms.Length == 2)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    //if it is not a variables value being used then invalid

                                    Boolean isVariable = variables.Contains(parms[i].ToLower());
                                    if (isVariable)
                                    {
                                        checkIfVariableDefined(args[1]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("triangle"))
                    {
                        String subCommand = command.Substring(8, (command.Length - 8));
                        String[] parms = subCommand.Split(',');

                        if (parms.Length == 3)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    checkIfVariableDefined(args[1]);
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("square"))
                    {
                        String subCommand = command.Substring(6, (command.Length - 6));
                        String[] parms = subCommand.Split(',');

                        if (parms.Length == 1)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    checkIfVariableDefined(args[1]);
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else { }
                }
                else if (firstWord.Equals("end"))
                {
                    if (args.Length == 2)
                    {
                        if (!args[1].Equals("loop"))
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("for"))
                {
                    if (args.Length >= 4 && args.Length <= 6)
                    {
                        Boolean isInt = args[1].All(char.IsDigit);
                        if (isInt)
                        {
                            if (shapes.Contains(args[2].ToLower()))
                            {

                                Boolean hasPlus = args[3].Contains('+');
                                if (hasPlus)
                                {
                                    string[] words2 = args[3].Split('+');
                                    for (int i = 0; i < words2.Length; i++)
                                    {
                                        words2[i] = words2[i].Trim();
                                    }
                                    Boolean firstWordIsVariable = variables.Contains(words2[0].ToLower());
                                    if (firstWordIsVariable)
                                    {
                                        checkIfVariableDefined(args[1]);
                                    }
                                    else
                                    {
                                        if (words2.Length != 2)
                                        {
                                            isValid = false;
                                        }
                                        else
                                        {
                                            //third char should be int to be valid

                                            Boolean isInt2 = words2[1].All(char.IsDigit);
                                            if (!isInt2)
                                            {
                                                isValid = false;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    if (variables.Contains(args[3].ToLower()))
                                    {
                                        if (args[4].Trim().Equals("+"))
                                        {
                                            Boolean isInt3 = args[5].All(char.IsDigit);
                                            if (!isInt3)
                                            {
                                                isValid = false;
                                            }
                                        }
                                        else
                                        {
                                            Boolean hasPlus2 = args[4].Contains('+');
                                            if (hasPlus2)
                                            {
                                                string[] words2 = args[4].Split('+');
                                                for (int i = 0; i < words2.Length; i++)
                                                {
                                                    words2[i] = words2[i].Trim();
                                                }
                                                if (words2.Length == 2)
                                                {
                                                    Boolean isInt2 = words2[1].All(char.IsDigit);
                                                    if (!isInt2)
                                                    {
                                                        isValid = false;
                                                    }
                                                }
                                                else
                                                {
                                                    isValid = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    if (args.Length == 5)
                    {
                        if (variables.Contains(args[1].ToLower()))
                        {
                            if (operators.Contains(args[2].ToLower()))
                            {
                                Boolean isInt = args[3].All(char.IsDigit);
                                if (isInt)
                                {
                                    if (args[4].ToLower().Equals("then"))
                                    {
                                        isValid = true;
                                    }
                                    else { isValid = false; Console.WriteLine("1"); }
                                }
                                else { isValid = false; Console.WriteLine("2"); }

                            }
                            else { isValid = false; Console.WriteLine("3"); }
                        }
                        else { isValid = false; Console.WriteLine("4"); }

                    }
                    else
                    {
                        isValid = false;
                        Console.WriteLine("5");
                    }

                }
                else if (firstWord.Equals("endif"))
                {
                    if (args.Length != 1)
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("drawto") || firstWord.Equals("moveto"))
                {
                    String subCommand = command.Substring(6, (command.Length - 6));
                    String[] parms = subCommand.Split(',');

                    if (parms.Length == 2)
                    {
                        Boolean isInt = false;
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            isInt = parms[i].All(char.IsDigit);
                            if (!isInt)
                            {
                               checkIfVariableDefined(args[1]);
                            }
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("var"))
                {
                    String subCommand = command.Substring(3, (command.Length - 3));
                    String[] parms = subCommand.Split('=');

                    if (parms.Length == 2)
                    {
                            Boolean isInt = false;
                            parms[0] = parms[0].Trim();
                            isInt = parms[1].Trim().All(char.IsDigit);
                            if (variables.Contains(parms[0]) || !isInt)
                            {
                                isValid = false;
                            }
                    }
                    else
                    {
                        isValid = false;
                    }
                    if (isValid)
                    {
                        Console.WriteLine("appended" + parms[1]);
                        variables[varCounter] = parms[0].ToLower();
                        variableValues[varCounter] = int.Parse(parms[1]);
                        varCounter++;
                    }
                }
            }
            if (!isValid)
            {
                invalidCommandExists = true;
            }

        }
        public void checkIfVariableDefined(string variable)
        {
            int value = -10000;
            if (variables[0] == null) { isValid = false;  return; } // first element is null therefore no variables defined.
            for (int i = 0; i<variables.Length; i++)  
            {
                if (variables[i].ToLower().Equals(variable.ToLower()))
                {
                    value = variableValues[i];
                    if (value == -10000)
                    {
                        MessageBox.Show("Varaible is not defined");
                        isValid = false;
                        continue;
                    }
                }

            }
            if (value == -10000)
            {
                MessageBox.Show("Varaible is not defined");
                isValid = false;
            }
            isValid = true;
        }

    public Boolean doesInvalidCommandExists()
        {
            return this.invalidCommandExists;
        }

    }
}
