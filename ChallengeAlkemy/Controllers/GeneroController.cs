using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("api/Generos")]
    public class GeneroController:ControllerBase
    {
        private readonly ApplicationContext _context;

        public GeneroController(ApplicationContext context)
        {
            _context = context;
        }        
    }
}
