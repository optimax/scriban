// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.

using Scriban.Parsing;

namespace Scriban.Syntax
{
    public class ScriptPage : ScriptNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptPage"/> class.
        /// </summary>
        public ScriptPage()
        {
        }

        /// <summary>
        /// Gets or sets the front matter. May be <c>null</c> if script is not parsed using  <see cref="ScriptMode.FrontMatterOnly"/> or <see cref="ScriptMode.FrontMatterAndContent"/>. See remarks.
        /// </summary>
        /// <remarks>
        /// Note that this code block is not executed when evaluating this page. It has to be evaluated separately (usually before evaluating the page).
        /// </remarks>
        public ScriptBlockStatement FrontMatter { get; set; }

        public ScriptBlockStatement Body { get; set; }

        public Template Layout { get; set; } //-AJW

        public override object Evaluate(TemplateContext context)
        {
            if (Layout != null)
            {
                var newBody = WrapBodyWithLayout(Body, Layout);
                return context.Evaluate(newBody);
            }
            return context.Evaluate(Body);
        }


        /// <summary>
        /// !!!! The recursive layout application
        /// </summary>
        private ScriptBlockStatement WrapBodyWithLayout(ScriptBlockStatement body, Template layout)
        {
            if (layout == null)
                return body;
            var newBody = ReplaceBodyDirective(layout.Page.Body, body);
            newBody = WrapBodyWithLayout(newBody, layout.Page.Layout);
            return newBody;
        }



        private ScriptBlockStatement ReplaceBodyDirective(ScriptBlockStatement layoutBody, ScriptBlockStatement pageBody)
        {
            var newBody = new ScriptBlockStatement();
            foreach (var node in layoutBody.Statements)
            {
                if (node is ScriptBodyStatement)
                    newBody.Statements.AddRange(pageBody.Statements);
                else
                    newBody.Statements.Add(node);
            }
            return newBody;
        }


        public override void Write(TemplateRewriterContext context)
        {
            context.Write(Body);
        }
    }
}