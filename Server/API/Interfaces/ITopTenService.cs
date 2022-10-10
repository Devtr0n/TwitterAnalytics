using API.Models;

namespace API.Interfaces
{
    /// <summary>
    /// Top Ten Service contract definition
    /// </summary>
    public interface ITopTenService
    {
        /// <summary>
        /// Top K Frequency - algorithm method for top ten hashtags
        /// </summary>
        /// <returns>Top Ten Hashtags</returns>
        IEnumerable<TweetHashtag>? TopKFrequent();

        /// <summary>
        /// Top Ten With Linq - Linq method for top ten hashtags
        /// </summary>
        /// <returns>Top Ten Hashtags</returns>
        IEnumerable<TweetHashtag>? TopTenWithLinq();

        /// <summary>
        /// Total Tweet Count - a count of all tweets processed by the background service
        /// </summary>
        /// <returns>Total Tweet Count</returns>
        int TotalTweetCount();
    }
}