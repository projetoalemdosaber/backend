using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using alemDoSaber.Util;

namespace RedeSocial.Model
{
    public class Postagem : Auditable
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
        public string? Foto { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string? Video { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int Curtir { get; set; } = 0;

        [Column(TypeName = "int")]
        public int Amei { get; set; } = 0;

        [Column(TypeName = "int")]
        public int Indico { get; set; } = 0;

        public virtual Tema? Tema { get; set; }

        public virtual User? User { get; set; }
    }
}
