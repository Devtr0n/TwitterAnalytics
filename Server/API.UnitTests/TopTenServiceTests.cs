using API.Models;
using API.Services;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace API.UnitTests;

public class TopTenServiceTests
{
    private readonly TopTenService _service;

    public TopTenServiceTests() => _service = new TopTenService();

    #region MemberData

    /// <summary>
    /// Mockup hashtag objects for unit testing purposes
    /// </summary>
    public static IEnumerable<object[]> HashtagMemberData
    {
        get
        {
            return new[]
            {
                new object[]
                {
                    new List<string>()
                    {
                        "testing", "testing", "testing",
                        "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test",
                        "python", "python",
                        "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test",
                        "robin", "robin", "robin",
                        "batman", "batman", "batman", "batman", "batman", "batman", "batman",
                        "batman", "batman", "batman", "batman", "batman", "batman", "batman",
                        "crypto", "crypto",
                        "octopus", "octopus", "octopus", "octopus", "octopus", "octopus",
                        "octopus", "octopus", "octopus", "octopus", "octopus", "octopus",
                        "seasnake", "seasnake",
                        "cactus", "cactus", "cactus", "cactus", "cactus", "cactus", "cactus", "cactus", "cactus", "cactus",
                        "scorpion", "scorpion", "scorpion", "scorpion", "scorpion", "scorpion", "scorpion", "scorpion",
                        "csharp", "csharp", "csharp",
                        "dragon", "dragon", "dragon", "dragon", "dragon", "dragon", "dragon", "dragon",
                        "blueberry", "blueberry", "blueberry", "blueberry", "blueberry", "blueberry", "blueberry",
                        "javascript", "javascript", "javascript",
                        "colorado", "colorado", "colorado", "colorado", "colorado", "colorado", "colorado",
                        "powershell", "powershell",
                        "dasvoldus", "dasvoldus", "dasvoldus", "dasvoldus", "dasvoldus",
                        "lamb", "lamb", "lamb", "lamb",
                        "tiger", "tiger",
                        "aboriginee", "aboriginee"
                    }
                }
            };
        }
    }

    #endregion MemberData

    /// <summary>
    /// Unit test for fetching top ten tweet hashtags using algorithm method
    /// </summary>
    /// <param name="hashtags"></param>
    [Theory]
    [MemberData(nameof(HashtagMemberData))]
    public async Task TopTenAlgorithmHashtagsTest(string[] hashtags)
    {
        // Arrange - see the 'member data' declarations above
        Worker.TweetProcessor.Hashtags = hashtags.ToList();

        // Act
        var result = _service.TopKFrequent();

        // Assert
        result?.Should().NotBeNullOrEmpty();
        result?.Should().AllBeOfType<TweetHashtag>();
        result?.Count().Should().Be(10);
        result?.First().Hashtag.Should().Be("test");
        result?.First().Count.Should().Be(22);
        result?.ElementAt(1).Hashtag.Should().Be("batman");
        result?.ElementAt(1).Count.Should().Be(14);
        result?.ElementAt(2).Hashtag.Should().Be("octopus");
        result?.ElementAt(2).Count.Should().Be(12);
        result?.ElementAt(3).Hashtag.Should().Be("cactus");
        result?.ElementAt(3).Count.Should().Be(10);
        result?.ElementAt(4).Hashtag.Should().Be("dragon");
        result?.ElementAt(4).Count.Should().Be(8);
        result?.ElementAt(5).Hashtag.Should().Be("scorpion");
        result?.ElementAt(5).Count.Should().Be(8);
        result?.ElementAt(6).Hashtag.Should().Be("blueberry");
        result?.ElementAt(6).Count.Should().Be(7);
        result?.ElementAt(7).Hashtag.Should().Be("colorado");
        result?.ElementAt(7).Count.Should().Be(7);
        result?.ElementAt(8).Hashtag.Should().Be("dasvoldus");
        result?.ElementAt(8).Count.Should().Be(5);
        result?.Last().Hashtag.Should().Be("lamb");
        result?.Last().Count.Should().Be(4);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Unit test for fetching top ten tweet hashtags using LINQ method
    /// </summary>
    /// <param name="hashtags"></param>
    [Theory]
    [MemberData(nameof(HashtagMemberData))]
    public async Task TopTenLinqHashtagsTest(List<string> hashtags)
    {
        // Arrange - see the 'member data' declarations above
        Worker.TweetProcessor.Hashtags = hashtags;

        // Act
        var result = _service.TopTenWithLinq();

        // Assert
        result?.Should().NotBeNullOrEmpty();
        result?.Should().AllBeOfType<TweetHashtag>();
        result?.Count().Should().Be(10);
        result?.First().Hashtag.Should().Be("test");
        result?.First().Count.Should().Be(22);
        result?.ElementAt(1).Hashtag.Should().Be("batman");
        result?.ElementAt(1).Count.Should().Be(14);
        result?.ElementAt(2).Hashtag.Should().Be("octopus");
        result?.ElementAt(2).Count.Should().Be(12);
        result?.ElementAt(3).Hashtag.Should().Be("cactus");
        result?.ElementAt(3).Count.Should().Be(10);
        result?.ElementAt(4).Hashtag.Should().Be("dragon");
        result?.ElementAt(4).Count.Should().Be(8);
        result?.ElementAt(5).Hashtag.Should().Be("scorpion");
        result?.ElementAt(5).Count.Should().Be(8);
        result?.ElementAt(6).Hashtag.Should().Be("blueberry");
        result?.ElementAt(6).Count.Should().Be(7);
        result?.ElementAt(7).Hashtag.Should().Be("colorado");
        result?.ElementAt(7).Count.Should().Be(7);
        result?.ElementAt(8).Hashtag.Should().Be("dasvoldus");
        result?.ElementAt(8).Count.Should().Be(5);
        result?.Last().Hashtag.Should().Be("lamb");
        result?.Last().Count.Should().Be(4);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Unit test for fetching total tweet count
    /// </summary>
    [Fact]
    public async Task GivenCountOfTenThousand_WhenTotalTweetCount_ThenTotalIsTenThousand()
    {
        // Arrange
        var count = 10000;
        Worker.TweetProcessor.TweetCounter = count;

        // Act
        var result = _service.TotalTweetCount();

        // Assert
        result.Should().Be(count);
        result.Should().NotBe(default);
        result.Should().BeOfType(typeof(int));
        await Task.CompletedTask;
    }
}