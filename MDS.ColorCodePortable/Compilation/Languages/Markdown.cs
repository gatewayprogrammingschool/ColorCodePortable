// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages
{
    public class Markdown : ILanguage
    {
        public string Id => LanguageId.Markdown;

        public string Name => "Markdown";

        public string CssClassName => "markdown";

        public string FirstLinePattern
        {
            get
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        private string link(string content = @"([^\]\n]+)")
            => @"\!?\[" + content + @"\][ \t]*(\([^\)\n]*\)|\[[^\]\n]*\])";

        public IList<LanguageRule> Rules => new List<LanguageRule>
        {
            // Heading
            new(
                @"^(\#.*)\r?|^.*\r?\n([-]+|[=]+)\r?$",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.MarkdownHeader },
                }),


            // code block
            new(
                @"^([ ]{4}(?![ ])(?:.|\r?\n[ ]{4})*)|^(```+[ \t]*\w*)((?:[ \t\r\n]|.)*?)(^```+)[ \t]*\r?$",
                new Dictionary<int, string>
                {
                    { 1, ScopeName.MarkdownCode },
                    { 2, ScopeName.XmlDocTag },
                    { 3, ScopeName.MarkdownCode },
                    { 4, ScopeName.XmlDocTag },
                }),

            // Line
            new(
                @"^\s*((-\s*){3}|(\*\s*){3}|(=\s*){3})[ \t\-\*=]*\r?$",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.MarkdownHeader },
                }),


            // List
            new(
                @"^[ \t]*([\*\+\-]|\d+\.)",
                new Dictionary<int, string>
                {
                    { 1, ScopeName.MarkdownListItem },
                }),

            // escape
            new(
                @"\\[\\`\*_{}\[\]\(\)\#\+\-\.\!]",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.StringEscape },
                }),

            // link
            new(
                link(link()) + "|" + link(),  // support nested links (mostly for images)
                new Dictionary<int, string>
                {
                    { 1, ScopeName.MarkdownBold },
                    { 2, ScopeName.XmlDocTag },
                    { 3, ScopeName.XmlDocTag },
                    { 4, ScopeName.MarkdownBold },
                    { 5, ScopeName.XmlDocTag },
                }),
            new(
                @"^[ ]{0,3}\[[^\]\n]+\]:(.|\r|\n[ \t]+(?![\r\n]))*$",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.XmlDocTag },    // nice gray
                }),

            // bold
            new(
                @"\*(?!\*)([^\*\n]|\*\w)+?\*(?!\w)|\*\*([^\*\n]|\*(?!\*))+?\*\*",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.MarkdownBold },
                }),

            // emphasized
            new(
                @"_([^_\n]|_\w)+?_(?!\w)|__([^_\n]|_(?=[\w_]))+?__(?!\w)",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.MarkdownEmph },
                }),

            // inline code
            new(
                @"`[^`\n]+?`|``([^`\n]|`(?!`))+?``",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.MarkdownCode },
                }),

            // strings
            new(
                @"""[^""\n]+?""|'[\w\-_]+'",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.String },
                }),

            // html tag
            new(
                @"</?\w.*?>",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.HtmlTagDelimiter },
                }),

            // html entity
            new(
                @"\&\#?\w+?;",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.HtmlEntity },
                }),
        };

        public bool HasAlias(string lang)
        {
            switch (lang.ToLower())
            {
                case "md":
                case "markdown":
                    return true;

                default:
                    return false;
            }
        }

        public override string ToString()
            => Name;
    }
}