using System;
using System.Collections.Generic;
using System.Text;

namespace Wilco.Web.SyntaxHighlighting
{
    internal class FunctionSelector
    {
        public virtual bool IsLineBelongingToRequiredFunction(string line)
        {
            return true;
        }

        protected string _FunctionName = string.Empty;
        public string FunctionName
        {
            get{ return _FunctionName; }
            set{ _FunctionName = value; }
        }

        protected int countOcurrence(string line, char c)
        {
            int count = 0;
            for (int i = 0; i < line.Length; i++)
                if (line[i].Equals(c))
                    count++;
            return count;
        }
        
    }
}
