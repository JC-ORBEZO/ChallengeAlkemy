﻿using ChallengeAlkemy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("api/Generos")]
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
    }
    
}