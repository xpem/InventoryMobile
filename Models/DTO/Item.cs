using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTO
{
    public record Item
    {
        [Key]
        public required int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int ExternalId { get; set; }

        public string? LocalFilePathImage1 { get; set; }

        public string? LocalFilePathImage2 { get; set; }
    }

}
