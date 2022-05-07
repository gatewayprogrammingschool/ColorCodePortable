using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkdownServer.ColorCode.Formatting;

namespace MarkdownServer.ColorCode
{
    /// <summary>
    /// Provides easy access to ColorCode's built-in formatters.
    /// </summary>
    public static class Formatters
    {
        /// <summary>
        /// Gets the default formatter.
        /// </summary>
        /// <remarks>
        /// The default formatter produces HTML with inline styles.
        /// </remarks>
        public static IFormatter Default { get { return new HtmlFormatter(); } }
    }
}
