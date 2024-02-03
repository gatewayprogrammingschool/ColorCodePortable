// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Koka : ILanguage
{
    public string Id => LanguageId.Koka;

    public string Name => "Koka";

    public string CssClassName => "koka";

    public string FirstLinePattern
    {
        get
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    private const string incomment = @"([^*/]|/(?!\*)|\*(?!/))*";

    private const string plainKeywords = @"infix|infixr|infixl|type|cotype|rectype|struct|alias|interface|instance|external|fun|function|val|var|con|module|import|as|public|private|abstract|yield";
    private const string controlKeywords = @"if|then|else|elif|match|return";
    private const string typeKeywords = @"forall|exists|some|with";
    private const string pseudoKeywords = @"include|inline|cs|js|file";
    private const string opkeywords = @"[=\.\|]|\->|\:=";

    private const string intype = @"\b(" + typeKeywords + @")\b|(?:[a-z]\w*/)*[a-z]\w+\b|(?:[a-z]\w*/)*[A-Z]\w*\b|([a-z][0-9]*\b|_\w*\b)|\->|[\s\|]";
    private const string toptype = "(?:" + intype + "|::)";
    private const string nestedtype = @"(?:([a-z]\w*)\s*[:]|" + intype + ")";

    private const string symbol = @"[$%&\*\+@!\\\^~=\.:\-\?\|<>/]";
    private const string symbols = @"(?:" + symbol + @")+";

    private const string escape = @"\\(?:[nrt\\""']|x[\da-fA-F]{2}|u[\da-fA-F]{4}|U[\da-fA-F]{6})";

    public IList<LanguageRule> Rules => new List<LanguageRule> {
        // Nested block comments. note: does not match on unclosed comments
        new(
            // Handle nested block comments using named balanced groups
            @"/\*" + incomment +
            @"(" +
            @"((?<comment>/\*)" + incomment + ")+" +
            @"((?<-comment>\*/)" + incomment + ")+" +
            @")*" +
            @"(\*+/)",
            new Dictionary<int, string>
            {
                { 0, ScopeName.Comment },
            }),

        // Line comments
        new(
            @"(//.*?)\r?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Comment },
            }),

        // operator keywords
        new(
            @"(?<!" + symbol + ")(" + opkeywords + @")(?!" + symbol + @")",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),

        // Types
        new(
            // Type highlighting using named balanced groups to balance parenthesized sub types
            // 'toptype' captures two groups: type keyword and type variables
            // each 'nestedtype' captures three groups: parameter names, type keywords and type variables
            @"(?:" + @"\b(type|struct|cotype|rectype)\b|"
                   + @"::?(?!" + symbol + ")|"
                   + @"\b(alias)\s+[a-z]\w+\s*(?:<[^>]*>\s*)?(=)" + ")"
                   + toptype + "*" +
                   @"(?:" +
                   @"(?:(?<type>[\(\[<])(?:" + nestedtype + @"|[,]" + @")*)+" +
                   @"(?:(?<-type>[\)\]>])(?:" + nestedtype + @"|(?(type)[,])" + @")*)+" +
                   @")*" +
                   @"", //(?=(?:[,\)\{\}\]<>]|(" + keywords +")\b))",
            new Dictionary<int,string> {
                { 0, ScopeName.Type },

                { 1, ScopeName.Keyword },   // type struct etc
                { 2, ScopeName.Keyword },   // alias
                { 3, ScopeName.Keyword },   //  =

                { 4, ScopeName.Keyword},
                { 5, ScopeName.TypeVariable },

                { 6, ScopeName.PlainText },
                { 7, ScopeName.Keyword },
                { 8, ScopeName.TypeVariable },

                { 9, ScopeName.PlainText },
                { 10, ScopeName.Keyword },
                { 11, ScopeName.TypeVariable },
            }),

        // module and imports
        new(
            @"\b(module)\s+(interface\s+)?((?:[a-z]\w*/)*[a-z]\w*)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.Keyword },
                { 3, ScopeName.NameSpace },
            }),

        new(
            @"\b(import)\s+((?:[a-z]\w*/)*[a-z]\w*)\s*(?:(=)\s*(qualified\s+)?((?:[a-z]\w*/)*[a-z]\w*))?",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.NameSpace },
                { 3, ScopeName.Keyword },
                { 4, ScopeName.Keyword },
                { 5, ScopeName.NameSpace },
            }),

        // keywords
        new(
            @"\b(" + plainKeywords + "|" + typeKeywords + @")\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),
        new(
            @"\b(" + controlKeywords + @")\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.ControlKeyword },
            }),
        new(
            @"\b(" + pseudoKeywords + @")\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.PseudoKeyword },
            }),

        // Names
        new(
            @"([a-z]\w*/)*([a-z]\w*|\(" + symbols + @"\))",
            new Dictionary<int, string>
            {
                { 1, ScopeName.NameSpace },
            }),
        new(
            @"([a-z]\w*/)*([A-Z]\w*)",
            new Dictionary<int, string>
            {
                { 1, ScopeName.NameSpace },
                { 2, ScopeName.Constructor },
            }),

        // Operators and punctuation
        new(
            symbols,
            new Dictionary<int, string>
            {
                { 0, ScopeName.Operator },
            }),
        new(
            @"[{}\(\)\[\];,]",
            new Dictionary<int, string>
            {
                { 0, ScopeName.Delimiter },
            }),

        // Literals
        new(
            @"0[xX][\da-fA-F]+|\d+(\.\d+([eE][\-+]?\d+)?)?",
            new Dictionary<int, string>
            {
                { 0, ScopeName.Number },
            }),

        new(
            @"(?s)'(?:[^\t\n\\']+|(" + escape + @")|\\)*'",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
                { 1, ScopeName.StringEscape },
            }),
        new(
            @"(?s)@""(?:("""")|[^""]+)*""(?!"")",
            new Dictionary<int, string>
            {
                { 0, ScopeName.StringCSharpVerbatim },
                { 1, ScopeName.StringEscape },
            }),
        new(
            @"(?s)""(?:[^\t\n\\""]+|(" + escape + @")|\\)*""",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
                { 1, ScopeName.StringEscape },
            }),
        new(
            @"^\s*(\#error|\#line|\#pragma|\#warning|\#!/usr/bin/env\s+koka|\#).*?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.PreprocessorKeyword },
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "kk":
            case "kki":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}
