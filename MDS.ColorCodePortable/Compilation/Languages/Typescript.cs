// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages
{
    public class Typescript : ILanguage
    {
        public string Id => LanguageId.TypeScript;

        public string Name => "Typescript";

        public string CssClassName => "typescript";

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
            new(
                @"\b(abstract|any|bool|boolean|break|byte|case|catch|char|class|const|constructor|continue|debugger|declare|default|delete|do|double|else|enum|export|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|module|native|new|number|null|package|private|protected|public|return|short|static|string|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|volatile|while|with)\b",
                new Dictionary<int, string>
                {
                    { 1, ScopeName.Keyword },
                }),
        };

        public bool HasAlias(string lang)
        {
            switch (lang.ToLower())
            {
                case "ts":
                    return true;

                default:
                    return false;
            }
        }

        public override string ToString()
            => Name;
    }
}