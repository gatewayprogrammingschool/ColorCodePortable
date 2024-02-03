// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class FSharp : ILanguage
{
    public string Id => LanguageId.FSharp;

    public string Name => "F#";

    public string CssClassName => "FSharp";

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
            @"\(\*([^*]|[\r\n]|(\*+([^*)]|[\r\n])))*\*+\)",
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
            @"(?s)""""""(?:""""|.)*?""""""(?!"")",
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
            @"^\s*(\#else|\#endif|\#if).*?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.PreprocessorKeyword },
            }),
        new(
            @"\b(let!|use!|do!|yield!|return!)\s",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),
        new(
            @"\b(abstract|and|as|assert|base|begin|class|default|delegate|do|done|downcast|downto|elif|else|end|exception|extern|false|finally|for|fun|function|global|if|in|inherit|inline|interface|internal|lazy|let|match|member|module|mutable|namespace|new|null|of|open|or|override|private|public|rec|return|sig|static|struct|then|to|true|try|type|upcast|use|val|void|when|while|with|yield|atomic|break|checked|component|const|constraint|constructor|continue|eager|fixed|fori|functor|include|measure|method|mixin|object|parallel|params|process|protected|pure|recursive|sealed|tailcall|trait|virtual|volatile)\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),
        new(
            @"(\w|\s)(->)(\w|\s)",
            new Dictionary<int, string>
            {
                { 2, ScopeName.Keyword },
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "fs":
            case "f#":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}