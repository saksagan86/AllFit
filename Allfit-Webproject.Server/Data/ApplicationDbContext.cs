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
        public DbSet<Openingstijd> Openingstijden { get; set; }
        public DbSet<Abonnement> Abonnement { get; set; }
        public DbSet<Les> Lessen { get; set; }
        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<Proefles> Proefles { get; set; }
        public DbSet<Doel> Doelen { get; set; }
        public DbSet<GebruikerDoel> GebruikerDoelen { get; set; }
        public DbSet<Voedingsschema> Voedingsschemas { get; set; }
        public DbSet<VoedingsschemaRegel> VoedingsschemaRegels { get; set; }
        public DbSet<Verhaal> Verhalen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aanbod>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Aanbod>("aanbod")
                .HasValue<Fitness>("fitness")
                .HasValue<Groepsles>("groepsles")
                .HasValue<Kickboks>("kickboks");

            modelBuilder.Entity<Gebruiker>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Lid>("Lid")
                .HasValue<Trainer>("trainer");

            modelBuilder.Entity<Doel>()
                .ToTable("Doelen")
                .HasKey(d => d.id);

            modelBuilder.Entity<Voedingsschema>()
                .ToTable("Voedingsschemas")
                .HasKey(v => v.id);

            modelBuilder.Entity<Voedingsschema>()
                .HasOne(v => v.doel)
                .WithMany(d => d.voedingsschemas)
                .HasForeignKey(v => v.doelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VoedingsschemaRegel>()
                .ToTable("VoedingsschemaRegels")
                .HasKey(r => r.id);

            modelBuilder.Entity<VoedingsschemaRegel>()
                .HasOne(r => r.voedingsschema)
                .WithMany(v => v.regels)
                .HasForeignKey(r => r.voedingsschemaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GebruikerDoel>()
                .ToTable("GebruikerDoelen")
                .HasKey(gd => gd.id);

            modelBuilder.Entity<GebruikerDoel>()
                .HasIndex(gd => gd.gebruikerId)
                .IsUnique();

            modelBuilder.Entity<GebruikerDoel>()
                .HasOne(gd => gd.gebruiker)
                .WithMany()
                .HasForeignKey(gd => gd.gebruikerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GebruikerDoel>()
                .HasOne(gd => gd.doel)
                .WithMany()
                .HasForeignKey(gd => gd.doelId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Verhaal>()
                .Property(v => v.Fotos)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
