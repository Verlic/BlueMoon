using System;
using System.Collections.Generic;
using System.Text;


namespace Wilco.Web.SyntaxHighlighting
{
    internal class FunctionSelectorVB : FunctionSelector
    {

        private bool _inFunctionEnd = false;
        private bool _inFunctionStarted = false;
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
                    if (line.IndexOf("Sub", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _inFunctionStarted = true;
                    }
                }
            }

            if (_inFunctionStarted)
            {
                int end_i = line.IndexOf("End", StringComparison.OrdinalIgnoreCase);
                if (end_i >= 0)
                {
                    int sub_i = line.IndexOf("Sub", StringComparison.OrdinalIgnoreCase);
                    if (sub_i >= 0 && end_i < sub_i)
                        _inFunctionEnd = true;
                }

            }
            return _inFunctionStarted;
        }

    }
}