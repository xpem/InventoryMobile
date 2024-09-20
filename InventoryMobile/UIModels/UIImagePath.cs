namespace InventoryMobile.UIModels
{
    public class FixedIndexesImagePaths()
    {
        public UIImagePath Image1 { get; set; }
        public UIImagePath Image2 { get; set; }
    }

    public class UIImagePath(string imageFilePath, string fileName, string externalFileName = null) : BindableObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string ExternalFileName { get; init; } = externalFileName;

        public string FileName { get; init; } = fileName;

        public string ImageFilePath { get; init; } = imageFilePath;
    }
}
