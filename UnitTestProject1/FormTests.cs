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
                "var y = 12 \r\nif y == 12 then \r\nsquare y \r\nendif \r\nvar y - 2 \nvar x = 3";

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
    }
}
