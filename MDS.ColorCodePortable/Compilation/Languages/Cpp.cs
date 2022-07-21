// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages
{
    public class Cpp : ILanguage
    {
        public string Id => LanguageId.Cpp;

        public string Name => "C++";

        public string CssClassName => "cplusplus";

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
                @"(?s)(""[^\n]*?(?<!\\)"")",
                new Dictionary<int, string>
                {
                    { 0, ScopeName.String },
                }),
            new(
                @"\b(abstract|array|auto|bool|break|case|catch|char|ref class|class|const|const_cast|continue|default|delegate|delete|deprecated|dllexport|dllimport|do|double|dynamic_cast|each|else|enum|event|explicit|export|extern|false|float|for|friend|friend_as|gcnew|generic|goto|if|in|initonly|inline|int|interface|literal|long|mutable|naked|namespace|new|noinline|noreturn|nothrow|novtable|nullptr|operator|private|property|protected|public|register|reinterpret_cast|return|safecast|sealed|selectany|short|signed|sizeof|static|static_cast|ref struct|struct|switch|template|this|thread|throw|true|try|typedef|typeid|typename|union|unsigned|using|uuid|value|virtual|void|volatile|wchar_t|while)\b",
                new Dictionary<int, string>
                {
                    {0, ScopeName.Keyword},
                }),
        };

        public bool HasAlias(string lang)
        {
            switch (lang.ToLower())
            {
                case "c++":
                case "c":
                    return true;

                default:
                    return false;
            }
        }

        public override string ToString()
            => Name;
    }
}
