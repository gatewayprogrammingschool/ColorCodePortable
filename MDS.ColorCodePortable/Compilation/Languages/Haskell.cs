﻿// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Haskell : ILanguage
{
    public string Id => LanguageId.Haskell;

    public string Name => "Haskell";

    public string CssClassName => "haskell";

    public string FirstLinePattern
    {
        get
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    private const string nonnestComment = @"((?:--.*\r?\n|{-(?:[^-]|-(?!})|[\r\n])*-}))";

    private const string incomment = @"([^-{}]|{(?!-)|-(?!})|(?<!-)})*";
    private const string keywords = @"case|class|data|default|deriving|do|else|foreign|if|import|in|infix|infixl|infixr|instance|let|module|newtype|of|then|type|where";
    private const string opKeywords = @"\.\.|:|::|=|\\|\||<-|->|@|~|=>";

    private const string vsymbol = @"[\!\#\$%\&\⋆\+\./<=>\?@\\\^\-~\|]";
    private const string symbol = @"(?:" + vsymbol + "|:)";

    private const string varop = vsymbol + "(?:" + symbol + @")*";
    private const string conop = ":(?:" + symbol + @")*";

    private const string conid = @"(?:[A-Z][\w']*|\(" + conop + @"\))";
    private const string varid = @"(?:[a-z][\w']*|\(" + varop + @"\))";

    private const string qconid = @"((?:[A-Z][\w']*\.)*)" + conid;
    private const string qvarid = @"((?:[A-Z][\w']*\.)*)" + varid;
    private const string qconidop = @"((?:[A-Z][\w']*\.)*)(?:" + conid + "|" + conop + ")";

    private const string intype = @"(\bforall\b|=>)|" + qconidop + @"|(?!deriving|where|data|type|newtype|instance|class)([a-z][\w']*)|\->|[ \t!\#]|\r?\n[ \t]+(?=[\(\)\[\]]|->|=>|[A-Z])";
    private const string toptype = "(?:" + intype + "|::)";
    private const string nestedtype = @"(?:" + intype + ")";

    private const string datatype = "(?:" + intype + @"|[,]|\r?\n[ \t]+|::|(?<!" + symbol + @"|^)([=\|])\s*(" + conid + ")|" + nonnestComment + ")";

    private const string inexports = @"(?:[\[\],\s]|(" + conid + ")|" + varid
                                      + "|" + nonnestComment
                                      + @"|\((?:[,\.\s]|(" + conid + ")|" + varid + @")*\)"
                                      + ")*";

    public IList<LanguageRule> Rules => new List<LanguageRule> {
        // Nested block comments: note does not match no unclosed block comments.
        new(
            // Handle nested block comments using named balanced groups
            @"{-+" + incomment +
            @"(?>" +
            @"(?>(?<comment>{-)" + incomment + ")+" +
            @"(?>(?<-comment>-})" + incomment + ")+" +
            @")*" +
            @"(-+})",
            new Dictionary<int, string>
            {
                { 0, ScopeName.Comment },
            }),


        // Line comments
        new(
            @"(--.*?)\r?$",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Comment },
            }),

        // Types
        new(
            // Type highlighting using named balanced groups to balance parenthesized sub types
            // 'toptype' and 'nestedtype' capture three groups: type keywords, namespaces, and type variables
            @"(?:" + @"\b(class|instance|deriving)\b"
                   + @"|::(?!" + symbol + ")"
                   + @"|\b(type)\s+" + toptype + @"*\s*(=)"
                   + @"|\b(data|newtype)\s+" + toptype + @"*\s*(=)\s*(" + conid + ")"
                   + @"|\s+(\|)\s*(" + conid + ")"
                   + ")" + toptype + "*" +
                   @"(?:" +
                   @"(?:(?<type>[\(\[<])(?:" + nestedtype + @"|[,]" + @")*)+" +
                   @"(?:(?<-type>[\)\]>])(?:" + nestedtype + @"|(?(type)[,])" + @")*)+" +
                   @")*",
            new Dictionary<int,string> {
                { 0, ScopeName.Type },

                { 1, ScopeName.Keyword },   // class instance etc

                { 2, ScopeName.Keyword},        // type
                { 3, ScopeName.Keyword},
                { 4, ScopeName.NameSpace },
                { 5, ScopeName.TypeVariable },
                { 6, ScopeName.Keyword},

                { 7, ScopeName.Keyword},        // data , newtype
                { 8, ScopeName.Keyword},
                { 9, ScopeName.NameSpace },
                { 10, ScopeName.TypeVariable },
                { 11, ScopeName.Keyword },       // = conid
                { 12, ScopeName.Constructor },

                { 13, ScopeName.Keyword },       // | conid
                { 14, ScopeName.Constructor },

                { 15, ScopeName.Keyword},
                { 16, ScopeName.NameSpace },
                { 17, ScopeName.TypeVariable },

                { 18, ScopeName.Keyword },
                { 19, ScopeName.NameSpace },
                { 20, ScopeName.TypeVariable },

                { 21, ScopeName.Keyword },
                { 22, ScopeName.NameSpace },
                { 23, ScopeName.TypeVariable },
            }),


        // Special sequences
        new(
            @"\b(module)\s+(" + qconid + @")(?:\s*\(" + inexports + @"\))?",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.NameSpace },
                { 4, ScopeName.Type },
                { 5, ScopeName.Comment },
                { 6, ScopeName.Constructor },
            }),
        new(
            @"\b(module|as)\s+(" + qconid + ")",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.NameSpace },
            }),

        new(
            @"\b(import)\s+(qualified\s+)?(" + qconid + @")\s*"
            + @"(?:\(" + inexports + @"\))?"
            + @"(?:(hiding)(?:\s*\(" + inexports + @"\)))?",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
                { 2, ScopeName.Keyword },
                { 3, ScopeName.NameSpace },
                { 5, ScopeName.Type },
                { 6, ScopeName.Comment },
                { 7, ScopeName.Constructor },
                { 8, ScopeName.Keyword},
                { 9, ScopeName.Type },
                { 10, ScopeName.Comment },
                { 11, ScopeName.Constructor },
            }),

        // Keywords
        new(
            @"\b(" + keywords + @")\b",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),
        new(
            @"(?<!" + symbol +")(" + opKeywords + ")(?!" + symbol + ")",
            new Dictionary<int, string>
            {
                { 1, ScopeName.Keyword },
            }),

        // Names
        new(
            qvarid,
            new Dictionary<int, string>
            {
                { 1, ScopeName.NameSpace },
            }),
        new(
            qconid,
            new Dictionary<int, string>
            {
                { 0, ScopeName.Constructor },
                { 1, ScopeName.NameSpace },
            }),

        // Operators and punctuation
        new(
            varop,
            new Dictionary<int, string>
            {
                { 0, ScopeName.Operator },
            }),
        new(
            conop,
            new Dictionary<int, string>
            {
                { 0, ScopeName.Constructor },
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
            @"'[^\n]*?'",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
            }),
        new(
            @"""[^\n]*?""",
            new Dictionary<int, string>
            {
                { 0, ScopeName.String },
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "hs":
                return true;

            default:
                return false;
        }
    }

    public override string ToString()
        => Name;
}