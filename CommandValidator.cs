﻿using System;
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
        private int loopend; //used in loop to determine loop end value
        private int loopcount;//used in loop to determine loop number of lines value
        private int ifCount;//used in if to determine if number of lines value
        private int ifEnd;//used in if to determine if end line value

        private int lineNumber = 0;    // used to show what lines the errors are on

        private bool hasLoop = false;//used in loop
        private bool hasEndLoop = false;//used in loop
        private bool hasIf = false;//used in loop
        private bool hasEndIf = false;//used in loop

        private Boolean invalidCommandExists = false;
        private Boolean isValid = true;
        private TextBox commandText;
        private string[] variables = new string[100];
        private int[] variableValues = new int[100];
        private int varCounter = 0;
        private string errorMessages = "";


        /// <summary>
        /// Constructor which takes Textbox object and validates the commands within it.
        /// </summary>
        /// <param name="commands"></param>
        public CommandValidator(TextBox commands) {
            this.commandText = commands;

            int numberOfLines = commandText.Lines.Length;
            for (int i = 0; i < numberOfLines; i++)
            {
                string command = commandText.Lines[i];
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
                string oneLineCommand = commandText.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (!oneLineCommand.Equals(""))
                {
                    hasLoop = Regex.IsMatch(oneLineCommand.ToLower(), @"\bfor\b");
                    if (hasLoop)
                    {
                        loopcount = (i + 1);
                    }
                    hasEndLoop = oneLineCommand.ToLower().Equals("endloop");
                    if (hasEndLoop)
                    {
                        loopend = (i + 1);
                    }
                    hasIf = Regex.IsMatch(oneLineCommand.ToLower(), @"\bif\b");
                    if (hasIf)
                    {
                        ifCount = (i + 1);
                    }
                    hasEndIf = oneLineCommand.ToLower().Equals("endif");
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
            if (lineNumber > loopend) 
            { 
                hasLoop = false; 
            }
            if (lineNumber > ifEnd)
            {
                hasIf = false;
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
            string[] keyword = { "factory", "circle", "rectangle", "triangle", "square", "drawto", "moveto", "for", "if", "endif", "endloop", "var", "reset", "colour", "fillIn", "unfill" };
            string[] shapes = { "circle", "rectangle", "triangle", "square" };
            string[] operators = { "==", ">", "<", ">=", "=<", "!=" };
            command = Regex.Replace(command, @"\s+", " ");
            string[] args = command.Split(' ');

            //removing white spaces in between words

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].Trim();
            }
            string firstWord = args[0].ToLower();
            Boolean firstWordIsKeyword = keyword.Contains(firstWord);
            if (firstWordIsKeyword)
            {
                Boolean firstWordIsShape = shapes.Contains(args[0].ToLower()); //TODO add error messages for each invalid command types
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
                            else if (isInt && int.Parse(args[1]) <= 0)
                            {
                                isValid = false;
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    else if (args[0].ToLower().Equals("rectangle"))
                    {
                        string subCommand = command.Substring(9, (command.Length - 9));
                        string[] parms = subCommand.Trim().Split(' ');

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
                                        checkIfVariableDefined(parms[i]);
                                    }
                                }
                                else if (isInt && int.Parse(parms[i]) <= 0)
                                {
                                    isValid = false;
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
                        string subCommand = command.Substring(8, (command.Length - 8));
                        string[] parms = subCommand.Trim().Split(' ');

                        if (parms.Length == 3)
                        {
                            Boolean isInt = false;
                            for (int i = 0; i < parms.Length; i++)
                            {
                                parms[i] = parms[i].Trim();
                                isInt = parms[i].All(char.IsDigit);
                                if (!isInt)
                                {
                                    checkIfVariableDefined(parms[i]);
                                }
                                else if (isInt && int.Parse(parms[i]) <= 0)
                                {
                                    isValid = false;
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
                            else if (isInt && int.Parse(args[1]) <= 0)
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
                else if (args[0].ToLower().Equals("colour"))
                {
                    string subCommand = command.Substring(6, (command.Length - 6));
                    string[] parms = subCommand.Trim().Split(' ');

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
                                    checkIfVariableDefined(parms[i]);
                                }
                            }
                            else if (isInt && int.Parse(parms[i]) < 0)
                            {
                                isValid = false;
                            }
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
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
                else if (firstWord.Equals("factory"))
                {
                    if (args.Length == 2)
                    {
                        if (!shapes.Contains(args[1].ToLower().Trim()))
                        {
                            isValid = false;
                        }
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("fillIn") || firstWord.Equals("fill"))
                {
                    if (args.Length == 2)
                    {
                        if (!args[1].ToLower().Equals("in"))
                        {
                            isValid = false;
                        }
                    }
                    else if (firstWord.Equals("fillIn") && args.Length > 1)
                    {
                        isValid = false;
                    }
                }
                else if (firstWord.Equals("unfill") && args.Length > 1)
                    {
                        isValid = false;
                    }
                else if (firstWord.Equals("reset") && args.Length > 1)
                {
                    isValid = false;
                }
                else if (firstWord.Equals("for"))
                {
                    if (args.Length == 2)
                    {
                        Boolean isInt = args[1].All(char.IsDigit);
                        if (!isInt)
                        {
                            Boolean firstWordIsVariable = variables.Contains(args[1].ToLower());
                            if (firstWordIsVariable)
                            {
                                checkIfVariableDefined(args[1]);
                            }
                            else
                            {
                                isValid = false;
                            }
                        }
                        else if (isInt && int.Parse(args[1]) <= 0)
                        {
                            isValid = false;
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
                        if (variables.Contains(args[1].ToLower()) || args[1].All(char.IsDigit)) // can allow non variable conditions for if
                        {
                            if (operators.Contains(args[2].ToLower()))
                            {
                                Boolean isInt = args[3].All(char.IsDigit);
                                if (isInt)
                                {
                                    if (args[4].ToLower().Equals("then"))
                                    {
                                        errorMessages += "If statement must end with 'then'. \n";

                                    }
                                    else { isValid = false; }
                                }
                                else { isValid = false; }

                            }
                            else { isValid = false; }
                        }
                        else { isValid = false; }

                    }
                    else
                    {
                        isValid = false;
                    }

                }
                else if (firstWord.Equals("endif") || firstWord.Equals("endloop"))
                {
                    if (args.Length != 1)
                    {
                        errorMessages += "End commands should have nothing come after them. \n";                    }
                }
                else if (firstWord.Equals("drawto") || firstWord.Equals("moveto"))
                {
                    string subCommand = command.Substring(6, (command.Length - 6)).Trim();
                    string[] parms = subCommand.Split(' ');

                    if (parms.Length == 2)
                    {
                        Boolean isInt = false;
                        for (int i = 0; i < parms.Length; i++)
                        {
                            parms[i] = parms[i].Trim();
                            isInt = parms[i].All(char.IsDigit);
                            if (!isInt)
                            {
                                checkIfVariableDefined(parms[i]);
                            }
                            else if (isInt && int.Parse(parms[i]) < 0)
                            {
                                errorMessages += "Move/draw command requires two positive integers. \n";

                            }
                        }
                    }
                    else
                    {
                        errorMessages += "Move/draw command does not have the right amount of parameters, two positive integers are needed. \n";
                    }
                }
                else if (firstWord.Equals("var"))
                {
                    string subCommand = command.Substring(3, (command.Length - 3));
                    string[] parms = subCommand.Trim().Split('=');

                    if (parms.Length == 2)
                    {
                        Boolean isInt = false;
                        parms[0] = parms[0].Trim();
                        isInt = parms[1].Trim().All(char.IsDigit);
                        if (variables.Contains(parms[0]) || !isInt)
                        {
                            errorMessages += "Variable " + parms[0] + " has already been assigned a value. \n";
                        }
                        else if (isInt && int.Parse(parms[1].Trim()) < 0)
                        {
                            errorMessages += "Variable " + parms[0] + " cannot have a negative value. \n";
                        }
                    }
                    else
                    {
                        errorMessages += "Var command must follow pattern 'var (string) = (int)' and the value must be positive. \n";
                    }
                    if (isValid)
                    {
                        variables[varCounter] = parms[0].ToLower();
                        variableValues[varCounter] = int.Parse(parms[1].Trim());
                        varCounter++;
                    }
                }
            }
            else { isValid = false; errorMessages += "Command" + args[0] + " is not a valid command. \n"; }
            if (!isValid)
            {
                invalidCommandExists = true;
            }

        }
        public void checkIfVariableDefined(string variable)
        {
            int value = -10000;
            if (variables[0] == null) { isValid = false; errorMessages += "No variables have been defined. \n"; return; } // first element is null therefore no variables defined.
            for (int i = 0; i<variables.Length; i++)  
            {
                if (variables[i] == null) { break; } // reached null objects meaning no more variables so break
                else if (variables[i].ToLower().Equals(variable.ToLower()))
                {
                    value = variableValues[i];
                    if (value < 0)
                    {
                        errorMessages += "Variable " + variable + " has not been assigned a value. \n";
                        isValid = false;
                        return;
                    }
                }

            }
            if (value < 0)
            {
                errorMessages += "Variable " + variable + " has not been defined. \n";
                isValid = false;
            }
        }

    public Boolean doesInvalidCommandExists()
        {
            return this.invalidCommandExists;
        }

    }
}
