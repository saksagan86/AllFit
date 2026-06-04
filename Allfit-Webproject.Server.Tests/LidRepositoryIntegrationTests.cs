using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Repository;
using Allfit_Webproject.Server.Models;

namespace Tests
{
    [TestClass]
    public class LidRepositoryIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> CreateInMemoryOptions(SqliteConnection connection)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;
        }

        [TestMethod]
        public async Task AddAndGetLid_WithSqliteInMemory_Works()
        {
            using var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = CreateInMemoryOptions(connection);

            // Create schema
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                // Seed a lidmaatschap required by service/Repo usage
                var lm = new Lidmaatschap { Naam = "TestPlan", Lidmaatschapgeld = 10.00 };
                context.Lidmaatschap.Add(lm);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var repo = new LidRepository(context);

                var lid = new Lid
                {
                    naam = "IntegrationTest",
                    email = "int@test.local",
                    wachtwoord = "hash",
                    telefoonnummer = "000",
                    geboortedatum = "2000-01-01",
                    adres = "Street",
                    huisnummer = "1",
                    woonplaats = "City",
                    postcode = "0000AA",
                    lidmaatschapId = 1,
                    isActief = true
                };

                var newId = await repo.AddLidAsync(lid);
                Assert.IsTrue(newId > 0);

                var fetched = await repo.GetByEmailAsync("int@test.local");
                Assert.IsNotNull(fetched);
                Assert.AreEqual("IntegrationTest", fetched!.naam);

                var lidmaatschap = await repo.GetLidmaatschapById(1);
                Assert.IsNotNull(lidmaatschap);
            }
        }
    }
}