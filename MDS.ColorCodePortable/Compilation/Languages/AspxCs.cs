// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class AspxCs : ILanguage
{
    public string Id => LanguageId.AspxCs;

    public string Name => "ASPX (C#)";

    public string CssClassName => "aspx-cs";

    public string FirstLinePattern => @"(?xims)<%@\s*?(?:page|control|master|servicehost|webservice).*?(?:language=""c\#""|src="".+?.cs"").*?%>";

    public IList<LanguageRule> Rules => new List<LanguageRule>
    {
        new(
            @"(?s)(<%)(--.*?--)(%>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlServerSideScript },
                { 2, ScopeName.HtmlComment },
                { 3, ScopeName.HtmlServerSideScript },
            }),
        new(
            @"(?s)<!--.*-->",
            new Dictionary<int, string>
            {
                { 0, ScopeName.HtmlComment },
            }),
        new(
            @"(?i)(<%)(@)(?:\s+([a-z0-9]+))*(?:\s+([a-z0-9]+)\s*(=)\s*(""[^\n]*?""))*\s*?(%>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlServerSideScript },
                { 2, ScopeName.HtmlTagDelimiter },
                { 3, ScopeName.HtmlElementName },
                { 4, ScopeName.HtmlAttributeName },
                { 5, ScopeName.HtmlOperator },
                { 6, ScopeName.HtmlAttributeValue },
                { 7, ScopeName.HtmlServerSideScript },
            }),
        new(
            @"(?s)(?:(<%=|<%)(?!=|@|--))(.*?)(%>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlServerSideScript },
                { 2, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.CSharp) },
                { 3, ScopeName.HtmlServerSideScript },
            }),
        new(RuleFormats.ServerScript, RuleCaptures.CSharpScript),
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
        new(RuleFormats.JavaScript, RuleCaptures.JavaScript),
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
            case "aspx-cs":
            case "aspx (cs)":
            case "aspx(cs)":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}