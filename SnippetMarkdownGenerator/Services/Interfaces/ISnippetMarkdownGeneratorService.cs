namespace SnippetMarkdownGenerator.Services.Interfaces
{
    using SnippetMarkdownGenerator.Models;

    /// <summary>
    /// Service that can be used to generate markdown documentation for Visual Studio snippets.
    /// </summary>
    public interface ISnippetMarkdownGeneratorService
    {
        /// <summary>
        /// Generate markdown documentation for snippets located in file at passed file path.
        /// </summary>
        /// <param name="snippetFilePath"></param>
        /// <returns></returns>
        Attempt<string> GenerateMarkdownDocumentation(string snippetFilePath);
    }
}
