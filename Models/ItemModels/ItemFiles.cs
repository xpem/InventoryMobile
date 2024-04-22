namespace Models.ItemModels
{
    public record ItemFiles
    {
        public string? Image1 { get; set; }

        public string? Image2 { get; set; }
    }

    public class FileToUpload
    {
        public Stream? FileStream { get; init; }

        public string? FileName { get; init; }

        public string? FileContentType { get; init; }
    }
}
