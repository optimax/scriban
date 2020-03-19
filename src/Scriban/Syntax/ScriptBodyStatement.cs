// AJW

namespace Scriban.Syntax
{
    [ScriptSyntax("body directive expression", "<target_expression> <argument[0]> ... <argument[n]>")]
    public class ScriptBodyStatement : ScriptStatement
    {

        public override object Evaluate(TemplateContext context)
        {
            return "";
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