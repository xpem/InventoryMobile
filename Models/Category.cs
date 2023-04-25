using System.Drawing;

namespace Models
{
    public class Category : ModelBase
    {
        public string? Name { get; set; }

        public string? Color { get; set; }

        public int? SystemDefault { get; set; }

    }
}