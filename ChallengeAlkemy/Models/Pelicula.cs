using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChallengeAlkemy.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Url]
        public string Imagen { get; set; }
        [Required]
        [MaxLength(50)]
        public string Titulo { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaCreacion { get; set; }
        [Range(1,5)]
        public int Calificacion { get; set; }
        public List<Personaje> PersonajeAsociado { get; set; }
    }
}
