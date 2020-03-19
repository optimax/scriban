using Scriban.Runtime;
using Scriban.Syntax;

namespace Scriban.Functions
{
    /// <summary>
    /// The exclude function is available through the keyword 'exclude' and does nothing
    /// (it allows for an easy inactivation of an include function).
    /// </summary>
    public sealed class ExcludeFunction : IScriptCustomFunction
    {
        public object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            return "";
        }
    }
}