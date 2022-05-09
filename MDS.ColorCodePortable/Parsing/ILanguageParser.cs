// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace MDS.ColorCode.Parsing
{
    public interface ILanguageParser
    {
        void Parse(string sourceCode,
                   ILanguage language,
                   Action<string, IList<Scope>> parseHandler);
    }
}