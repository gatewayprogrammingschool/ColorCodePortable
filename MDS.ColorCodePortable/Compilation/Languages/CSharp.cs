// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class CSharp : ILanguage
{
    public string Id => LanguageId.CSharp;

    public string Name => "C#";

    public string CssClassName => "csharp";

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
            @"(///)(?:\s*?(<[/a-zA-Z0-9\s""=]+>))*([^\r\n]*)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.XmlDocTag },
                { 2, ScopeName.XmlDocTag },
                { 3, ScopeName.XmlDocComment },
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
            @"(?s)@""(?:""""|.)*?""(?!"")",
            new Dictionary<int, string>
            {
                { 0, ScopeName.StringCSharpVerbatim },
            }),
        new(
            @"(?s)(""[^\n]*?(?<!\\)"")",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
            }),
        new(
            @"\[(assembly|module|type|return|param|method|field|property|event):[^\]""]*(""[^\n]*?(?<!\\)"")?[^\]]*\]",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.String },
            }),
        new(
            @"^\s*(\#define|\#elif|\#else|\#endif|\#endregion|\#error|\#if|\#line|\#pragma|\#region|\#undef|\#warning).*?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.PreprocessorKeyword },
            }),
        new(
            @"\b(abstract|as|ascending|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|goto|group|if|implicit|in|int|into|interface|internal|is|join|let|lock|long|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|where|while|yield)\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "cs":
            case "c#":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}
