using GraphicalProgammingLanguage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace UnitTestProject1
{
    /// <summary>
    ///  Test class for Form1 class methods
    /// </summary>
    [TestClass]
    public class FormTests
    {
        [TestMethod()]
        public void ParseCommandsTest()// should validate that command inout is parsed and ran without any exceptions/errors being thrown.
        {
            String input;
            TextBox textbox = new TextBox();
            input = "Rectangle 100 100 \r\nCircle 5 \r\ntriangle 20 40 50 \r\ncolour 30 40 40 " +
                "\r\nmoveto 100 100 \r\ndrawto 200 150 \r\nsquare 20 \r\nvar x = 10 \r\nfor 1 \r\nfillin \r\nvar x + 10 \r\nfactory square \r\nendloop \r\nunfill \r\nreset \r\nclear \r\n " +
                "var y = 12 \r\nif y == 12 then \r\nsquare y \r\nendif \r\nvar y - 2 \nvar x = 3 \nfactory circle \nfactory triangle \nfactory rectangle " +
                "\nfillin \nRectangle 100 100 \r\nCircle 5 \r\ntriangle 20 40 50 \nsquare 70";

            textbox.Text = input;
            Form1 form = new Form1();
            try
            {
                form.ParseCommands(textbox);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod()]// test that each different type of operator used in an if statement returns true
        [DataRow(1,"<",2, true)]
        [DataRow(5, "<=", 5, true)]
        [DataRow(5, ">=", 2, true)]
        [DataRow(7, ">", 3, true)]
        [DataRow(1, "==", 1, true)]
        [DataRow(4, "!=", 99, true)]
        [DataRow(1, "==", 2, false)]
        public void IsIfStatementTrueTest(int left, string condition, int right, bool expectedOutcome)
        {

            Form1 form = new Form1();
            bool realOutcome = form.IsIfStatementTrue(left, condition, right);
            Assert.AreEqual(expectedOutcome, realOutcome);
        }

        [TestMethod()]//Test negative values given if variable does not exist
        public void NoVariableGetDefinedVariableValueAndPositionTest()
        {
            Form1 form = new Form1();
            Tuple<int,int> valueAndPosition = form.GetDefinedVariableValueAndPosition("somethingRandom");
            Assert.AreEqual(-10000, valueAndPosition.Item1);
            Assert.AreEqual(-1, valueAndPosition.Item2);
        }

        [TestMethod()]// Test returns 0 if no if and endif statements
        public void NoIfBlockGetIfStartAndEndLineTest()
        {
            TextBox commands = new TextBox {Text = "jgdkfk" };
            Form1 form = new Form1();
            int startLine = form.GetIfStartLineNumber(commands);
            int endLine = form.GetEndifLineNumber(commands);
            Assert.AreEqual(0, startLine);
            Assert.AreEqual(0, endLine);
        }

        [TestMethod()]// Test returns null if factory shape does not exist
        public void NoShapeFoundForFactoryGetShapeTest()
        {
            Factory factory = new Factory
            {
                showMessage = false
            };
            Shape shape = factory.GetShape("hexagon");
            Assert.IsNull(shape);
        }

    }
}
