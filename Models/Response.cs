using System.Text.Json.Nodes;

namespace Models
{
    public class Response
    {
        public bool Success { get; set; }

        public string? Content { get; set; }

        public System.Net.HttpStatusCode HttpStatusCode { get; set; }
    }
}
