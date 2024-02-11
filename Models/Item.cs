namespace Models
{
    public class Item : ModelBase
    {
        public string? Name { get; set; }

        public string? TechnicalDescription { get; set; }

        public DateOnly AcquisitionDate { get; set; }

        public int AcquisitionType { get; set; }
        
        public decimal? PurchaseValue { get; set; }

        public string? PurchaseStore { get; set; }

        public decimal? ResaleValue { get; set; }

        public ItemSituation? Situation { get; set; }

        public string? Comment { get; set; }

        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

}
