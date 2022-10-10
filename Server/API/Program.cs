using API.Interfaces;
using API.Services;
using API.Worker;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tweetinvi;
using Tweetinvi.Models;

// WebApplication middleware builder
var builder = WebApplication.CreateBuilder(args);

// Configuration file settings
var configuration = new ConfigurationBuilder().AddJsonFile($"appSettings.json", true, true);

// Background Service
builder.Services.AddHostedService<TweetWorker>();
builder.Services.AddSingleton<TweetWorker>();
builder.Services.AddSingleton<TweetProcessor>();

// Service layer
builder.Services.AddSingleton<ITopTenService, TopTenService>();
builder.Services.AddSingleton(sp =>
{
    // Twitter credentials
    var config = configuration.Build();
    var consumerKey = config["Twitter:ConsumerKey"];
    var consumerSecret = config["Twitter:ConsumerSecret"];
    var bearerToken = config["Twitter:BearerToken"];
    var userCredentials = new TwitterCredentials(consumerKey, consumerSecret, bearerToken);
    return new TwitterClient(userCredentials);
});

// Routing
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(swag =>
{
    swag.SwaggerDoc("v1", new OpenApiInfo
    {
        Contact = new OpenApiContact() { Name = "Richard Hollon", Email = "richardsmailbox@gmail.com", Url = new Uri("https://www.richardhollon.com") },
        Description = "Coding challenge for Top Ten Hashtags from Twitter Dev API stream",
        Title = "TwitterAnalytics",
        Version = "v1"
    });

    // set the comments path for the Swagger JSON and UI
    swag.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

// Configure the HTTP request pipeline
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(builder => builder
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

/// <summary>
/// This is a bit of a hack for integration testing purposes, this helps expose the middleware to the test project
/// as recommended by Microsoft for .NET 6 integration test projects
/// </summary>
public partial class Program
{ }