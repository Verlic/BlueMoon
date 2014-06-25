namespace BlueMoon.UI.Components
{
    using System.Drawing;
    using System.Text.RegularExpressions;

    using ScintillaNET;

    public class MarkdownLexer
    {
        #region Style Ids
        private const int StyleDefault = 1;
        private const int StyleItalic = 2;
        private const int StyleBold = 3;
        private const int StyleItalicBold = 4;
        private const int StyleImage = 5;
        private const int StyleLink = 6;
        private const int StyleHeading = 7;
        private const int StyleList = 8;
        private const int StyleBlockQuote = 9;
        private const int StyleBlockCode = 10;
        private const int StyleHorizontalRule = 11;
        private const int StyleComment = 12;
        private const int StyleLineNumber = 33;
        private const int StyleBraceLight = 34;
        private const int StyleBraceBad = 35;
        #endregion

        #region Style Regex
        private static readonly Regex RegexBackslashEscape = new Regex(@"^(\\)(.)");
        private static readonly Regex RegexItalic = new Regex(@"^(\*|_)(?!(\*|_|\t|\040))(.+?)(\*|_)");
        private static readonly Regex RegexBold = new Regex(@"^(\*\*|__)(?!(\*|_|\t|\040))(.+?)(\*\*|__)");
        private static readonly Regex RegexItalicBold = new Regex(@"^(\*\*\*|___)(?!(\*|_|\t|\040))(.+?)(\*\*\*|___)");
        private static readonly Regex RegexImage = new Regex(@"^!\[.+?]\(.+?\)");
        private static readonly Regex RegexLink = new Regex(@"^\[.+?\]\(.+?\)");
        private static readonly Regex RegexHeadingA = new Regex(@"^(.+?\r\n)(===+|---+)\s*$");
        private static readonly Regex RegexHeadingB = new Regex(@"^(#+)(.+?[^#$&])(#*)\s*$");
        private static readonly Regex RegexList = new Regex(@"^(\*|\+|\-|\d+\.)(\t|\040)(?=.*)");
        private static readonly Regex RegexBlockQuote = new Regex(@"^\s*>(?=.*)");
        private static readonly Regex RegexBlockCode = new Regex(@"^(````)(.+)");
        private static readonly Regex RegexHorizontalRule = new Regex(@"^((\*\*\*+)|(\r?\n?)(---+))\s*$");
        private static readonly Regex RegexComment = new Regex(@"^((<!--)|(-->)|(</)|(<)|(/>)|(>))");
        #endregion

        public static void Init(Scintilla scintillaControl)
        {
            scintillaControl.Indentation.SmartIndentType = SmartIndent.None;
            scintillaControl.ConfigurationManager.Language = string.Empty;
            scintillaControl.Lexing.LexerName = "container";
            scintillaControl.Lexing.Lexer = Lexer.Container;
            scintillaControl.Font = new Font("Segoe UI", 12);
            scintillaControl.Indentation.TabWidth = 3;
            

            scintillaControl.Styles[StyleDefault].ForeColor = Color.Black;
            scintillaControl.Styles[StyleBraceLight].ForeColor = Color.Black;
            scintillaControl.Styles[StyleBraceBad].ForeColor = Color.FromArgb(250, 40, 115);

            scintillaControl.Styles[StyleLineNumber].BackColor = Color.FromArgb(40, 40, 40);
            scintillaControl.Styles[StyleLineNumber].ForeColor = Color.White;
            scintillaControl.Styles[StyleLineNumber].IsVisible = true;
            scintillaControl.Styles[StyleLineNumber].Size = 9;
            scintillaControl.Styles[StyleLineNumber].Font = new Font("Consolas", 9);


            scintillaControl.Styles[StyleItalic].Italic = true;
            scintillaControl.Styles[StyleBold].Bold = true;
            
            scintillaControl.Styles[StyleItalicBold].Italic = true;
            scintillaControl.Styles[StyleItalicBold].Bold = true;
            scintillaControl.Styles[StyleImage].ForeColor = Color.FromArgb(240, 125, 50);
            scintillaControl.Styles[StyleLink].ForeColor = Color.FromArgb(0, 0, 255);
            scintillaControl.Styles[StyleHeading].ForeColor = Color.FromArgb(30, 80, 120);
            scintillaControl.Styles[StyleHeading].Bold = true;
            
            scintillaControl.Styles[StyleList].ForeColor = Color.Gray;
            scintillaControl.Styles[StyleBlockQuote].BackColor = Color.FromArgb(45, 145, 175);
            scintillaControl.Styles[StyleBlockCode].BackColor = Color.FromArgb(45, 200, 105);
            scintillaControl.Styles[StyleBlockCode].ForeColor = Color.White;
            scintillaControl.Styles[StyleHorizontalRule].ForeColor = Color.FromArgb(250, 40, 115);
            scintillaControl.Styles[StyleComment].ForeColor = Color.FromArgb(117, 113, 94);
        }

        public static void StyleLine(Scintilla scintillaControl, int charPosition)
        {
            Line currentLine = scintillaControl.Lines.FromPosition(charPosition);
            int lineStartPosition = currentLine.StartPosition;
            string lineText = currentLine.Text;

            // Select from previous line if current line is an underline (for H1)
            if (currentLine.Number > 0)
            {
                if (currentLine.Previous.Text != "\r\n" && Regex.IsMatch(lineText, @"^(==+|--+)\s*$"))
                {
                    lineText = currentLine.Previous.Text + lineText;
                    lineStartPosition = currentLine.Previous.StartPosition;
                }
            }

            // Select next line too if its an underline (for H1)
            if (currentLine.Next != null)
            {
                if (Regex.IsMatch(currentLine.Next.Text, @"^(==+|--+)\s*$"))
                {
                    lineText = lineText + currentLine.Next.Text;
                }
            }

            StyleText(scintillaControl, lineText, lineStartPosition);
        }

        public static void StyleText(Scintilla scintillaControl, string textToStyle, int lineStartPosition)
        {
            int currentPosition = 0;
            while (currentPosition <= textToStyle.Length)
            {
                string textToEndOfLine = textToStyle.Substring(currentPosition, textToStyle.Length - currentPosition);
                int matchLenght = 1;
                int styleId = StyleDefault;

                if (RegexBackslashEscape.IsMatch(textToEndOfLine))
                {
                    matchLenght = RegexBackslashEscape.Match(textToEndOfLine).Length;
                }
                else if (RegexItalic.IsMatch(textToEndOfLine))
                {
                    Match italicMatch = RegexItalic.Match(textToEndOfLine);

                    // style to StyleDefault the opening character
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, italicMatch.Groups[1].Length, StyleDefault);
                    currentPosition += italicMatch.Groups[1].Length;

                    // style to StyleItalic the content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, italicMatch.Groups[3].Length, StyleItalic);
                    currentPosition += italicMatch.Groups[3].Length;

                    // style to StyleDefault the closing character
                    matchLenght = italicMatch.Groups[4].Length;
                    styleId = StyleDefault;
                }
                else if (RegexBold.IsMatch(textToEndOfLine))
                {
                    Match boldMatch = RegexBold.Match(textToEndOfLine);

                    // style to StyleDefault the opening character
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, boldMatch.Groups[1].Length, StyleBold);
                    currentPosition += boldMatch.Groups[1].Length;

                    // style to StyleBold the content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, boldMatch.Groups[3].Length, StyleBold);
                    currentPosition += boldMatch.Groups[3].Length;

                    // style to StyleDefault the closing character
                    matchLenght = boldMatch.Groups[4].Length;
                    styleId = StyleBold;
                }
                else if (RegexItalicBold.IsMatch(textToEndOfLine))
                {
                    Match italicBoldMatch = RegexItalicBold.Match(textToEndOfLine);

                    // style to StyleDefault the opening character
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, italicBoldMatch.Groups[1].Length, StyleDefault);
                    currentPosition += italicBoldMatch.Groups[1].Length;

                    // style to StyleItalicBold the content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, italicBoldMatch.Groups[3].Length, StyleItalicBold);
                    currentPosition += italicBoldMatch.Groups[3].Length;

                    // style to StyleDefault the closing character
                    matchLenght = italicBoldMatch.Groups[4].Length;
                    styleId = StyleDefault;
                }
                else if (RegexImage.IsMatch(textToEndOfLine))
                {
                    matchLenght = RegexImage.Match(textToEndOfLine).Length;
                    styleId = StyleImage;
                }
                else if (RegexLink.IsMatch(textToEndOfLine))
                {
                    matchLenght = RegexLink.Match(textToEndOfLine).Length;
                    styleId = StyleLink;
                }
                else if (RegexHeadingA.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    Match headingMatch = RegexHeadingA.Match(textToEndOfLine);

                    // style to StyleDefault the content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, headingMatch.Groups[1].Length, StyleHeading);
                    currentPosition += headingMatch.Groups[1].Length;

                    // style to StyleHeading the heading underline
                    matchLenght = headingMatch.Groups[2].Length;
                    styleId = StyleHeading;
                }
                else if (RegexHeadingB.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    Match headingMatch = RegexHeadingB.Match(textToEndOfLine);

                    // style to StyleHeading the first set of #
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, headingMatch.Groups[1].Length, StyleHeading);
                    currentPosition += headingMatch.Groups[1].Length;

                    // style to StyleDefault the header content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, headingMatch.Groups[2].Length, StyleHeading);
                    currentPosition += headingMatch.Groups[2].Length;

                    // style to StyleHeading the last set of # (if any)
                    matchLenght = headingMatch.Groups[3].Length;
                    styleId = StyleHeading;
                }
                else if (RegexList.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = RegexList.Match(textToEndOfLine).Length;
                    styleId = StyleList;
                }
                else if (RegexBlockQuote.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = RegexBlockQuote.Match(textToEndOfLine).Length;
                    styleId = StyleBlockQuote;
                }
                else if (RegexBlockCode.IsMatch(textToEndOfLine))
                {
                    matchLenght = RegexBlockCode.Match(textToEndOfLine).Length;
                    styleId = StyleBlockCode;
                }
                else if (RegexHorizontalRule.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = RegexHorizontalRule.Match(textToEndOfLine).Length;
                    styleId = StyleHorizontalRule;
                }
                else if (RegexComment.IsMatch(textToEndOfLine))
                {
                    matchLenght = RegexComment.Match(textToEndOfLine).Length;
                    styleId = StyleComment;
                }

                SetStyle(scintillaControl, lineStartPosition + currentPosition, matchLenght, styleId);
                currentPosition += matchLenght;
            }
        }

        public static void SetStyle(Scintilla scintillaControl, int startPosition, int styleLength, int styleId)
        {
            ((INativeScintilla)scintillaControl).StartStyling(startPosition, 0x1F);
            ((INativeScintilla)scintillaControl).SetStyling(styleLength, styleId);
        }

        public static void UnstyleAll(Scintilla scintillaControl)
        {
            foreach (Line line in scintillaControl.Lines)
            {
                SetStyle(scintillaControl, line.StartPosition, line.Length, StyleDefault);
            }
        }

        public static void StyleAll(Scintilla scintillaControl)
        {
            foreach (Line line in scintillaControl.Lines)
            {
                StyleLine(scintillaControl, line.StartPosition);
            }
        }

        public static void EnableHighlighting(bool enableHighlight, Scintilla scintillaControl)
        {
            if (enableHighlight)
            {
                StyleAll(scintillaControl);
            }
            else
            {
                UnstyleAll(scintillaControl);
            }
        }
    }
}
