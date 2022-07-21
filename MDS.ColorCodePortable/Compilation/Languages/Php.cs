// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages
{
    public class Php : ILanguage
    {
        public string Id => LanguageId.Php;

        public string Name => "PHP";

        public string CssClassName => "php";

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
                @"(#.*?)\r?$",
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
                @"""[^\n]*?(?<!\\)""",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.String },
                }),
            new(
                // from http://us.php.net/manual/en/reserved.keywords.php
                @"\b(abstract|and|array|as|break|case|catch|cfunction|class|clone|const|continue|declare|default|do|else|elseif|enddeclare|endfor|endforeach|endif|endswitch|endwhile|exception|extends|fclose|file|final|for|foreach|function|global|goto|if|implements|interface|instanceof|mysqli_fetch_object|namespace|new|old_function|or|php_user_filter|private|protected|public|static|switch|throw|try|use|var|while|xor|__CLASS__|__DIR__|__FILE__|__FUNCTION__|__LINE__|__METHOD__|__NAMESPACE__|die|echo|empty|exit|eval|include|include_once|isset|list|require|require_once|return|print|unset)\b",
                new Dictionary<int, string>
                {
                    { 1, ScopeName.Keyword },
                }),
        };

        public bool HasAlias(string lang)
        {
            switch (lang.ToLower())
            {
                case "php3":
                case "php4":
                case "php5":
                    return true;

                default:
                    return false;
            }
        }

        public override string ToString()
            => Name;
    }
}
