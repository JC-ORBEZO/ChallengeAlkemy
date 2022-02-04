using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChallengeAlkemy.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public List<Pelicula> Peliculas { get; set; }
    }
}
