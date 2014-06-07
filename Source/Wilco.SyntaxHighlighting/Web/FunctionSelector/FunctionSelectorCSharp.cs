using System;
using System.Collections.Generic;
using System.Text;

namespace Wilco.Web.SyntaxHighlighting
{
    internal class FunctionSelectorCSharp : FunctionSelector
    {

        private bool _inFunctionStarted = false;
        private int _inFunctionCount = 0;
        private bool _inFunctionEnd = false;
        private bool _inFunctionFirstBracket = false;

        public override bool IsLineBelongingToRequiredFunction(string line)
        {
            if (FunctionName.Equals(string.Empty))
                return true;

            if (_inFunctionEnd)
                return false;

            //Find first occurrence
            if (!_inFunctionStarted)
            {
                if (line.IndexOf(FunctionName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (line.IndexOf("public", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         line.IndexOf("protected", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         line.IndexOf("internal", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         line.IndexOf("private", StringComparison.OrdinalIgnoreCase) >= 0 ||
                         line.TrimStart().StartsWith("<span")
                     )
                    {
                        _inFunctionStarted = true;
                        _inFunctionFirstBracket = false;
                    }
                }
            }

            if (_inFunctionStarted)
            {
                int open = countOcurrence(line, '{');
                int close = countOcurrence(line, '}');
                _inFunctionCount = _inFunctionCount + (open - close);

                if (!_inFunctionFirstBracket && open > 0)
                    _inFunctionFirstBracket = true;
            }

            if (_inFunctionFirstBracket) //if first bracket found analyze
                _inFunctionEnd = (_inFunctionStarted && _inFunctionCount == 0);

            return _inFunctionStarted;
        }
    }
}
