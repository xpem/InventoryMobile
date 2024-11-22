namespace Models
{
    public record SubCategory : ModelBase
    {
        public string? Name { get; set; }

        public string? IconName { get; set; }

        public bool? SystemDefault { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }
    }
}
