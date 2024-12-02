using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace A3System.Dbo.Dto.Setor
{
    public class CreateSetorDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public bool Active = true;
        public DateTime CreatedAt = DateTime.Now;
        public DateTime UpdatedAt = DateTime.Now;
    }
}
