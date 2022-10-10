using API.Interfaces;
using API.Models;
using API.Worker;

namespace API.Services
{
    /// <summary>
    /// Service layer for fetching tweet details from the background worker service
    /// </summary>
    public class TopTenService : ITopTenService
    {
        /// <summary>
        /// Top K Frequency - algorithm method for top ten hashtags
        /// </summary>
        /// <returns>Top Ten Hashtags</returns>
        public IEnumerable<TweetHashtag>? TopKFrequent()
        {
            // create frequency map for each string using a dictionary
            var frequencyMap = new Dictionary<string, int>();
            foreach (var word in TweetProcessor.Hashtags)
            {
                if (frequencyMap.ContainsKey(word))
                    frequencyMap[word]++;
                else
                    frequencyMap.Add(word, 1);
            }
            // convert to 'KeyValuePair' list, sorting by frequency then by alphabetical order
            var results = frequencyMap.ToList();
            results.Sort((p1, p2) =>
            {
                if (p2.Value - p1.Value == 0)
                    return string.Compare(p1.Key, p2.Key);

                return p2.Value - p1.Value;
            });
            // add first k (10) elements to (answer) list
            var answer = new List<TweetHashtag>();
            int i = 0;
            foreach (var result in results)
            {
                answer.Add(new TweetHashtag { Count = result.Value, Hashtag = result.Key });
                if (++i == 10)
                    return answer;
            }
            return null;
        }

        /// <summary>
        /// Top Ten With Linq - Linq method for top ten hashtags
        /// </summary>
        /// <returns>Top Ten Hashtags</returns>
        public IEnumerable<TweetHashtag>? TopTenWithLinq()
        {
            // group by hashtag, ordering by count then hashtag alphabetically, taking the top ten
            var hashtags = TweetProcessor.Hashtags
                .GroupBy(tweet => tweet)
                .Select(tweet => new TweetHashtag { Count = tweet.Count(), Hashtag = tweet.Key })
                .OrderByDescending(topten => topten.Count)
                .ThenBy(topten => topten.Hashtag)
                .Take(10)
                .ToList()
                .AsEnumerable();

            // determine whether any hashtags are present
            if (hashtags.Any())
                return hashtags;

            // default to null
            return null;
        }

        /// <summary>
        /// Total Tweet Count - a count of all tweets processed by the background service
        /// </summary>
        /// <returns>Total Tweet Count</returns>
        public int TotalTweetCount() =>
            // get the tweet count from the background service
            TweetProcessor.TweetCounter;
    }
}