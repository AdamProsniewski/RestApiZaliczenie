using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using RestApiZaliczenie;
using RestApiZaliczenie.Controllers;
using RestApiZaliczenie.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static System.Uri;

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
        public async Task GetActivity_GoodRequest_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Activities");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task GetActivity_RequestNotFound_SuccessStatusCode()
        {
            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Aktywnosci");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostActivity_InvalidKey_BadRequestStatusCode()
        {
            using var client = _factory.CreateClient();

            var jsonString = "{\"nieistnieje\":\"TestPOSTAction\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await client.PostAsync("api/Activities", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [Fact]
        public async Task PostActivity_GoodRequest_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"name\":\"string2\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("/api/Activities", httpContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task PostActivity_Identity_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"id\":1,\"name\":\"string\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("/api/Activities", httpContent);

            // Assert
            var message = await client.PostAsync("api/Activities", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [Fact]
        public async Task PostActivity_WrongData_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"name\":2}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync("/api/Activities", httpContent);

            // Assert
            var message = await client.PostAsync("api/Activities", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }
        [Fact]
        public async void Task_Delete_WrongId()
        {
            using var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/Activities/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            using var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/Activities/last");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }





        [Fact]
        public async Task PutActivity_InvalidKey_BadRequestStatusCode()
        {
            using var client = _factory.CreateClient();

            var jsonString = "{\"id':14,\"nieIstnieje\":\"TestPutAction\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var message = await client.PutAsync("api/Activities/14", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [Fact]
        public async Task PutActivity_GoodRequest_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"id\":14,\"name\":\"string\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PutAsync("/api/Activities/14", httpContent);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }



        [Fact]
        public async Task PutActivity_WrongData_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"name\":2}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var message = await client.PutAsync("/api/Activities/14", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
        }

        [Fact]
        public async Task PutActivity_NotFound_SuccessStatusCode()
        {

            using var client = _factory.CreateClient();

            var jsonString = "{\"id\":9999,\"name\":\"string\"}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Act
            var message = await client.PutAsync("/api/Activities/9999", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, message.StatusCode);
        }

    }
}

