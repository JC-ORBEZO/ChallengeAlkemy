using ChallengeAlkemy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("movies")]
    public class PeliculaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PeliculaController(ApplicationContext context)
        {
            _context = context;
        }
        /*
         * métodos:
         * https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.tolistasync?view=efcore-6.0
         */
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

        [HttpPost]
        public async Task<ActionResult> Post(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Pelicula pelicula, int id)
        {
            var existe = await _context.Personajes.AnyAsync(x => x.Id == id);
            //if (existe==false) return BadRequest("El Id ingresado no existe.");
            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {            
            _context.Personajes.Remove(new Personaje { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
