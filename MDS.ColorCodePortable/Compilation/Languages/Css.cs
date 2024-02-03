// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class Css : ILanguage
{
    public string Id => LanguageId.Css;

    public string Name => "CSS";

    public string CssClassName => "css";

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
            @"(?msi)(?:(\s*/\*.*?\*/)|(([a-z0-9#. \[\]=\"":_-]+)\s*(?:,\s*|{))+(?:(\s*/\*.*?\*/)|(?:\s*([a-z0-9 -]+\s*):\s*([a-z0-9#,<>\?%. \(\)\\\/\*\{\}:'\""!_=-]+);?))*\s*})",
            new Dictionary<int, string>
            {
                { 3, ScopeName.CssSelector },
                { 5, ScopeName.CssPropertyName },
                { 6, ScopeName.CssPropertyValue },
                { 4, ScopeName.Comment },
                { 1, ScopeName.Comment },
            }),
    };

    public bool HasAlias(string lang)
        => false;

    public override string ToString()
        => Name;
}
