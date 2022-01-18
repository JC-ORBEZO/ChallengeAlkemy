using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeAlkemy.Models
{
    public class Personaje
    {
        [Key]
        public int Id { get; set; }
        public string Imagen { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Range(0,150)]
        public int Edad { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Peso { get; set; }
        public string Historia { get; set; }
        //public List<Pelicula> Peliculas { get; set; }        
    }
}
