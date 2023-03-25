using System.Text;
using SnippetMarkdownGenerator.Models;
using SnippetMarkdownGenerator.Services;
using SnippetMarkdownGenerator.Services.Interfaces;

//TODO: order snippets alphabetically when generating documenation.
//TODO: Look into using a T4 template instead.

string[] snippetFilePaths = new string[]
{
    @"C:\Users\dummyUsername\Downloads\code snippets\CollectionSnippets.snippet",
    @"C:\Users\dummyUsername\Downloads\code snippets\CommonCodeSnippets.snippet",
    @"C:\Users\dummyUsername\Downloads\code snippets\DefensiveCodeSnippets.snippet",
    @"C:\Users\dummyUsername\Downloads\code snippets\TestSnippets.snippet"
};


string xmlNamespace = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet";
StringBuilder stringBuilder = new();

foreach (string snippetFilePath in snippetFilePaths)
{
    ISnippetMarkdownGeneratorService snippetMarkdownGeneratorService = new SnippetMarkdownGeneratorService(xmlNamespace);
    Attempt<string> generatedMarkdownDocumenationAttempt = snippetMarkdownGeneratorService.GenerateMarkdownDocumentation(snippetFilePath);
    if (!generatedMarkdownDocumenationAttempt.Success)
    {
        throw new Exception(generatedMarkdownDocumenationAttempt.ErrorMessage);
    }

    stringBuilder.AppendLine($"# {Path.GetFileName(snippetFilePath)}");
    stringBuilder.AppendLine(generatedMarkdownDocumenationAttempt.Result);
}
Console.WriteLine(stringBuilder.ToString());
