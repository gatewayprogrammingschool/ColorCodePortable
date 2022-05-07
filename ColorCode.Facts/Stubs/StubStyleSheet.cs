using MarkdownServer.ColorCode.Styling;

namespace MarkdownServer.ColorCode.Stubs
{
    public class StubStyleSheet : IStyleSheet
    {
        public string Name__getValue;
        public StyleDictionary Styles__getValue;

        public string Name
        {
            get { return Name__getValue; }
        }

        public StyleDictionary Styles
        {
            get { return Styles__getValue; }
        }
    }
}