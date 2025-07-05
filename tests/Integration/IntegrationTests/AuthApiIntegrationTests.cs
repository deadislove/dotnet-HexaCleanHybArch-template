using Adapters.Auth.Application.DTOs.RequestModel;
using Adapters.User.Application.DTOs;
using FluentAssertions;
using HexaCleanHybArch.Template.Tests.Integration.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HexaCleanHybArch.Template.Tests.Integration.IntegrationTests
{
    [Collection("SharedApiFactory")]
    public class AuthApiIntegrationTests
    {
        private readonly ModularApiFactory _factory;
        private readonly HttpClient _client;

        public AuthApiIntegrationTests(ModularApiFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        internal readonly UserDto _testUser = TestUsers.TestUser;
        internal readonly RegisterDto _register = TestUsers.RegisterDto;

        [Fact]
        public async Task Full_Auth_Flow_Should_Succeed()
        {
            // 1. Register
            _register.Email = TestDataGenerator.GenerateRandomEmail();

            HttpResponseMessage? registerResponse = await _client.PostAsJsonAsync("/api/auth/register", _register);
            registerResponse.EnsureSuccessStatusCode();

            // 2. Login
            LoginDto loginDto = new LoginDto { 
                Email = _register.Email,
                Password = _register.Password,
            };

            HttpResponseMessage? loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            loginResponse.EnsureSuccessStatusCode();

            string loginJson = await loginResponse.Content.ReadAsStringAsync();
            JsonObject? root = JsonNode.Parse(loginJson)?.AsObject();
            string? refreshToken = root?["data"]?["refreshToken"]?.GetValue<string>();

            // 3. Refresh
            RefreshTokenDto refreshTokenDto = new RefreshTokenDto
            {
                RefreshToken = refreshToken ?? string.Empty
            };

            HttpResponseMessage? refreshResponse = await _client.PostAsJsonAsync("/api/auth/RefreshToken", refreshTokenDto);
            refreshResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
