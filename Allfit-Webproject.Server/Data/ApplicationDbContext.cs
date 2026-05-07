using Allfit_Webproject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Allfit_Webproject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lid> Lid { get; set; }
        public DbSet<Aanbod> Aanbod { get; set; }
        public DbSet<Groepsles> Groepslessen { get; set; }
        public DbSet<Fitness> FitnessAanbod { get; set; }
        public DbSet<Kickboks> KickboksAanbod { get; set; }
        public DbSet<Sportschool> Sportscholen { get; set; }
        public DbSet<Faciliteit> Faciliteiten { get; set; }
        public DbSet<ContactFormulier> ContactFormulieren { get; set; }
        public DbSet<Gebruiker> Gebruiker { get; set; }
        public DbSet<Lidmaatschap> Lidmaatschap { get; set; }
    }
}
