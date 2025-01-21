namespace Models.DTO
{
    public class SubCategory : DTOModelBase
    {
        public string? Name { get; set; }

        public string? IconName { get; set; }

        public bool? SystemDefault { get; set; }

        public int CategoryId { get; set; }

    }
}
