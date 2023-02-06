using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Color Color { get; set; }
    }

    public class CategoryList
    {
        public List<Category> List { get; } = new()
    {
        new Category { Id = 1, Name = "Casa", Color = Color.FromArgb("#EF7C8E") },
        new Category { Id = 1, Name = "Carro", Color = Color.FromArgb("#EF7C8E") },
        new Category { Id = 1, Name = "Moto", Color = Color.FromArgb("#EF7C8E") },
        new Category { Id = 1, Name = "Vestimenta", Color = Color.FromArgb("#EF7C8E") },
    };
    }

}
