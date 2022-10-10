![Twitter-Analytics](https://i.ibb.co/MG3CQfC/twitter-analytics.png)

## _A coding challenge to find the top ten hashtags from a Twitter tweet stream_

TwitterAnalytics is a .NET 6 solution written in C# and React.js. 

The TwitterAnalytics solution consists of the following:
- Background Service (Async)
- API (C# WebAPI)
- UI/Client (React.js)
- Unit Tests (xUnit)
- Integration Tests (xUnit)
- SOLID design principles
- DRY principle
- Headless architecture
- Test Driven Development

The Twitter Dev API credentials are stored in the API appsettings.json configuration file, like so:

```
  "Twitter": {
    "ConsumerKey": "your-consumer-key-here",
    "ConsumerSecret": "your-consumer-secret-here",
    "BearerToken": "your-bearer-token-here"
  }
```
To obtain the consumer key/secret and bearer token, you must first register your twitter account and phone number at Twitter Dev API @ https://developer.twitter.com/en/docs/twitter-api/getting-started/getting-access-to-the-twitter-api 

## Installation for UI

TwitterAnalytics requires [Node.js](https://nodejs.org/) v16+ to run the Client UI
I used [chocolately to install nodejs](https://community.chocolatey.org/packages/nodejs) for this project, like so:

```
choco install nodejs
```

Install the package dependencies via npm. 
From the solution root directory:

```
cd Client\UI\ClientApp
npm i
npm install react-scripts --save
```

I had some trouble getting the "ClientApp" to build and start, even after it was working perfectly fine. What fixed it for me was to update to the latest react-scripts package. This may or may not help you.

```
npm install react-scripts@latest
```
## Running the API & Background Service (Server)

The solution is configured to execute the API server as the startup project.
The easiest way to launch the API server is to use the .NET CLI.
From the solution root directory, you can build the solution using the command:

```
dotnet build
```

![DotNetBuildCLI](https://i.ibb.co/rcxptZT/dot-net-build.png)

From the API directory, you can run the API and server process using the commands:

```
cd Server/API
dotnet run
```

You should similar output as shown here
![DotNetBuildCLI](https://i.ibb.co/LkZLw6s/dot-net-run.png)

Alternatively, you can use Visual Studio IDE to launch the API and server.
- Open the .sln in Visual Studio 2022
- Press F5/Run
- You should see a console window open and begin logging hashtag debug info from the Background Service Worker task
![BackgroundService](https://i.ibb.co/02vZ7X2/API-worker-tweet-processor.png)
- You should also see the SwaggerUI open in your default browser at https://localhost:7044/swagger/index.html
![SwaggerUI](https://i.ibb.co/4RJbztZ/API-swagger.png)
- You should now be able to manually invoke the API via Swagger (the "Try It Out" buttons) and see top ten hashtag data in the responses
![Execute-Swagger](https://i.ibb.co/txrPXv1/API-swagger-top-ten-hashtags.png)

Alternatively to Swagger, you can also use PowerShell and curl to invoke the API endpoints, like so:
```
curl "https://localhost:7044/api/Tweet/TopTen" | Select-Object -Expand Content
curl "https://localhost:7044/api/Tweet/TopTenAlgorithm" | Select-Object -Expand Content
curl "https://localhost:7044/api/Tweet/TotalCount"
```

Now the client is ready to fire up and start running

## Running the ReactJS UI (Client)

```
cd Client\UI\ClientApp
npm run build
npm start
```

- Verify the UI deployment by navigating to the localhost address in your preferred browser. You should now see the UI/webpage load after going to the address:

```sh
https://localhost:44464/
```

- The UI has 4 navigation menu has options
    - Home - a brief description of the website, technology stack and contact information
     ![Homepage](https://i.ibb.co/NrYjybj/UI-website-homepage-index.png)
    - Top Ten Hashtags (LINQ) - a table consisting of the top ten hashtags ordered by count and tag using the LINQ method
     ![TopTenHashtags](https://i.ibb.co/7SZqZZW/UI-website-top-ten-hashtags.png)
    - Top Ten Hashtags (Algorithm) - a table consisting of the top ten hashtags ordered by count and tag using the algorithm method
     ![TopTenHashtagsAlgorithm](https://i.ibb.co/jbZt9kP/UI-website-top-ten-algorithm-hashtags.png)
    - Total Tweet Count - a table consisting of the total number of tweets processed by the server
     ![TotalTweetCount](https://i.ibb.co/09k0hkY/UI-website-total-tweet-count.png)

## Testing
There are unit tests and API integration tests written in [xUnit] for proof of concept. The easiest way to run the tests is from the CLI. From within the solution root directory simply invoke the following testrunner command:

```sh
dotnet test
```

![CLI-Testing](https://i.ibb.co/zJnBxH6/cli-test-runner.png)


## Features

- Top Ten hashtags from Twitter Dev API streaming in real time (analytics)
- Total Tweets processed counter in real time (analytics)
- REST API with Swagger UI support
- ReactJS front-end client with mobile browser support
- API Unit and Integration testing to verify expected results (SOLID architecture)
- Bootstrap 4 for responsive layout
- .NET 6 for the latest Microsoft development platform

## Tech Stack

TwitterAnalytics uses a number of tech stack and open source projects to work properly:

- [.NET 6] - an open source developer platform, created by Microsoft, for building many different types of applications.</
- [C#] - a modern, object-oriented, and type-safe programming language.
- [ASP.NET Web API] - secure REST APIs on any platform with C#
- [ReactJS] - a JavaScript library for building user interfaces
- [Twitter Bootstrap] - great UI boilerplate for modern web apps
- [node.js] - an open-source, cross-platform JavaScript runtime environment.
- [xUnit] - a free, open source, community-focused unit testing tool for the .NET Framework. 
- [Devtr0n] - my personal github profile (blood, sweat and tears!)

And of course TwitterAnalytics itself is open source with a [public repository][Devtr0nRepo] on GitHub.

## Docker

I will add Docker support to TwitterAnalytics soon, as I ran out of time to do so. I have been rather busy lately with closing on our house, organizing a move into said new house, a 7 month old daughter and supporting my lovely wife of nearly 4 years, all while transitioning between homes and working a fulltime position as a .NET Lead over ASP.NET greenfield projects in the automotive warranty and claims industry.


## License

MIT

**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   
   [Devtr0n]: <https://github.com/Devtr0n>
   [Devtr0nRepo]: <https://github.com/Devtr0n/TwitterAnalytics>
   [node.js]: <http://nodejs.org>
   [Twitter Bootstrap]: <http://twitter.github.com/bootstrap/>
   [.NET 6]: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0> 
   [xUnit]: <https://xunit.net/>
   [ASP.NET Web API]: <https://dotnet.microsoft.com/en-us/apps/aspnet/apis>
   [C#]: <https://learn.microsoft.com/en-us/dotnet/csharp/>
   [ReactJS]: <https://reactjs.org/>
