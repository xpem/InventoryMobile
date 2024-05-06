﻿namespace Models.ItemModels
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

    public class ImageFile(string fileName, string? imageFilePath = null)
    {
        public string FileName { get; set; } = fileName;

        public string? ImageFilePath { get; set; } = imageFilePath;
    }
}