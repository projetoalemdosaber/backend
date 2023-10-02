using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedeSocial.Model
{
    public class Postagem
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(2000)]
        public string Texto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")] 
        [StringLength(5000)]
        public string Foto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string Video { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public string Curtir { get; set; }

        [Column(TypeName = "int")]
        public string Amei { get; set; }

        [Column(TypeName = "int")]
        public string Indico { get; set; }

        public virtual Tema? Tema { get; set; }

        public virtual User? User { get; set; }
    }
}
