// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace MDS.ColorCode.Compilation;

public interface ILanguageCompiler
{
    CompiledLanguage Compile(ILanguage language);
}