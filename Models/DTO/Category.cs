namespace Models.DTO
{
    public record Category : ModelBase
    {
        public string? Name { get; set; }

        public string? Color { get; set; }

        public bool? SystemDefault { get; set; }

        public List<SubCategoryDTO>? SubCategories { get; set; }

        /// <summary>
        /// used in get item
        /// </summary>
        public SubCategoryDTO? SubCategory { get; set; }
    }
}