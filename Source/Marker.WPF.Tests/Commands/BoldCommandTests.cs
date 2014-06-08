using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marker.WPF.Tests.Commands
{
    using Marker.WPF.EditorCommands;

    using ScintillaNET;

    [TestClass]
    public class BoldCommandTests
    {
        private BoldCommand boldCommand;

        private Scintilla scintilla;

        [TestInitialize]
        public void Initialize()
        {
            this.boldCommand = new BoldCommand();
            this.scintilla = new Scintilla();
        }

        [TestMethod]
        public void BoldSingleWord()
        {
            var word = "bold";
            var expected = "**bold**";
            var startPosition = 3;
            var endPosition = startPosition;
            var expectedStartPosition = startPosition + 2;
            var expectedEndPosition = expectedStartPosition;

            this.ConfigureCommandTest(word, startPosition, endPosition, expected, expectedStartPosition, expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleSelectedWord()
        {
            var word = "bold";
            var expected = "**bold**";
            var startPosition = 0;
            var endPosition = 4;
            var expectedStartPosition = startPosition;
            var expectedEndPosition = endPosition + 4;

            this.ConfigureCommandTest(word, startPosition, endPosition, expected, expectedStartPosition, expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleWordInSentence()
        {
            var word = "test bold this";
            var expected = "test **bold** this";
            var startPosition = 7;
            var endPosition = startPosition;
            var expectedStartPosition = startPosition + 2;
            var expectedEndPosition = expectedStartPosition;
            this.ConfigureCommandTest(word, startPosition, endPosition, expected, expectedStartPosition, expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleSelectedWordInSentence()
        {
            var word = "test bold this";
            var expected = "test **bold** this";
            var startPosition = 5;
            var endPosition = 9;
            var expectedStartPosition = startPosition;
            var expectedEndPosition = endPosition + 4;

            this.ConfigureCommandTest(word, startPosition, endPosition, expected, expectedStartPosition, expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleWordAfterEndOfLine()
        {
            var word = "this \r\nbold";
            var expected = "this \r\n**bold**";
            var startPosition = 10;
            var endPosition = startPosition;
            var expectedStartPosition = startPosition + 2;
            var expectedEndPosition = expectedStartPosition;

            this.ConfigureCommandTest(word, startPosition, endPosition, expected, expectedStartPosition, expectedEndPosition);
        }

        private void ConfigureCommandTest(
            string word,
            int startPosition,
            int endPosition,
            string expected,
            int expectedStartPosition,
            int expectedEndPosition)
        {
            this.scintilla.Text = word;
            this.scintilla.Selection.Start = startPosition;
            this.scintilla.Selection.End = endPosition;
            this.AssertBoldCommand(expected, expectedStartPosition, expectedEndPosition);
        }

        private void AssertBoldCommand(string expected, int expectedStartPosition, int expectedEndPosition = -1)
        {
            if (expectedEndPosition == -1)
            {
                expectedEndPosition = expectedStartPosition;
            }

            this.boldCommand.Execute(this.scintilla);

            Assert.AreEqual(expected, this.scintilla.Text);
            Assert.AreEqual(expectedStartPosition, this.scintilla.Selection.Start);
            Assert.AreEqual(expectedEndPosition, this.scintilla.Selection.End);
        }
    }
}
