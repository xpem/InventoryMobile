namespace PersonalAssetsMobile.Models
{
    public class ItemUI
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }

        public Color CategoryColor { get; set; }

        public string SubCategoryIcon { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TechnicalDescription { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public decimal PurchaseValue { get; set; }

        public string PurchaseStore { get; set; }

        public decimal ResaleValue { set; get; }

        public int StatusId { get; set; }

        public string Comment { get; set; }

        public int AcquisitionTypeId { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

    }

    public class ItemGroup : List<ItemUI>
    {
        public string GroupingParameter { get; set; }

        public ItemGroup(string orderParameter,List<ItemUI> items) : base(items)
        {
            GroupingParameter = orderParameter;
        }
    }
}
