// AJW

namespace Scriban.Syntax
{
    [ScriptSyntax("'body' directive", "body")]
    public class ScriptBodyStatement : ScriptStatement
    {

        public override object Evaluate(TemplateContext context)
        {
            throw new ScriptRuntimeException(this.Span, $"The 'body' directive can only appear in a template used as a layout");
        }

        public override void Write(TemplateRewriterContext context)
        {
        }

        public override string ToString()
        {
            return "{BODY}";
        }
    }
}