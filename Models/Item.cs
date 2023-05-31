namespace Models
{
    public class Item : ModelBase
    {
        public string? Name { get; set; }

        public string? TechnicalDescription { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public int AcquisitionType { get; set; }

        public string? PurchaseValue { get; set; }

        public string? PurchaseStore { get; set; }

        public decimal ResaleValue { get; set; }

        public int? Status { get; set; }

        public string? Comment { get; set; }
    }
}
