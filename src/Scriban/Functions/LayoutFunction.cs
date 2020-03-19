using System;
using System.Collections.Generic;
using Scriban.Parsing;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Scriban.Functions
{
    /// <summary>
    /// The layout function available through the keyword 'layout'
    /// </summary>
    public sealed class LayoutFunction : IScriptCustomFunction
    {
        public LayoutFunction()
        {
        }

        public object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            if (arguments.Count == 0)
            {
                throw new ScriptRuntimeException(callerContext.Span, "The 'layout' function requires a filename argument.");
            }

            //TODO: Look at Include funcxtion

            context.PushOutput();
            object result = null;
            try
            {
                context.EnterRecursive(callerContext);
                result = "[[LAYOUT]]";
                context.ExitRecursive(callerContext);
            }
            finally
            {
                context.PopOutput();
            }

            return result;
        }
    }
}