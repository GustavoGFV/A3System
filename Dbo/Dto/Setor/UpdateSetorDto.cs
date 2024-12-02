using System.ComponentModel.DataAnnotations;

namespace A3System.Dbo.Dto.Setor
{
    public class UpdateSetorDto
    {
        [Required]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public DateTime UpdatedAt = DateTime.Now;
    }
}
