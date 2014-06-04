namespace Marker.WPF
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
        private static Regex regexBackslashEscape = new Regex(@"^(\\)(.)");
        private static Regex regexItalic = new Regex(@"^(\*|_)(?!(\*|_|\t|\040))(.+?)(\*|_)");
        private static Regex regexBold = new Regex(@"^(\*\*|__)(?!(\*|_|\t|\040))(.+?)(\*\*|__)");
        private static Regex regexItalicBold = new Regex(@"^(\*\*\*|___)(?!(\*|_|\t|\040))(.+?)(\*\*\*|___)");
        private static Regex regexImage = new Regex(@"^!\[.+?]\(.+?\)");
        private static Regex regexLink = new Regex(@"^\[.+?\]\(.+?\)");
        private static Regex regexHeadingA = new Regex(@"^(.+?\r\n)(===+|---+)\s*$");
        private static Regex regexHeadingB = new Regex(@"^(#+)(.+?[^#$&])(#*)\s*$");
        private static Regex regexList = new Regex(@"^(\*|\+|\-|\d+\.)(\t|\040)(?=.*)");
        private static Regex regexBlockQuote = new Regex(@"^\s*>(?=.*)");
        private static Regex regexBlockCode = new Regex(@"^(````)(.+)");
        private static Regex regexHorizontalRule = new Regex(@"^((\*\*\*+)|(\r?\n?)(---+))\s*$");
        private static Regex regexComment = new Regex(@"^((<!--)|(-->)|(</)|(<)|(/>)|(>))");
        #endregion

        public static void Init(Scintilla scintillaControl)
        {
            scintillaControl.Indentation.SmartIndentType = SmartIndent.None;
            scintillaControl.ConfigurationManager.Language = string.Empty;
            scintillaControl.Lexing.LexerName = "container";
            scintillaControl.Lexing.Lexer = Lexer.Container;
            scintillaControl.Font = new Font("Consolas", 12);
            scintillaControl.Indentation.TabWidth = 3;
            

            scintillaControl.Styles[StyleDefault].ForeColor = Color.White;
            scintillaControl.Styles[StyleBraceLight].ForeColor = Color.White;
            scintillaControl.Styles[StyleBraceBad].ForeColor = Color.FromArgb(250, 40, 115);

            scintillaControl.Styles[StyleLineNumber].BackColor = Color.FromArgb(40, 40, 40);
            scintillaControl.Styles[StyleLineNumber].ForeColor = Color.White;
            scintillaControl.Styles[StyleLineNumber].IsVisible = true;
            scintillaControl.Styles[StyleLineNumber].Size = 12;
            scintillaControl.Styles[StyleLineNumber].Font = new Font("Consolas", 12);


            scintillaControl.Styles[StyleItalic].Italic = true;
            scintillaControl.Styles[StyleItalic].ForeColor = Color.FromArgb(230, 220, 116);
            scintillaControl.Styles[StyleBold].ForeColor = Color.FromArgb(230, 220, 116);
            scintillaControl.Styles[StyleBold].Bold = true;
            
            scintillaControl.Styles[StyleItalicBold].Italic = true;
            scintillaControl.Styles[StyleItalicBold].Bold = true;
            scintillaControl.Styles[StyleImage].ForeColor = Color.FromArgb(250, 40, 115);
            scintillaControl.Styles[StyleLink].ForeColor = Color.FromArgb(250, 40, 115);
            scintillaControl.Styles[StyleHeading].ForeColor = Color.FromArgb(100, 220, 240);
            scintillaControl.Styles[StyleHeading].Bold = true;
            
            scintillaControl.Styles[StyleList].ForeColor = Color.Gray;
            scintillaControl.Styles[StyleBlockQuote].BackColor = Color.FromArgb(230, 220, 116);
            scintillaControl.Styles[StyleBlockCode].ForeColor = Color.FromArgb(230, 220, 116);
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
            while (currentPosition < textToStyle.Length)
            {
                string textToEndOfLine = textToStyle.Substring(currentPosition, textToStyle.Length - currentPosition);
                int matchLenght = 1;
                int styleId = StyleDefault;

                if (regexBackslashEscape.IsMatch(textToEndOfLine))
                {
                    matchLenght = regexBackslashEscape.Match(textToEndOfLine).Length;
                }
                else if (regexItalic.IsMatch(textToEndOfLine))
                {
                    Match italicMatch = regexItalic.Match(textToEndOfLine);

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
                else if (regexBold.IsMatch(textToEndOfLine))
                {
                    Match boldMatch = regexBold.Match(textToEndOfLine);

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
                else if (regexItalicBold.IsMatch(textToEndOfLine))
                {
                    Match italicBoldMatch = regexItalicBold.Match(textToEndOfLine);

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
                else if (regexImage.IsMatch(textToEndOfLine))
                {
                    matchLenght = regexImage.Match(textToEndOfLine).Length;
                    styleId = StyleImage;
                }
                else if (regexLink.IsMatch(textToEndOfLine))
                {
                    matchLenght = regexLink.Match(textToEndOfLine).Length;
                    styleId = StyleLink;
                }
                else if (regexHeadingA.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    Match headingMatch = regexHeadingA.Match(textToEndOfLine);

                    // style to StyleDefault the content
                    SetStyle(scintillaControl, lineStartPosition + currentPosition, headingMatch.Groups[1].Length, StyleHeading);
                    currentPosition += headingMatch.Groups[1].Length;

                    // style to StyleHeading the heading underline
                    matchLenght = headingMatch.Groups[2].Length;
                    styleId = StyleHeading;
                }
                else if (regexHeadingB.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    Match headingMatch = regexHeadingB.Match(textToEndOfLine);

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
                else if (regexList.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = regexList.Match(textToEndOfLine).Length;
                    styleId = StyleList;
                }
                else if (regexBlockQuote.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = regexBlockQuote.Match(textToEndOfLine).Length;
                    styleId = StyleBlockQuote;
                }
                else if (regexBlockCode.IsMatch(textToEndOfLine))
                {
                    matchLenght = regexBlockCode.Match(textToEndOfLine).Length;
                    styleId = StyleBlockCode;
                }
                else if (regexHorizontalRule.IsMatch(textToEndOfLine) && currentPosition == 0)
                {
                    matchLenght = regexHorizontalRule.Match(textToEndOfLine).Length;
                    styleId = StyleHorizontalRule;
                }
                else if (regexComment.IsMatch(textToEndOfLine))
                {
                    matchLenght = regexComment.Match(textToEndOfLine).Length;
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
