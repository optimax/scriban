// AJW
using System;
using System.Collections;
using System.Collections.Generic;
using Scriban.Helpers;
using Scriban.Runtime;

namespace Scriban.Syntax
{
    [ScriptSyntax("body directive expression", "<target_expression> <argument[0]> ... <argument[n]>")]
    public class ScriptBodyDirective : ScriptFunctionCall
    {

        public override object Evaluate(TemplateContext context)
        {
            return "";
        }


        public override void Write(TemplateRewriterContext context)
        {
        }

        public override bool CanHaveLeadingTrivia()
        {
            return false;
        }

        public override string ToString()
        {
            var args = StringHelper.Join(" ", Arguments);
            return $"{Target} {args}";
        }

    }
}