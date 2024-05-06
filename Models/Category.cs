namespace Models
{
    public record Category : ModelBase
    {
        public string? Name { get; set; }

        public string? Color { get; set; }

        public bool? SystemDefault { get; set; }

        public List<SubCategory>? SubCategories { get; set; }

        /// <summary>
        /// used in get item
        /// </summary>
        public SubCategory? SubCategory { get; set; }
    }
}