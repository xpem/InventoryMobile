namespace Models.ItemModels
{
    public record ItemFileNames
    {
        public string? Image1 { get; set; }

        public string? Image2 { get; set; }
    }

    public class ItemFilesToUpload
    {
        public ImageFile? Image1 { get; set; }

        public ImageFile? Image2 { get; set; }
    }

    public class ImageFile()
    {
        public string? FileName { get; set; }

        public int FileId { get; set; }

        public string? ImageFilePath { get; set; } = null;
    }

}
