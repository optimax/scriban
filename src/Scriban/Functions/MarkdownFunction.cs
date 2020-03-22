// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.
using System;
using System.Collections.Generic;
using Scriban.Parsing;
using Scriban.Runtime;
using Scriban.Syntax;

namespace Scriban.Functions
{
    /// <summary>
    /// The include function available through the function 'include' in scriban.
    /// </summary>
    public class MarkdownFunction : IncludeFunction
    {
        public override object Invoke(TemplateContext context, ScriptNode callerContext, ScriptArray arguments, ScriptBlockStatement blockStatement)
        {
            if (arguments.Count == 0)
                throw new ScriptRuntimeException(callerContext.Span, "Expecting the name of the include for the 'markdown' function");
            var md = base.Invoke(context, callerContext, arguments, blockStatement) as string;
            var html = context.Markdown?.Render(md) ?? "";
            return html;
        }
    }
}