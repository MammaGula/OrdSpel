using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrdSpel.API.Controllers;
using OrdSpel.API.Services;
using OrdSpel.Shared.AuthDTOs;
using Xunit;

namespace OrdSpel.API.Test
{
    public class RegisterTests
    {
        private IConfiguration FakeConfig()
        {
            return new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "12345678901234567890123456789012" },
                    { "Jwt:Issuer", "test" },
                    { "Jwt:Audience", "test" }
                })
                .Build();
        }

        // Test 1: Lyckat register returnerar OK med token
        [Fact]
        public async Task Register_WithValidData_ReturnsOk()
        {
            var controller = new AuthController(
                new MockAuthService(),
                new JwtService(FakeConfig())
            );

            var result = await controller.Register(new RegisterDto
            {
                Username = "testuser",
                Password = "Test123!",
                ConfirmPassword = "Test123!"
            });

            Assert.IsType<OkObjectResult>(result);
        }

        // Test 2: Register med felaktiga uppgifter returnerar BadRequest
        [Fact]
        public async Task Register_WithInvalidData_ReturnsBadRequest()
        {
            var controller = new AuthController(
                new MockAuthService(),
                new JwtService(FakeConfig())
            );

            var result = await controller.Register(new RegisterDto
            {
                Username = "okändanvändare",
                Password = "FelLösen!",
                ConfirmPassword = "FelLösen!"
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
