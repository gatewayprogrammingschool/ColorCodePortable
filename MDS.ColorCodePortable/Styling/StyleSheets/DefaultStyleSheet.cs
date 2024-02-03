// Copyright (c) Microsoft Corporation.  All rights reserved.

using MDS.ColorCode.Common;

namespace MDS.ColorCode.Styling.StyleSheets;

public class DefaultStyleSheet : IStyleSheet
{
    private static readonly Color DullRed = new(163, 21, 21);
    private static readonly StyleDictionary styles;

    static DefaultStyleSheet()
        => styles = new()
        {
            new(ScopeName.PlainText)
            {
                Foreground = Color.Black,
                Background = Color.White,
                CssClassName = "plainText",
            },
            new(ScopeName.HtmlServerSideScript)
            {
                Background = Color.Yellow,
                CssClassName = "htmlServerSideScript",
            },
            new(ScopeName.HtmlComment)
            {
                Foreground = Color.Green,
                CssClassName = "htmlComment",
            },
            new(ScopeName.HtmlTagDelimiter)
            {
                Foreground = Color.Blue,
                CssClassName = "htmlTagDelimiter",
            },
            new(ScopeName.HtmlElementName)
            {
                Foreground = DullRed,
                CssClassName = "htmlElementName",
            },
            new(ScopeName.HtmlAttributeName)
            {
                Foreground = Color.Red,
                CssClassName = "htmlAttributeName",
            },
            new(ScopeName.HtmlAttributeValue)
            {
                Foreground = Color.Blue,
                CssClassName = "htmlAttributeValue",
            },
            new(ScopeName.HtmlOperator)
            {
                Foreground = Color.Blue,
                CssClassName = "htmlOperator",
            },
            new(ScopeName.Comment)
            {
                Foreground = Color.Green,
                CssClassName = "comment",
            },
            new(ScopeName.XmlDocTag)
            {
                Foreground = Color.Gray,
                CssClassName = "xmlDocTag",
            },
            new(ScopeName.XmlDocComment)
            {
                Foreground = Color.Green,
                CssClassName = "xmlDocComment",
            },
            new(ScopeName.String)
            {
                Foreground = DullRed,
                CssClassName = "string",
            },
            new(ScopeName.StringCSharpVerbatim)
            {
                Foreground = DullRed,
                CssClassName = "stringCSharpVerbatim",
            },
            new(ScopeName.Keyword)
            {
                Foreground = Color.Blue,
                CssClassName = "keyword",
            },
            new(ScopeName.PreprocessorKeyword)
            {
                Foreground = Color.Blue,
                CssClassName = "preprocessorKeyword",
            },
            new(ScopeName.HtmlEntity)
            {
                Foreground = Color.Red,
                CssClassName = "htmlEntity",
            },
            new(ScopeName.XmlAttribute)
            {
                Foreground = Color.Red,
                CssClassName = "xmlAttribute",
            },
            new(ScopeName.XmlAttributeQuotes)
            {
                Foreground = Color.Black,
                CssClassName = "xmlAttributeQuotes",
            },
            new(ScopeName.XmlAttributeValue)
            {
                Foreground = Color.Blue,
                CssClassName = "xmlAttributeValue",
            },
            new(ScopeName.XmlCDataSection)
            {
                Foreground = Color.Gray,
                CssClassName = "xmlCDataSection",
            },
            new(ScopeName.XmlComment)
            {
                Foreground = Color.Green,
                CssClassName = "xmlComment",
            },
            new(ScopeName.XmlDelimiter)
            {
                Foreground = Color.Blue,
                CssClassName = "xmlDelimiter",
            },
            new(ScopeName.XmlName)
            {
                Foreground = DullRed,
                CssClassName = "xmlName",
            },
            new(ScopeName.ClassName)
            {
                Foreground = Color.MediumTurquoise,
                CssClassName = "className",
            },
            new(ScopeName.CssSelector)
            {
                Foreground = DullRed,
                CssClassName = "cssSelector",
            },
            new(ScopeName.CssPropertyName)
            {
                Foreground = Color.Red,
                CssClassName = "cssPropertyName",
            },
            new(ScopeName.CssPropertyValue)
            {
                Foreground = Color.Blue,
                CssClassName = "cssPropertyValue",
            },
            new(ScopeName.SqlSystemFunction)
            {
                Foreground = Color.Magenta,
                CssClassName = "sqlSystemFunction",
            },
            new(ScopeName.PowerShellAttribute)
            {
                Foreground = Color.PowderBlue,
                CssClassName = "powershellAttribute",
            },
            new(ScopeName.PowerShellOperator)
            {
                Foreground = Color.Gray,
                CssClassName = "powershellOperator",
            },
            new(ScopeName.PowerShellType)
            {
                Foreground = Color.Teal,
                CssClassName = "powershellType",
            },
            new(ScopeName.PowerShellVariable)
            {
                Foreground = Color.OrangeRed,
                CssClassName = "powershellVariable",
            },

            new(ScopeName.Type)
            {
                Foreground = Color.Teal,
                CssClassName = "type",
            },
            new(ScopeName.TypeVariable)
            {
                Foreground = Color.Teal,
                Italic = true,
                CssClassName = "typeVariable",
            },
            new(ScopeName.NameSpace)
            {
                Foreground = Color.Navy,
                CssClassName = "namespace",
            },
            new(ScopeName.Constructor)
            {
                Foreground = Color.Purple,
                CssClassName = "constructor",
            },
            new(ScopeName.Predefined)
            {
                Foreground = Color.Navy,
                CssClassName = "predefined",
            },
            new(ScopeName.PseudoKeyword)
            {
                Foreground = Color.Navy,
                CssClassName = "pseudoKeyword",
            },
            new(ScopeName.StringEscape)
            {
                Foreground = Color.Gray,
                CssClassName = "stringEscape",
            },
            new(ScopeName.ControlKeyword)
            {
                Foreground = Color.Blue,
                CssClassName = "controlKeyword",
            },
            new(ScopeName.Number)
            {
                CssClassName = "number",
            },
            new(ScopeName.Operator)
            {
                CssClassName = "operator",
            },
            new(ScopeName.Delimiter)
            {
                CssClassName = "delimiter",
            },

            new(ScopeName.MarkdownHeader)
            {
                // Foreground = Color.Bluelue,
                Bold = true,
                CssClassName = "markdownHeader",
            },
            new(ScopeName.MarkdownCode)
            {
                Foreground = Color.Teal,
                CssClassName = "markdownCode",
            },
            new(ScopeName.MarkdownListItem)
            {
                Bold = true,
                CssClassName = "markdownListItem",
            },
            new(ScopeName.MarkdownEmph)
            {
                Italic = true,
                CssClassName = "italic",
            },
            new(ScopeName.MarkdownBold)
            {
                Bold = true,
                CssClassName = "bold",
            },
        };

    public string Name => "DefaultStyleSheet";

    public StyleDictionary Styles => styles;
}