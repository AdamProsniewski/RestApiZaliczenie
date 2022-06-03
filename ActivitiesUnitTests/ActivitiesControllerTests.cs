using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using RestApiZaliczenie;
using RestApiZaliczenie.Controllers;
using RestApiZaliczenie.Data;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ActivitiesUnitTests
{
    public class ActivitiesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;
        public ActivitiesControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetActivity()
        {
            
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Activities");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}
