using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Tests
{
    [TestClass]
    public class SportSchoolEndpointTests
    {
        [TestMethod]
        public async Task GetAllSportscholen_ReturnsSeededSportschool_UsingActualDatabase()
        {
            // Arrange
            await using var factory = new WebApplicationFactory<Program>();

            int seededId = 0;
            var seededName = "IntegrationTestGym";
            var seededCity = "IntegrationCity";

            using (var scope = factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                // Ensure DB exists (will create if using LocalDB and migrations applied externally)
                db.Database.EnsureCreated();

                var s = new Sportschool
                {
                    naam = seededName,
                    adres = "Seeded Address 1",
                    stad = seededCity,
                    lon = 0f,
                    lat = 0f,
                    openingstijdenSportschool = new System.Collections.Generic.List<Openingstijd>(),
                    alleFaciliteiten = new System.Collections.Generic.List<Faciliteit>()
                };

                db.Sportscholen.Add(s);
                await db.SaveChangesAsync();
                seededId = s.id;
            }

            try
            {
                // Act
                using var client = factory.CreateClient();
                var response = await client.GetAsync("/api/Sportschool");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.IsTrue(content.Contains(seededName), "Response should contain the seeded sportschool name");
                Assert.IsTrue(content.Contains(seededCity), "Response should contain the seeded sportschool city");
            }
            finally
            {
                // Cleanup: remove the seeded data to avoid polluting the actual database
                using var cleanupScope = factory.Services.CreateScope();
                var cleanupDb = cleanupScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var existing = await cleanupDb.Sportscholen.FindAsync(seededId);
                if (existing != null)
                {
                    cleanupDb.Sportscholen.Remove(existing);
                    await cleanupDb.SaveChangesAsync();
                }
            }
        }
    }
}
