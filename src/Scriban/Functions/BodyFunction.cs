using System;
using System.Collections.Generic;
using Scriban.Parsing;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Scriban.Functions
{
    /// <summary>
    /// The body function available through the function 'body' - only applicable to layouts
    /// </summary>
    public sealed class BodyFunction : IScriptCustomFunction
    {
        public BodyFunction()
        {
        }

        public object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {

            context.PushOutput();
            object result = null;
            try
            {
                context.EnterRecursive(callerContext);
                result = "[[BODY]]";
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