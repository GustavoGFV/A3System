namespace A3System.Dbo.Dto.Setor

{
    public class ReadSetorDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt = DateTime.Now;
    }
}

