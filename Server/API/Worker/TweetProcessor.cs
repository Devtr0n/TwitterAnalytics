using System.Text.RegularExpressions;
using Tweetinvi;

namespace API.Worker
{
    /// <summary>
    /// Processes tweets from the sample stream in Twitter Dev API
    /// </summary>
    public sealed class TweetProcessor
    {        
        private readonly ILogger<TweetWorker> _logger;
        private static readonly Regex _regex = new(@"^[\P{L}\p{IsBasicLatin}]+$");
        private readonly TwitterClient _twitterClient;

        /// <summary>
        /// Hashtag list for all processed hashtags
        /// </summary>
        public static List<string> Hashtags = new();

        /// <summary>
        /// Counter for total tweets processed
        /// </summary>
        public static int TweetCounter;

        /// <summary>
        /// Constructor for the tweet processor, including the console logger and twitter client used for Twitter Dev API access
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="twitterClient"></param>
        public TweetProcessor(ILogger<TweetWorker> logger, TwitterClient twitterClient)
        {
            _logger = logger;
            _twitterClient = twitterClient;
        }

        /// <summary>
        /// Asynchronous method for processing streamed tweets into a collection
        /// </summary>
        /// <returns></returns>
        public async Task ProcessTweets()
        {
            // call the twitter API stream
            var sampleStreamV2 = _twitterClient.StreamsV2.CreateSampleStream();
            sampleStreamV2.TweetReceived += (sender, args) =>
            {
                // increment the total counter
                TweetCounter++;

                // find any hashtags in the current "tweet"
                var hashTags = args.Tweet?.Entities?.Hashtags;

                // when hashtags are found in the "tweet", let's process them into the stack
                if (hashTags is not null && args.Tweet?.Lang == "en")
                {
                    // fetch all hashtags found in the "tweet"
                    var tags = hashTags.Select(h => h.Tag);

                    // determine hashtags are found
                    if (tags.Any())
                    {
                        // process each hashtag into the list
                        foreach (var tag in tags)
                        {
                            // determine whether hashtag is English language via regex, for cleaner analysis (just for giggles)
                            if (_regex.Match(tag).Success)
                            {
                                _logger.LogInformation($"Found hashtag: {tag} with tweet count {TweetCounter}");
                                Hashtags.Add(tag);
                            }
                        }
                    }
                }
            };

            await sampleStreamV2.StartAsync();
        }
    }
}
