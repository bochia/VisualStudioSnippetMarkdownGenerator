namespace SnippetMarkdownGenerator.Services
{
    using System;
    using System.Text;
    using System.Xml.Linq;
    using SnippetMarkdownGenerator.Models;
    using SnippetMarkdownGenerator.Services.Interfaces;


    /// <inheritdoc />
    public class SnippetMarkdownGeneratorService : ISnippetMarkdownGeneratorService
    {
        private const string BreakTag = "<br>";
        private const string CodeSnippetTagName = "CodeSnippet";
        private const string Header3Delimeter = "###";
        private const string HorizontalDivider = "---";
        private const string MarkdownCodeDelimeter = "```";
        private const string Space = " ";

        private XNamespace xmlNamespace;

        public SnippetMarkdownGeneratorService(string xmlNamespace)
        {
            this.xmlNamespace = xmlNamespace;
        }

        /// <inheritdoc />
        public Attempt<string> GenerateMarkdownDocumentation(string snippetsFilePath)
        {
            if (string.IsNullOrWhiteSpace(snippetsFilePath))
            {
                return new Attempt<string>()
                {
                    ErrorMessage = $"{nameof(snippetsFilePath)} cannot be null, empty, or whitespace."
                };
            }

            try
            {
                XDocument document = XDocument.Load(snippetsFilePath);
                if (document == null)
                {
                    return new Attempt<string>()
                    {
                        ErrorMessage = $"{nameof(document)} was null."
                    };
                }

                IEnumerable<XElement> codeSnippetElements = document.Descendants(xmlNamespace + CodeSnippetTagName);

                if (codeSnippetElements == null || !codeSnippetElements.Any())
                {
                    return new Attempt<string>()
                    {
                        ErrorMessage = "There were not snippets to process."
                    };
                }

                StringBuilder stringBuilder = new StringBuilder();

                foreach (XElement codeSnippetElement in codeSnippetElements)
                {
                    GenerateMarkdownDocumenationForSnippet(codeSnippetElement, stringBuilder);
                }

                return new Attempt<string>()
                {
                    Success = true,
                    Result = stringBuilder.ToString()
                };
            }
            catch (Exception ex)
            {
                return new Attempt<string>()
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        private void GenerateMarkdownDocumenationForSnippet(XElement codeSnippetElement, StringBuilder stringBuilder)
        {
            CodeSnippetElementChildren codeSnippetElementChildren = new CodeSnippetElementChildren(codeSnippetElement, xmlNamespace);

            stringBuilder.AppendLine($"{Header3Delimeter}{Space}{codeSnippetElementChildren.Title.Value}");
            stringBuilder.AppendLine($"{codeSnippetElementChildren.Shortcut.Value}{BreakTag}{BreakTag}");
            stringBuilder.AppendLine($"{MarkdownCodeDelimeter}\n{codeSnippetElementChildren.Code.Value}\n{MarkdownCodeDelimeter}\n");
            stringBuilder.AppendLine(HorizontalDivider);
        }

        public string GetStringsBetweenTwoStrings(string originialStr, string startStr, string endString)
        {
            string FinalString;
            int Pos1 = originialStr.IndexOf(startStr) + startStr.Length;
            int Pos2 = originialStr.IndexOf(endString);
            FinalString = originialStr.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
