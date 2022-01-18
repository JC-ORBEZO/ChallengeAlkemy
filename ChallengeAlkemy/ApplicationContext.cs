using ChallengeAlkemy.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeAlkemy
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Personaje> Personajes { get; set; }
    }
}
