// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Html : ILanguage
{
    public string Id => LanguageId.Html;

    public string Name => "HTML";

    public string CssClassName => "html";

    public string FirstLinePattern
    {
        get
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public IList<LanguageRule> Rules => new List<LanguageRule>
    {
        new(
            @"\<![ \r\n\t]*(--([^\-]|[\r\n]|-[^\-])*--[ \r\n\t]*)\>",
            new Dictionary<int, string>
            {
                { 0, ScopeName.HtmlComment },
            }),
        new(
            @"(?i)(<!)(DOCTYPE)(?:\s+([a-zA-Z0-9]+))*(?:\s+(""[^""]*?""))*(>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlTagDelimiter },
                { 2, ScopeName.HtmlElementName },
                { 3, ScopeName.HtmlAttributeName },
                { 4, ScopeName.HtmlAttributeValue },
                { 5, ScopeName.HtmlTagDelimiter },
            }),
        new(
            @"(?xis)(<)
                                          (script)
                                          (?:
                                             [\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*([^\s\n""']+?)
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*(""[^\n]+?"")
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*('[^\n]+?')
                                            |[\s\n]+([a-z0-9-_]+) )*
                                          [\s\n]*
                                          (>)
                                          (.*?)
                                          (</)(script)(>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlTagDelimiter },
                { 2, ScopeName.HtmlElementName },
                { 3, ScopeName.HtmlAttributeName },
                { 4, ScopeName.HtmlOperator },
                { 5, ScopeName.HtmlAttributeValue },
                { 6, ScopeName.HtmlAttributeName },
                { 7, ScopeName.HtmlOperator },
                { 8, ScopeName.HtmlAttributeValue },
                { 9, ScopeName.HtmlAttributeName },
                { 10, ScopeName.HtmlOperator },
                { 11, ScopeName.HtmlAttributeValue },
                { 12, ScopeName.HtmlAttributeName },
                { 13, ScopeName.HtmlTagDelimiter },
                { 14, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.JavaScript) },
                { 15, ScopeName.HtmlTagDelimiter },
                { 16, ScopeName.HtmlElementName },
                { 17, ScopeName.HtmlTagDelimiter },
            }),
        new(
            @"(?xis)(</?)
                                          (?: ([a-z][a-z0-9-]*)(:) )*
                                          ([a-z][a-z0-9-_]*)
                                          (?:
                                             [\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*([^\s\n""']+?)
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*(""[^\n]+?"")
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*('[^\n]+?')
                                            |[\s\n]+([a-z0-9-_]+) )*
                                          [\s\n]*
                                          (/?>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlTagDelimiter },
                { 2, ScopeName.HtmlElementName },
                { 3, ScopeName.HtmlTagDelimiter },
                { 4, ScopeName.HtmlElementName },
                { 5, ScopeName.HtmlAttributeName },
                { 6, ScopeName.HtmlOperator },
                { 7, ScopeName.HtmlAttributeValue },
                { 8, ScopeName.HtmlAttributeName },
                { 9, ScopeName.HtmlOperator },
                { 10, ScopeName.HtmlAttributeValue },
                { 11, ScopeName.HtmlAttributeName },
                { 12, ScopeName.HtmlOperator },
                { 13, ScopeName.HtmlAttributeValue },
                { 14, ScopeName.HtmlAttributeName },
                { 15, ScopeName.HtmlTagDelimiter },
            }),
        new(
            @"(?i)&\#?[a-z0-9]+?;",
            new Dictionary<int, string>
            {
                { 0, ScopeName.HtmlEntity },
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "htm":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}