using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RedeSocial.Model
{
    public class Tema
    {
        [Key] // Primary Key Id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Identity 1,1
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Assunto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(2000)]
        public string Descricao { get; set; } = string.Empty;

        public virtual ICollection<Postagem>? Postagem { get; set; }
    }
}
