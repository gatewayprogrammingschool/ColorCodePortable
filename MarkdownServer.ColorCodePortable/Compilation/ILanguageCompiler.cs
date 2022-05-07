// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace MarkdownServer.ColorCode.Compilation
{
    public interface ILanguageCompiler
    {
        CompiledLanguage Compile(ILanguage language);
    }
}