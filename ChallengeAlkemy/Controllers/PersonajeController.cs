using ChallengeAlkemy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("characters")]
    //[Authorize]
    public class PersonajeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PersonajeController(ApplicationContext context)
        {
            _context = context;
        }

        // Listado General
        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> Get()
        {
            return await _context.Personajes.ToListAsync();
        }

        //Listado por Id
        /*[HttpGet("{id:int}")]
        public async Task<ActionResult<Personaje>> Get(int id)
        {
            var elemento = await _context.Personajes.FirstAsync(x => x.Id == id);
            if(elemento==null) NotFound();
            return Ok(elemento);
        }*/

        //Buscar por Nombre
        [HttpGet("name")]
        public async Task<ActionResult<Personaje>> Get(string name)
        {
            var elemento = await _context.Personajes.FirstAsync(x => x.Nombre == name);
            if (elemento == null) NotFound();
            return Ok(elemento);
        }

        //Buscar por Edad
        [HttpGet("{edad:int}")]
        public async Task<ActionResult<Personaje>> Get(int edad)
        {
            //var elemento = await _context.Personajes.FirstAsync(x => x.Edad == edad);
            //if (elemento == null) NotFound();
            return Ok(await _context.Personajes.FirstOrDefaultAsync(x=>x.Edad==edad));
        }

        /*[HttpGet("edad")]
        public async Task<ActionResult<Personaje>> Get(int edad)
        {
            var elemento = await _context.Personajes.FirstAsync(x => x.Edad == edad);
            if (elemento == null) NotFound();
            return Ok(elemento);
        }*/

        //Buscar por Id de Película

        [HttpPost]
        public async Task<ActionResult> Post(Personaje personaje)
        {
            _context.Add(personaje);
            await _context.SaveChangesAsync();
            return Ok(personaje);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Personaje personaje,int id)
        {
            //var existe = await _context.Personajes.AnyAsync(x => x.Id == id);
            //if (!existe) NotFound();
            _context.Update(personaje);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Personajes.AnyAsync(x => x.Id == id);
            if (!existe) NotFound();
            _context.Remove(new Personaje {Id = id});
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
