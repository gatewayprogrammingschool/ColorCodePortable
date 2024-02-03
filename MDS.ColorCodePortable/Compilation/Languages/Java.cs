// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Java : ILanguage
{
    public string Id => LanguageId.Java;

    public string Name => "Java";

    public string CssClassName => "java";

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
            @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/",
            new Dictionary<int, string>
            {
                { 0, ScopeName.Comment },
            }),
        new(
            @"(//.*?)\r?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Comment },
            }),
        new(
            @"'[^\n]*?(?<!\\)'",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
            }),
        new(
            @"(?s)(""[^\n]*?(?<!\\)"")",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
            }),
        new(
            @"\b(abstract|assert|boolean|break|byte|case|catch|char|class|const|continue|default|do|double|else|enum|extends|false|final|finally|float|for|goto|if|implements|import|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|strictfp|super|switch|synchronized|this|throw|throws|transient|true|try|void|volatile|while)\b",
            new Dictionary<int, string>
            {
                {0, ScopeName.Keyword},
            }),
    };

    public bool HasAlias(string lang)
        => false;

    public override string ToString()
        => Name;
}
