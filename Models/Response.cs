using System.Text.Json.Nodes;

namespace Models
{
    public class Response
    {
        public bool Success { get; set; }

        public JsonNode? Content { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
