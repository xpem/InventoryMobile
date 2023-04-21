namespace Models.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string? Content { get; set; }

        public ErrorTypes? Error { get; set; }
    }
}
