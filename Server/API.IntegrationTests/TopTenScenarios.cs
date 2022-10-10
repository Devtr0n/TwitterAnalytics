using API.Models;
using API.Worker;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.IntegrationTests;

public class TopTenScenarios : TestScenarioBase
{
    //[Fact]
    //public async void SwaggerTest()
    //{
    //    var response = await Client.GetAsync($"api/swagger");
    //    response.IsSuccessStatusCode.Should().BeTrue();
    //}

    [Theory]
    [InlineData("api/Tweet/TopTen")]
    [InlineData("api/Tweet/TopTenAlgorithm")]
    public async void TopTenHashtagTest(string url)
    {
        // Arrange - invoke the background service, wait, then cancel it
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        var tweetWorker = Server.Services.GetService<TweetWorker>();
        if (tweetWorker is not null)
        {
            await tweetWorker.StartAsync(token);
            // delay 15 seconds while invoking the worker methods under the hood
            await Task.Delay(20000, token);
            cts.Cancel();
        }

        // Act
        var response = await Client.GetAsync(url);
        var responseContent = response.Content.ReadAsStringAsync();
        var results = JsonConvert.DeserializeObject<IEnumerable<TweetHashtag>>(responseContent.Result);
        var orderedCounts = results.Select(r => r.Count);

        // Assert - verify we have a valid 'top ten hashtag' response
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNullOrEmpty();
        results.Count().Should().Be(10);
        orderedCounts.Should().BeInDescendingOrder();
    }
}