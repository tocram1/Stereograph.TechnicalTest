using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Controllers;
using Stereograph.TechnicalTest.Api.Entities;
using Xunit;
using Person = Stereograph.TechnicalTest.Api.Models.Person;

namespace Stereograph.TechnicalTest.Tests;

public class PersonControllerTests
{
    private readonly DbContextOptions<ApplicationDbContext> m_DbContextOptions;

    public PersonControllerTests()
    {
        m_DbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public void CreatePerson_ReturnsCreatedPerson()
    {
        // Arrange
        Faker v_Faker = new();
        using ApplicationDbContext v_DbContext = new(m_DbContextOptions);
        PersonController v_Controller = new(v_DbContext);
        Person v_PersonData = new()
        {
            FirstName = v_Faker.Name.FirstName(),
            LastName = v_Faker.Name.LastName(),
            Email = v_Faker.Internet.Email(),
            Address = v_Faker.Address.StreetAddress(),
            City = v_Faker.Address.City()
        };

        // Act
        ActionResult<Person> v_Result = v_Controller.CreatePerson(v_PersonData);

        // Assert
        CreatedAtActionResult v_CreatedAtActionResult = Assert.IsType<CreatedAtActionResult>(v_Result.Result);
        Person v_CreatedPerson = Assert.IsType<Person>(v_CreatedAtActionResult.Value);
        Assert.Equal(v_PersonData.FirstName, v_CreatedPerson.FirstName);
        Assert.Equal(v_PersonData.LastName, v_CreatedPerson.LastName);
        Assert.Equal(v_PersonData.Email, v_CreatedPerson.Email);
        Assert.Equal(v_PersonData.Address, v_CreatedPerson.Address);
        Assert.Equal(v_PersonData.City, v_CreatedPerson.City);
    }

    [Fact]
    public void UpdatePerson_CorrectlyUpdatesPerson()
    {
        // Arrange
        Faker v_Faker = new();
        using ApplicationDbContext v_DbContext = new(m_DbContextOptions);
        PersonController v_Controller = new(v_DbContext);

        // Create the person
        Person v_PersonData = new()
        {
            FirstName = v_Faker.Name.FirstName(),
            LastName = v_Faker.Name.LastName(),
            Email = v_Faker.Internet.Email(),
            Address = v_Faker.Address.FullAddress(),
            City = v_Faker.Address.City()
        };

        // Create a new person
        ActionResult<Person> v_CreateResult = v_Controller.CreatePerson(v_PersonData);
        CreatedAtActionResult v_CreatedAtActionResult = Assert.IsType<CreatedAtActionResult>(v_CreateResult.Result);
        Person v_CreatedPerson = Assert.IsType<Person>(v_CreatedAtActionResult.Value);

        // Update the person
        Person v_UpdatedPersonData = new()
        {
            Id = v_CreatedPerson.Id,
            FirstName = v_Faker.Name.FirstName(),
            LastName = v_Faker.Name.LastName(),
            Email = v_Faker.Internet.Email(),
            Address = v_Faker.Address.FullAddress(),
            City = v_Faker.Address.City()
        };

        // Act
        ActionResult v_UpdateResult = v_Controller.UpdatePerson(v_UpdatedPersonData.Id, v_UpdatedPersonData);

        // Assert
        NoContentResult v_NoContentResult = Assert.IsType<NoContentResult>(v_UpdateResult);
    }
}