namespace BlueMoon.UI.Tests.Commands
{
    using BlueMoon.UI.Commands.EditorCommands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ScintillaNET;

    [TestClass]
    public class BoldCommandTests
    {
        #region Fields

        private BoldCommand boldCommand;

        private Scintilla scintilla;

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void BoldSingleSelectedWord()
        {
            string word = "bold";
            string expected = "**bold**";
            int startPosition = 0;
            int endPosition = 4;
            int expectedStartPosition = startPosition;
            int expectedEndPosition = endPosition + 4;

            this.ConfigureCommandTest(
                word,
                startPosition,
                endPosition,
                expected,
                expectedStartPosition,
                expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleSelectedWordInSentence()
        {
            string word = "test bold this";
            string expected = "test **bold** this";
            int startPosition = 5;
            int endPosition = 9;
            int expectedStartPosition = startPosition;
            int expectedEndPosition = endPosition + 4;

            this.ConfigureCommandTest(
                word,
                startPosition,
                endPosition,
                expected,
                expectedStartPosition,
                expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleWord()
        {
            const string Word = "bold";
            const string Expected = "**bold**";
            const int StartPosition = 3;
            const int EndPosition = StartPosition;
            const int ExpectedStartPosition = StartPosition + 2;
            const int ExpectedEndPosition = ExpectedStartPosition;

            this.ConfigureCommandTest(
                Word,
                StartPosition,
                EndPosition,
                Expected,
                ExpectedStartPosition,
                ExpectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleWordAfterEndOfLine()
        {
            string word = "this \r\nbold";
            string expected = "this \r\n**bold**";
            int startPosition = 10;
            int endPosition = startPosition;
            int expectedStartPosition = startPosition + 2;
            int expectedEndPosition = expectedStartPosition;

            this.ConfigureCommandTest(
                word,
                startPosition,
                endPosition,
                expected,
                expectedStartPosition,
                expectedEndPosition);
        }

        [TestMethod]
        public void BoldSingleWordInSentence()
        {
            string word = "test bold this";
            string expected = "test **bold** this";
            int startPosition = 7;
            int endPosition = startPosition;
            int expectedStartPosition = startPosition + 2;
            int expectedEndPosition = expectedStartPosition;
            this.ConfigureCommandTest(
                word,
                startPosition,
                endPosition,
                expected,
                expectedStartPosition,
                expectedEndPosition);
        }

        [TestInitialize]
        public void Initialize()
        {
            this.boldCommand = new BoldCommand();
            this.scintilla = new Scintilla();
        }

        #endregion

        #region Methods

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

        #endregion
    }
}