// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation.Languages;

public class PowerShell : ILanguage
{
    public string Id => LanguageId.PowerShell;

    public string Name => "PowerShell";

    public string CssClassName => "powershell";

    public string FirstLinePattern
    {
#pragma warning disable CS8603 // Possible null reference return.
        get
        {
            return null;
        }
#pragma warning restore CS8603 // Possible null reference return.
    }

    public IList<LanguageRule> Rules => new List<LanguageRule>
    {
        new(
            @"(?s)(<\#.*?\#>)",
            new Dictionary<int, string>
            {
                {1, ScopeName.Comment},
            }),
        new(
            @"(\#.*?)\r?$",
            new Dictionary<int, string>
            {
                {1, ScopeName.Comment},
            }),
        new(
            @"'[^\n]*?(?<!\\)'",
            new Dictionary<int, string>
            {
                {0, ScopeName.String},
            }),
        new(
            @"(?s)@"".*?""@",
            new Dictionary<int, string>
            {
                {0, ScopeName.StringCSharpVerbatim},
            }),
        new(
            @"(?s)(""[^\n]*?(?<!`)"")",
            new Dictionary<int, string>
            {
                {0, ScopeName.String},
            }),
        new(
            @"\$(?:[\d\w\-]+(?::[\d\w\-]+)?|\$|\?|\^)",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellVariable},
            }),
        new(
            @"\${[^}]+}",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellVariable},
            }),
        new(
            @"\b(begin|break|catch|continue|data|do|dynamicparam|elseif|else|end|exit|filter|finally|foreach|for|from|function|if|in|param|process|return|switch|throw|trap|try|until|while)\b",
            new Dictionary<int, string>
            {
                {1, ScopeName.Keyword},
            }),
        new(
            @"-(?:c|i)?(?:eq|ne|gt|ge|lt|le|notlike|like|notmatch|match|notcontains|contains|replace)",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellOperator},
            }
        ),
        new(
            @"-(?:band|and|as|join|not|bxor|xor|bor|or|isnot|is|split)",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellOperator},
            }
        ),
        new(
            @"(?:\+=|-=|\*=|/=|%=|=|\+\+|--|\+|-|\*|/|%)",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellOperator},
            }
        ),
        new(
            @"(?:\>\>|2\>&1|\>|2\>\>|2\>)",
            new Dictionary<int, string>
            {
                {0, ScopeName.PowerShellOperator},
            }
        ),
        new(
            @"(?s)\[(CmdletBinding)[^\]]+\]",
            new Dictionary<int, string>
            {
                {1, ScopeName.PowerShellAttribute},
            }),
        new(
            @"(\[)([^\]]+)(\])(::)?",
            new Dictionary<int, string>
            {
                {1, ScopeName.PowerShellOperator},
                {2, ScopeName.PowerShellType},
                {3, ScopeName.PowerShellOperator},
                {4, ScopeName.PowerShellOperator},
            }),
    };

    public bool HasAlias(string lang)
    {
        switch (lang.ToLower())
        {
            case "posh":
            case "ps1":
                return true;

            default:
                return false;
        }
    }
}