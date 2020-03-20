// AJW

using System.Collections.Generic;
using System.Linq;

namespace Scriban.Syntax
{
    [ScriptSyntax("'render' statement", "render \"<section-name>\"")]
    public class ScriptRenderStatement : ScriptStatement
    {
        public List<ScriptExpression> Arguments { get; private set; }

        public string Name => Arguments.Any() ? Arguments[0].ToString() : "";

        public ScriptRenderStatement()
        {
            Arguments = new List<ScriptExpression>();
        }


        public override object Evaluate(TemplateContext context)
        {
            
            if (context.CurrentPage.Sections.ContainsKey(Name))
            {
                var section = context.CurrentPage.Sections[Name];
                var renderedSection = section.Body.Evaluate(context);
                return renderedSection;
            }
            return ""; 
        }


        public override void Write(TemplateRewriterContext context)
        {
        }


        public override string ToString()
        {
            var name = Arguments.Count > 0 ? Arguments[0].ToString() : "???";
            return $"{{render \"{name}\"}}";
        }
    }
}