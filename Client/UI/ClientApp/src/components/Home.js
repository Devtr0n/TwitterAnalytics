import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Hello, and welcome!</h1>
                <p>Welcome to to the top ten twitter hashtags coding challenge, built with:</p>
                <ul>
                    <li><a href='https://dotnet.microsoft.com/en-us/download/dotnet/6.0'>.NET 6</a> an open source developer platform, created by Microsoft, for building many different types of applications.</li>
                    <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                    <li><a href='https://github.com/linvi/tweetinvi'>TweetInvi</a> an intuitive .NET C# library to access the Twitter REST API.</li>
                </ul>
                <p>Please feel free to get in touch with me (Richard Hollon) at my website <a href="https://www.richardhollon.com" target="_blank" rel="noreferrer">my website.</a></p>
            </div>
        );
    }
}
