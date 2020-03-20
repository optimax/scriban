// AJW

using System;
using System.Collections.Generic;
using Scriban.Runtime;

namespace Scriban.Syntax
{
    [ScriptSyntax("'layout' directive", "layout <layout-template-name>")]
    public class ScriptLayoutStatement : ScriptStatement
    {
        public List<ScriptExpression> Arguments { get; private set; }

        public ScriptLayoutStatement()
        {
            Arguments = new List<ScriptExpression>();
        }


        public override object Evaluate(TemplateContext context)
        {
            var templateName = context.ToString(this.Span, Arguments[0]);

            throw new ScriptRuntimeException(this.Span, $"There can only be one 'layout' statement in a file, found 'layout \"{templateName}\"'");
        }


        public override void Write(TemplateRewriterContext context)
        {

        }


        public Template ApplyLayout(TemplateContext context)
        {
            if (Arguments.Count == 0)
                throw new ScriptRuntimeException(this.Span,
                    "The 'layout' statement requires a template-name argument.");

            //var layoutFileName = Arguments[0];
            var templateName = context.ToString(this.Span, Arguments[0]);
            if (string.IsNullOrEmpty(templateName))
                throw new ScriptRuntimeException(this.Span, $"The 'layout' template name cannot be null or empty");


            var templateLoader = context.TemplateLoader;
            if (templateLoader == null)
            {
                throw new ScriptRuntimeException(this.Span,
                    $"Unable to include <{templateName}>. No TemplateLoader registered in TemplateContext.TemplateLoader");
            }

            string templatePath;

            try
            {
                templatePath = templateLoader.GetPath(context, this.Span, templateName);
            }
            catch (Exception ex) when (!(ex is ScriptRuntimeException))
            {
                throw new ScriptRuntimeException(this.Span,
                    $"Unexpected exception while getting the path for the include name `{templateName}`", ex);
            }

            // If template path is empty (probably because template doesn't exist), throw an exception
            if (templatePath == null)
            {
                throw new ScriptRuntimeException(this.Span, $"Include template path is null for `{templateName}");
            }

            //// Compute a new parameters for the include
            //var newParameters = new ScriptArray(Arguments.Count - 1);
            //for (int i = 1; i < Arguments.Count; i++)
            //{
            //    newParameters[i] = Arguments[i];
            //}

            //context.SetValue(ScriptVariable.Arguments, newParameters, true);

            Template template;

            if (!context.CachedTemplates.TryGetValue(templatePath, out template))
            {

                string templateText;
                try
                {
                    templateText = templateLoader.Load(context, this.Span, templatePath);
                }
                catch (Exception ex) when (!(ex is ScriptRuntimeException))
                {
                    throw new ScriptRuntimeException(this.Span,
                        $"Unexpected exception while loading the include `{templateName}` from path `{templatePath}`",
                        ex);
                }

                if (templateText == null)
                {
                    throw new ScriptRuntimeException(this.Span,
                        $"The result of including `{templateName}->{templatePath}` cannot be null");
                }

                // Clone parser options
                var parserOptions = context.TemplateLoaderParserOptions;
                var lexerOptions = context.TemplateLoaderLexerOptions;
                template = Template.Parse(templateText, templatePath, parserOptions, lexerOptions);

                // If the template has any errors, throw an exception
                if (template.HasErrors)
                {
                    throw new ScriptParserRuntimeException(this.Span,
                        $"Error while parsing template `{templateName}` from `{templatePath}`", template.Messages);
                }

                context.CachedTemplates.Add(templatePath, template);
            }

            // Make sure that we cannot recursively include a template

            context.PushOutput();
            Template result = null;
            try
            {
                context.EnterRecursive(this);
                //AJW
                template.ResolvePageLayout(context);
                result = template;
                //`
                context.ExitRecursive(this);
            }
            finally
            {
                context.PopOutput();
            }

            return result;
        }
    }
}