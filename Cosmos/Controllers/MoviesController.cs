using CosmosData;
using CosmosModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CosmosControllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : Controller
{
    private readonly MoviesContext _context;

    public MoviesController(MoviesContext context)
    {
        _context = context;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _context.Movies
            .SingleOrDefaultAsync(m => m.Id == id);

        foreach (var role in movie.Characters)
        {
            var actor = _context.Actors.Find(role.ActorId);
            role.Actor = actor;
        }
        
        return movie == null
            ? NotFound()
            : Ok(movie);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }
}