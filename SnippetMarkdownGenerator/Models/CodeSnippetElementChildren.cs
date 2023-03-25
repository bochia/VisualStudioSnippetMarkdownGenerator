namespace SnippetMarkdownGenerator.Models
{
    using System.Xml.Linq;

    public class CodeSnippetElementChildren
    {
        public XElement Title { get; set; }
        public XElement Shortcut { get; set; }
        public XElement Code { get; set; }
        private readonly XNamespace xmlNamespace;

        public CodeSnippetElementChildren(XElement codeSnippetElement, XNamespace xmlNamespace)
        {
            this.xmlNamespace = xmlNamespace ?? throw new ArgumentNullException(nameof(xmlNamespace));

            XElement? headerElement = codeSnippetElement.Element(AddNamespaceToTag("Header"));

            if (headerElement == null)
            {
                throw new Exception($"{nameof(headerElement)} was null.");
            }

            XElement? title = headerElement.Element(AddNamespaceToTag("Title"));
            if (title == null)
            {
                throw new Exception($"{nameof(title)} was null.");
            }
            Title = title;

            XElement? shortcut = headerElement.Element(AddNamespaceToTag("Shortcut"));
            if (shortcut == null)
            {
                throw new Exception($"{nameof(shortcut)} was null.");
            }
            Shortcut = shortcut;

            XElement? code = codeSnippetElement.Element(AddNamespaceToTag("Snippet"))
                                                     ?.Element(AddNamespaceToTag("Code"));
            if (code == null)
            {
                throw new Exception($"{nameof(code)} was null.");
            }
            Code = code;
        }

        private XName AddNamespaceToTag(string tagName)
        {
            return xmlNamespace + tagName;
        }

    }
}
