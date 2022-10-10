using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace API.UnitTests
{
    public abstract class BaseUnitTest : IDisposable
    {
        protected BaseUnitTest()
        {
            // called before every test method, set a new database name to avoid data collisions
            //Context = new TwitterAnalyticsDbContext(new DbContextOptionsBuilder<TwitterAnalyticsDbContext>()
            //    // generate a generic in-memory sql database (for testing only), based on the database model & context
            //    .UseInMemoryDatabase(databaseName: new Guid().ToString()).Options);
        }

        // load the local configuration settings json file
        protected static IConfiguration Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.unittest.json")
            .Build();

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}