using Allfit_Webproject.Server.Models;
using Allfit_Webproject.Server.Repository;
using Moq;

namespace Allfit_Webproject.Tests;

[TestClass]
public class AanbodServiceTest
{
    [TestMethod]
    public void GetById_Returns_Correct_Fitness()
    {
        // Arrange
        var mockRepo = new Mock<IAanbodRepository>();
        mockRepo.Setup(repo => repo.GetFitnessAsync(1))
            .ReturnsAsync(new List<Fitness>
            {
                new Fitness { id = 1, naam = "Fitness 1", image = "fitness1.jpg" }
            });

        // Act
        var aanbodService = new AanbodService(mockRepo.Object);
        var result = aanbodService.GetFitnessAsync(1).Result;

        // Assert
        Assert.AreEqual("Fitness 1", result.First().Naam);
    }

    [TestMethod]
    public void GetById_Returns_Error_Fitness()
    {
        // Arrange
        var mockRepo = new Mock<IAanbodRepository>();
        mockRepo.Setup(repo => repo.GetFitnessAsync(1))
            .ReturnsAsync(new List<Fitness>());
        // Act
        var aanbodService = new AanbodService(mockRepo.Object);
        var result = aanbodService.GetFitnessAsync(1).Result;
        // Assert
        Assert.IsEmpty(result);
    }

}
