using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string[] element; //used to see each part of the commandes inputted in the checker
        private string[] vars = new string[50];    //stores the variables names
        private int[] varsParams = new int[50];   //stores the varables numbers in the corrsponding position to vars
        private int[] errorline = new int[50];    // used to show what lines the errors are on

        private bool loopflag = false;//used in loop
        private bool ifResult = false;//used in the if checker
        private bool iffaslse = false;//used to skip the line not used in the if
        private bool check = false;//used in syntax check

        CommandValidator() { } // TODO: implement validator

    }
}
