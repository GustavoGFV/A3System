using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3System.Dbo.Model
{
    /// <summary>
    /// Estrutura do banco de dados do Usuarios
    /// </summary>  

    [Table("Usuarios")]
    public class UserModel
    {
        [Key]
        [Column("Id")]
        public int? Id { get; set; }

        [Column("Username")]
        public string? Username { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("Password")]
        public string? Password { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Surname")]
        public string? Surname { get; set; }

        [Column("Role")]
        public string? Role { get; set; }
    }
}
