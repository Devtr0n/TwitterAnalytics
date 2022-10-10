using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Tweetinvi;
using Tweetinvi.Models;
using Xunit;

namespace API.IntegrationTests
{
    [CollectionDefinition("IntegrationTest")]
    public abstract class TestScenarioBase
    {
        // create a "client" for the "test" server
        protected readonly HttpClient Client = Server.CreateClient();

        // create a "test" server to run integration tests against
        protected static readonly WebApplicationFactory<Program> Server = CreateServer();

        private static WebApplicationFactory<Program> CreateServer()
        {
            // fetch configuration settings for base address uri with port number
            var integrationTestConfig = new ConfigurationBuilder().AddJsonFile($"appSettings.test.json", true, true).Build();
            var baseAddress = new Uri(integrationTestConfig["IntegrationTestServer:BaseAddress"]);

            // use the API configuration file in a different project
            var apiConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            // fetch the Twitter credentials from API project configuration
            var bearerToken = apiConfig["Twitter:BearerToken"];
            var consumerKey = apiConfig["Twitter:ConsumerKey"];
            var consumerSecret = apiConfig["Twitter:ConsumerSecret"];
            var userCredentials = new TwitterCredentials(consumerKey, consumerSecret, bearerToken);

            // create a test web application from factory based on the API entry point & middleware in a different project (API)
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // set the "test server" uri with port number
                    builder.UseTestServer(testServer =>
                    {
                        testServer.BaseAddress = baseAddress;
                    });

                    // define the middleware for test services
                    builder.ConfigureTestServices(x => x.AddSingleton(sp => { return new TwitterClient(userCredentials); }));
                });

            // instruct the client to use the uri with port number
            application.ClientOptions.BaseAddress = baseAddress;

            return application;
        }
    }
}