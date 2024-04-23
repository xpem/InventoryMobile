namespace InventoryMobile.UIModels
{
    public class FixedIndexesImagePaths()
    {
        public UIImagePath Image1 { get; set; }
        public UIImagePath Image2 { get; set; }
    }

    public class UIImagePath(string imageFilePath, Guid? externalId = null) : BindableObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid? ExternalId { get; init; } = externalId;

        public string ImageFilePath { get; init; } = imageFilePath;
    }
}
