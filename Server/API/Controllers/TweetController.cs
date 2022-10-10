using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// API Controller for fetching top ten tweets by LINQ method and algorithm method and total tweets processed count
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class TweetController : ControllerBase
{
    private readonly ITopTenService _service;

    /// <summary>
    /// Constructor for Tweet Controller injects the "top ten" service
    /// </summary>
    /// <param name="service"></param>
    public TweetController(ITopTenService service) => _service = service;

    /// <summary>
    /// Retrieves top ten hashtags using LINQ approach
    /// </summary>
    /// <response code="200">Returns 'ok' when the top ten hashtags are found</response>
    /// <response code="204">Returns 'no content' when top ten hashtags are not found</response>
    /// <response code="500">Returns 'internal server error' when an error occurs</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Models.TweetHashtag>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("[action]")]
    public IActionResult TopTen()
    {
        var hashtags = _service.TopTenWithLinq();

        if (hashtags is not null)
            return Ok(hashtags);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Retrieves top ten hashtags using algorithm approach
    /// </summary>
    /// <response code="200">Returns 'ok' when the top ten hashtags are found</response>
    /// <response code="204">Returns 'no content' when top ten hashtags are not found</response>
    /// <response code="500">Returns 'internal server error' when an error occurs</response>
    [HttpGet("[action]")]
    public IActionResult TopTenAlgorithm()
    {
        var hashtags = _service.TopKFrequent();

        if (hashtags is not null)
            return Ok(hashtags);

        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    /// Retrieves a count of total tweets processed by the server
    /// </summary>
    /// <response code="200">Returns 'ok' for total tweets counted</response>
    /// <response code="500">Returns 'internal server error' when an error occurs</response>
    [HttpGet("[action]")]
    public IActionResult TotalCount() => Ok(_service.TotalTweetCount());
}