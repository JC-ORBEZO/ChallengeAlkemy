using ChallengeAlkemy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("api/Generos")]
    //[Authorize]
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public GeneroController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genero>>> Get()
        {
            return await _context.Generos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var existe = await _context.Generos.AnyAsync(x => x.Id == id);
            if (!existe) return BadRequest($"No existe el Genero de Id: {id}");
            return await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero)
        {
            _context.Add(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Genero genero, int id)
        {
            if (genero.Id != id) return BadRequest("El Id Genero ingresado no coincidE con el Id de la URL");
            var existe = await _context.Generos.AnyAsync(x => x.Id == id);
            if (existe == false) return NotFound();
            _context.Update(genero);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Generos.AnyAsync(x => x.Id == id);
            if (!existe) return NotFound();
            _context.Remove(new Genero { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
    
}
