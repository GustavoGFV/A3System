using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3System.Dbo.Model
{
    /// <summary>
    /// Estrutura do banco de dados do Setor
    /// </summary>    

    [Table("Setor")]
    public class SetorModel
    {
        [Key]
        [Column("Id")]
        public int? Id { get; set; }

        [Column("Nome")]
        public string? Name { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Ativo")]
        public bool Active { get; set; }

        [Column("CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
