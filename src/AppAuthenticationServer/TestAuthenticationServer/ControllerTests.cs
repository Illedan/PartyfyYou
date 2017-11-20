using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppAuthenticationServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace TestAuthenticationServer
{
    public class ControllerTests
    {
        [Fact]
        public async Task TokenNotRegisteredForUser_EmptyString()
        {
            using (var testServer = CreateTestServer())
            {
                var client = testServer.CreateClient();

                var token = await client.GetStringAsync("api/auth/guidenough");

                Assert.Equal("", token);
            }
        }

        [Fact]
        public async Task TokenRegisteredForUser_ReturnsToken()
        {
            using (var testServer = CreateTestServer())
            {
                var client = testServer.CreateClient();

                const string UserId = "userId";

                await client.PutAsync($"api/auth/{UserId}", new StringContent("\"token\"", Encoding.UTF8, "application/json"));
                var token = await client.GetStringAsync($"api/auth/{UserId}");

                Assert.Equal("token", token);

                token = await client.GetStringAsync($"api/auth/{UserId}");

                Assert.Equal("", token);
            }
        }

        // TODO: Test der cachen timer ut...

        TestServer CreateTestServer()
        {
            var builder = new WebHostBuilder()
            .UseStartup<Startup>();
            return new TestServer(builder);
        }
    }
}
