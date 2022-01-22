using ChallengeAlkemy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("api/Peliculas")]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PeliculaController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            return await _context.Peliculas.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> Get(int id)
        {
            var existir = await _context.Peliculas.AnyAsync(x => x.Id == id);
            if (!existir) BadRequest($"La pelicula de Id: {id}, no existe");
            return await _context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
