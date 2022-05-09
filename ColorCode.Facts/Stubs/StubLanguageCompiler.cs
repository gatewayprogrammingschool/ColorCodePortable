using System;
using MDS.ColorCode.Compilation;

namespace MDS.ColorCode.Stubs
{
    public class StubLanguageCompiler : ILanguageCompiler
    {
        public ILanguage Compile__language;
        public Func<ILanguage, CompiledLanguage> Compile__do;
        public CompiledLanguage Compile__return;

        public CompiledLanguage Compile(ILanguage language)
        {
            Compile__language = language;

            if (Compile__do != null)
                return Compile__do(language);
            else
                return Compile__return;
        }
    }
}