// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Ashx : ILanguage
{
    public string Id => LanguageId.Ashx;

    public string Name => "ASHX";

    public string CssClassName => "ashx";

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
            @"(<%)(--.*?--)(%>)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.HtmlServerSideScript },
                { 2, ScopeName.HtmlComment },
                { 3, ScopeName.HtmlServerSideScript },
            }),
        new(
            @"(?is)(?<=<%@.+?language=""c\#"".*?%>)(.*)",
            new Dictionary<int, string>
            {
                { 1, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.CSharp) },
            }),
        new(
            @"(?is)(?<=<%@.+?language=""vb"".*?%>)(.*)",
            new Dictionary<int, string>
            {
                { 1, string.Format("{0}{1}", ScopeName.LanguagePrefix, LanguageId.VbDotNet) },
            }),
        new(
            @"(<%)(@)(?:\s+([a-zA-Z0-9]+))*(?:\s+([a-zA-Z0-9]+)(=)(""[^\n]*?""))*\s*?(%>)",
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
    };

    public bool HasAlias(string lang)
        => false;

    public override string ToString()
        => Name;
}