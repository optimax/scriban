using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Scriban.Syntax
{
    [ScriptSyntax("'markdown' statement", "markdown ... end")]
    public class ScriptMarkdownStatement : ScriptBlockStatement
    {
        public List<ScriptExpression> Arguments { get; private set; }

        public ScriptBlockStatement Body { get; set; }

        public ScriptMarkdownStatement()
        {
            Arguments = new List<ScriptExpression>();
        }

        public override object Evaluate(TemplateContext context)
        {
            if (Arguments.Any())
                return EvaluateMarkdownFunction(context);
            Debug.Assert(Body != null);
            return EvaluateMarkdownBlock(context);
        }


        public object EvaluateMarkdownFunction(TemplateContext context)
        {
            return "markdown()"; //TODO
        }


        public object EvaluateMarkdownBlock(TemplateContext context)
        {
            var toOutput = context.EnableOutput;
            context.EnableOutput = false;
            var block = (string)Body.Evaluate(context);
            context.EnableOutput = toOutput;

            var html = context.Markdown?.Render(block) ?? "";
            return html;
        }


        public override void Write(TemplateRewriterContext context)
        {
        }


        public override string ToString()
        {
            return $"{{markdown-statement}}";
        }
    }


    public static class EnumerableExtensions
    {

        public static string ToText(this List<ScriptStatement> statements)
        {
            var sb = new StringBuilder();
            var firstTime = true;
            if (statements != null)
                foreach (var statement in statements)
                {
                    var stat = statement as ScriptRawStatement;
                    if (stat == null)
                        continue;
                    if (!firstTime)
                        sb.Append("\n");
                    sb.Append($"{stat.Text}");
                    firstTime = false;
                }
            return sb.ToString();
        }

    }


}
