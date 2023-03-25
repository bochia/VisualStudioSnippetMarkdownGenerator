namespace SnippetMarkdownGenerator.Models
{
    public class Attempt
    {
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }

        public Attempt()
        {
            ErrorMessage = string.Empty;
            Success = false;
        }
    }
}
