using Scriban.Runtime;
using Scriban.Syntax;

namespace Scriban.Functions
{
    /// <summary>
    /// The exclude function is available through the function 'exclude' in scriban and does nothing.
    /// </summary>
    public sealed class ExcludeFunction : IScriptCustomFunction
    {
        public ExcludeFunction()
        {
        }

        public object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            return "";
        }
    }
}