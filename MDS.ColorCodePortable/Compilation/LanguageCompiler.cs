// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Text;
using System.Text.RegularExpressions;

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Compilation;

public class LanguageCompiler : ILanguageCompiler
{
    private static readonly Regex numberOfCapturesRegex = new(@"(?x)(?<!(\\|(?!\\)\(\?))\((?!\?)", Compiled());
    private readonly Dictionary<string, CompiledLanguage> compiledLanguages;
    private readonly ReaderWriterLockSlim compileLock;

    public LanguageCompiler(Dictionary<string, CompiledLanguage> compiledLanguages)
    {
        this.compiledLanguages = compiledLanguages;

        compileLock = new();
    }

    public CompiledLanguage Compile(ILanguage language)
    {
        Guard.ArgNotNull(language, "language");

        if (string.IsNullOrEmpty(language.Id))
            throw new ArgumentException("The language identifier must not be null.", "language");

        CompiledLanguage compiledLanguage;

        compileLock.EnterReadLock();
        try
        {
            // for performance reasons we should first try with
            // only a read lock since the majority of the time
            // it'll be created already and upgradeable lock blocks
            if (compiledLanguages.ContainsKey(language.Id))
                return compiledLanguages[language.Id];
        }
        finally
        {
            compileLock.ExitReadLock();
        }

        compileLock.EnterUpgradeableReadLock();
        try
        {
            if (compiledLanguages.ContainsKey(language.Id))
                compiledLanguage = compiledLanguages[language.Id];
            else
            {
                compileLock.EnterWriteLock();

                try
                {
                    if (string.IsNullOrEmpty(language.Name))
                        throw new ArgumentException("The language name must not be null or empty.", "language");

                    if (language.Rules == null || language.Rules.Count == 0)
                        throw new ArgumentException("The language rules collection must not be empty.", "language");

                    compiledLanguage = CompileLanguage(language);

                    compiledLanguages.Add(compiledLanguage.Id, compiledLanguage);
                }
                finally
                {
                    compileLock.ExitWriteLock();
                }
            }
        }
        finally
        {
            compileLock.ExitUpgradeableReadLock();
        }

        return compiledLanguage;
    }

    private static RegexOptions Compiled()
    {
        RegexOptions compiledOption;
        if (Enum.TryParse("Compiled", out compiledOption))
        {
            return compiledOption;
        }
        return RegexOptions.None;
    }

    private static CompiledLanguage CompileLanguage(ILanguage language)
    {
        string id = language.Id;
        string name = language.Name;
        Regex regex;
        IList<string> captures;

        CompileRules(language.Rules, out regex, out captures);

        return new(id, name, regex, captures);
    }

    private static void CompileRules(IList<LanguageRule> rules,
                                     out Regex regex,
                                     out IList<string> captures)
    {
        StringBuilder regexBuilder = new();
        captures = new List<string>();

        regexBuilder.AppendLine("(?x)");
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        captures.Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        CompileRule(rules[0], regexBuilder, captures, true);

        for (int i = 1; i < rules.Count; i++)
            CompileRule(rules[i], regexBuilder, captures, false);

        regex = new(regexBuilder.ToString());
    }


    private static void CompileRule(LanguageRule languageRule,
                                             StringBuilder regex,
                                             ICollection<string> captures,
                                             bool isFirstRule)
    {
        if (!isFirstRule)
        {
            regex.AppendLine();
            regex.AppendLine();
            regex.AppendLine("|");
            regex.AppendLine();
        }

        regex.AppendFormat("(?-xis)(?m)({0})(?x)", languageRule.Regex);

        int numberOfCaptures = GetNumberOfCaptures(languageRule.Regex);

        for (int i = 0; i <= numberOfCaptures; i++)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string scope = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            foreach (int captureIndex in languageRule.Captures.Keys)
            {
                if (i == captureIndex)
                {
                    scope = languageRule.Captures[captureIndex];
                    break;
                }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            captures.Add(scope);
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }

    private static int GetNumberOfCaptures(string regex)
        => numberOfCapturesRegex.Matches(regex).Count;
}