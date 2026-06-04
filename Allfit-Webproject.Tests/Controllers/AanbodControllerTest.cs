using Allfit_Webproject.Server.Controllers;
using Allfit_Webproject.Server.Dtos;
using Allfit_Webproject.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Allfit_Webproject.Tests;

[TestClass]
public class AanbodControllerTest
{
    [TestMethod]
    public void TestGetAll_CorrectInput_ReturnsOk()
    {
        // Arrange
        var mockService = new Mock<IAanbodService>();
        mockService.Setup(service => service.GetAllAsync(1))
            .ReturnsAsync(new List<AanbodDTO>
        {
            new AanbodDTO { Id = 1, Naam = "Fitness" },
            new AanbodDTO { Id = 2, Naam = "Groepsles" },
            new AanbodDTO { Id = 3, Naam = "Kickboksen" },
        });

        // Act
        var aanbodController = new AanbodController(mockService.Object);
        var result = aanbodController.GetAll(1).Result as OkObjectResult;

        // Assert
        Assert.AreEqual(200, result.StatusCode); 
    }

    [TestMethod]
    public void TestGetAll_IncorrectInput_ReturnsEmptyList()
    {
        // Arrange
        var mockService = new Mock<IAanbodService>();
        mockService.Setup(service => service.GetAllAsync(1))
            .ReturnsAsync(new List<AanbodDTO>());
        // Act
        var aanbodController = new AanbodController(mockService.Object);
        var result = aanbodController.GetAll(1).Result as OkObjectResult;
        // Assert
        Assert.IsEmpty(result.Value as List<AanbodDTO>);
    }
}
