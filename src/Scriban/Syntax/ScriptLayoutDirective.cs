// AJW
using System;
using System.Collections;
using System.Collections.Generic;
using Scriban.Helpers;
using Scriban.Runtime;

namespace Scriban.Syntax
{
    [ScriptSyntax("layout directive expression", "<target_expression> <argument[0]> ... <argument[n]>")]
    public class ScriptLayoutDirective : ScriptFunctionCall
    {
        

        public override object Evaluate(TemplateContext context)
        {
            // Invoke evaluate on the target, but don't automatically call the function as if it was a parameterless call.
            var targetFunction = context.Evaluate(Target, true);

            // Throw an exception if the target function is null
            if (targetFunction == null)
            {
                throw new ScriptRuntimeException(Target.Span, $"The target `{Target}` function is null");
            }

            return Call(context, this, targetFunction, context.AllowPipeArguments, Arguments);
        }


        public override void Write(TemplateRewriterContext context)
        {
            context.Write(Target);
            foreach (var scriptExpression in Arguments)
            {
                context.ExpectSpace();
                context.Write(scriptExpression);
            }
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