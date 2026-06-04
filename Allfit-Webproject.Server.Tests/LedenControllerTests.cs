using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Allfit_Webproject.Server.Controllers;
using Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Tests
{
    [TestClass]
    public class LedenControllerTests
    {
        [TestMethod]
        public async Task RegisterLid_InvalidModel_ReturnsBadRequest()
        {
            var mockService = new Mock<ILidService>();
            var controller = new LedenController(mockService.Object);

            // Simulate model validation failure (e.g., missing Email)
            controller.ModelState.AddModelError("Email", "The Email field is required.");

            var dto = new RegisterLidDTO { Naam = "Test", Email = "" };
            var result = await controller.RegisterLid(dto);

            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest, "Expected BadRequestObjectResult");
            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [TestMethod]
        public async Task RegisterLid_ValidModel_ReturnsOkWithId()
        {
            var mockService = new Mock<ILidService>();
            mockService.Setup(s => s.RegisterLidAsync(It.IsAny<RegisterLidDTO>()))
                       .ReturnsAsync(123);

            var controller = new LedenController(mockService.Object);

            var dto = new RegisterLidDTO
            {
                Naam = "Test",
                Email = "test@example.com",
                Wachtwoord = "secret123",
                BevestigWachtwoord = "secret123",
                Telefoonnummer = "0123456789",
                Geboortedatum = "1990-01-01",
                Adres = "Street",
                Huisnummer = "1",
                Woonplaats = "City",
                Postcode = "1234AB",
                AccepteerVoorwaarden = true
            };

            var result = await controller.RegisterLid(dto);

            var ok = result as OkObjectResult;
            Assert.IsNotNull(ok, "Expected OkObjectResult");
            Assert.AreEqual(200, ok.StatusCode);

            dynamic payload = ok.Value!;
            Assert.AreEqual(123, (int)payload.id);
        }
    }
}