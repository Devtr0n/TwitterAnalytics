namespace API.Worker;

/// <summary>
/// Background service for processing Twitter Dev API streamed tweets
/// </summary>
public sealed class TweetWorker : BackgroundService
{
    private readonly ILogger<TweetWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor for background service
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    public TweetWorker(ILogger<TweetWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Asynchronous task for tweet processing 
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Worker started running at {DateTimeOffset.Now}");

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var processor = scope.ServiceProvider.GetRequiredService<TweetProcessor>();
                await processor.ProcessTweets();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while processing tweet");
            }
        }
        await Task.Delay(1000, stoppingToken);
    }
}