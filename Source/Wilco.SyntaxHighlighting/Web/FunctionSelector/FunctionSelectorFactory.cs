using System;
using System.Collections.Generic;
using System.Text;


namespace Wilco.Web.SyntaxHighlighting
{
    internal class FunctionSelectorFactory
    {
        public static FunctionSelector Get(string language)
        {
            FunctionSelector retVal = null;

            switch (language)
            {
                case "C#": retVal = new FunctionSelectorCSharp(); break;
                case "VisualBasic": retVal = new FunctionSelectorVB(); break;
                default: retVal = new FunctionSelector(); break;
            }

            return retVal;
        }
    }

}