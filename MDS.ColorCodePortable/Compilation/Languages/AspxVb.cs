// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class AspxVb : ILanguage
{
    public string Id => LanguageId.AspxVb;

    public string Name => "ASPX (VB.NET)";

    public string CssClassName => "aspx-vb";

    public string FirstLinePattern => @"(?xims)<%@\s*?(?:page|control|master|webhandler|servicehost|webservice).*?language=""vb"".*?%>";

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
                { 2, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.VbDotNet) },
                { 3, ScopeName.HtmlServerSideScript },
            }),
        new(RuleFormats.ServerScript, RuleCaptures.VbDotNetScript),
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
                                             [\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*(?:'(<%\#)(.+?)(%>)')
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*(?:""(<%\#)(.+?)(%>)"")
                                            |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*([^\s\n""']+?)
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
                { 7, ScopeName.HtmlServerSideScript },
                { 8, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.VbDotNet) },
                { 9, ScopeName.HtmlServerSideScript },
                { 10, ScopeName.HtmlAttributeName },
                { 11, ScopeName.HtmlOperator },
                { 12, ScopeName.HtmlServerSideScript },
                { 13, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.VbDotNet) },
                { 14, ScopeName.HtmlServerSideScript },
                { 15, ScopeName.HtmlAttributeName },
                { 16, ScopeName.HtmlOperator },
                { 17, ScopeName.HtmlAttributeValue },
                { 18, ScopeName.HtmlAttributeName },
                { 19, ScopeName.HtmlOperator },
                { 20, ScopeName.HtmlAttributeValue },
                { 21, ScopeName.HtmlAttributeName },
                { 22, ScopeName.HtmlOperator },
                { 23, ScopeName.HtmlAttributeValue },
                { 24, ScopeName.HtmlAttributeName },
                { 25, ScopeName.HtmlTagDelimiter },
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
            case "aspx-vb":
            case "aspx (vb.net)":
            case "aspx(vb.net)":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}