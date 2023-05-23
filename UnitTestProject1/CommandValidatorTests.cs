using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using GraphicalProgammingLanguage;

namespace UnitTestProject1
{
    /// <summary>
    /// Test class for command validator class methods
    /// </summary>
    [TestClass]
    public class CommandValidatorTests
    {

        //Testing if it validates a line of command
        [TestMethod()]
        public void ValidateTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "Rectangle 100 100 \r\n Circle 5 \r\n triangle 20 40 50 \r\n colour 30 40 40 " +
                "\r\n moveto 100 100 \r\n drawto 200 150 \r\n square 20 \r\n var x = 10 \r\n for 1 \r\n fillin \r\n var x + 10 \r\n factory square \r\n endloop \r\n unfill \r\n reset \r\n clear \r\n " +
                "var y = 12 \r\n if y == 12 then \r\n square y \r\n endif \r\n var y - 2 \n var x = 30 \n for x \n circle x \n colour x 20 y \n endloop";

            textbox.Text = input;
            CommandValidator validation = new CommandValidator(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = false;
            validation.Validate();
            realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        [TestMethod]
        //Checking if this function validates if endif case or not
        public void CheckIfValidationTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "var counter = 5 \r\n If counter == 5 then \r\n Circle 5 \r\n EndIf";
            textbox.Text = input;
            CommandValidator validate = new CommandValidator(textbox);
            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = false;
            validate.CheckLoopAndIfValidation();
            realOutcome = validate.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates an invalid line of command
        [TestMethod()]
        [DataRow("circle -100")]
        [DataRow("circle djhfg")]
        [DataRow("circle")]
        [DataRow("square -100")]
        [DataRow("square djhfg")]
        [DataRow("square")]
        [DataRow("rectangle -100 20")]
        [DataRow("rectangle djhfg 20")]
        [DataRow("rectangle")]
        [DataRow("triangle -100 20 40")]
        [DataRow("triangle djhfg 30 40")]
        [DataRow("triangle")]
        [DataRow("factory hexagon")]
        [DataRow("factory")]
        [DataRow("drawto -100 20")]
        [DataRow("drawto")]
        [DataRow("drawto fff dfgdg")]
        [DataRow("moveto -100 20")]
        [DataRow("moveto")]
        [DataRow("moveto fgfjg fdgfg")]
        [DataRow("for -100")]
        [DataRow("for")]
        [DataRow("for dfhgfghf")]
        [DataRow("endif 34")]
        [DataRow("endloop 34")]
        [DataRow("reset 34")]
        [DataRow("clear 34")]
        [DataRow("fillin 34")]
        [DataRow("unfill 34")]
        [DataRow("colour -100 20m 42")]
        [DataRow("colour")]
        [DataRow("if c ! 20 then")]
        [DataRow("if dfdfv")]
        [DataRow("if 20 == x then")]
        [DataRow("if 20 ! x then")]
        [DataRow("if 20 == 20 than")]
        [DataRow("var c = -100")]
        [DataRow("var c +")]
        [DataRow("var c + 3 + 5 + x")]
        [DataRow("var c ! 23")]
        [DataRow("var c + 12 345 45656")]
        [DataRow("var c - 1000000")]
        [DataRow("somethingRandom")]
        public void InvalidCommandsCheckLineValidationTest(string input)
        {
            TextBox textbox = new TextBox{Text = input};
            CommandValidator validation = new CommandValidator(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = true;
            validation.CheckLineValidation(input);
            realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        [TestMethod()]// test that commands that are invalid that require previous commands to be valid are caught (usually var cases)
        [DataRow("var c = 100 \n var c = -29")]
        [DataRow("var c = 100 \n var c - 1029")]
        [DataRow("var c = 100 \n moveto x 100")]
        [DataRow("var c = 100 \n if c != 20 then \n circle 20")]
        [DataRow("var c = 100 \n for c \n circle 20")]
        public void InvalidCommandsValidateTest(string input)
        {
            TextBox textbox = new TextBox { Text = input };
            CommandValidator validation = new CommandValidator(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = true;
            validation.Validate(false);
            realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates a line of command
        [TestMethod()]
        public void CheckLineValidationTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "Rectangle 100 100";

            textbox.Text = input;
            CommandValidator validation = new CommandValidator(textbox);

            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = false;
            validation.CheckLineValidation(input);
            realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates a variable
        [TestMethod()]
        public void CheckIfVariableDefinedTest()
        {
            String input;
            TextBox textbox = new TextBox();
            input = "var radius = 20 \r\n Circle Radius";

            textbox.Text = input;
            CommandValidator validation = new CommandValidator(textbox);
            Boolean expectedOutcome;
            Boolean realOutcome;
            expectedOutcome = false;
            validation.CheckIfVariableDefined("radius");
            realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        //Testing if it validates a variable
        [TestMethod()]
        [DataRow("var c = 100")]
        [DataRow("")]
        public void InvalidVariablesGetDefinedVariableValueAndPositionTest(String input)
        {
            TextBox textbox = new TextBox{Text = input};
            CommandValidator validation = new CommandValidator(textbox);
            bool expectedOutcome = false;
            validation.Validate();// initialise variable
            Tuple<int, int> valueAndPosition = validation.GetDefinedVariableValueAndPosition("somethingRandom");
            bool realOutcome = validation.DoesInvalidCommandExists();
            Assert.AreEqual(expectedOutcome, realOutcome);
            Assert.AreEqual(-10000, valueAndPosition.Item1);
            Assert.AreEqual(-1, valueAndPosition.Item2);
        }
    }
}
