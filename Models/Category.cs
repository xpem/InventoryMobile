﻿namespace Models
{
    public class Category : ModelBase
    {
        public string? Name { get; set; }

        public string? Color { get; set; }

        public int? SystemDefault { get; set; }

        public List<SubCategory>? SubCategories { get; set; }

    }
}