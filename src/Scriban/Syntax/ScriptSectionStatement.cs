// AJW

using System.Collections.Generic;
using System.Linq;

namespace Scriban.Syntax
{
    [ScriptSyntax("'section' statement", "section \"section-name\"\n...\nend")]
    public class ScriptSectionStatement : ScriptBlockStatement
    {
        public List<ScriptExpression> Arguments { get; private set; }

        public ScriptBlockStatement Body { get; set; }

        public string Name => Arguments.Any() ? Arguments[0].ToString() : "";

        public ScriptSectionStatement()
        {
            Arguments = new List<ScriptExpression>();
        }

        public override object Evaluate(TemplateContext context)
        {
            throw new ScriptRuntimeException(this.Span, $"Section cannot be defined in a layout");
        }

        public override void Write(TemplateRewriterContext context)
        {
        }

        public override string ToString()
        {
            var name = Arguments.Count > 0 ? Arguments[0].ToString() : "???";
            return $"section \"{name}\"";
        }
    }
}
