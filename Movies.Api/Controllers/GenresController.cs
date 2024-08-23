﻿using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models;
using Movies.Api.Repositories;
using Movies.Api.Services;

namespace Movies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController(IGenreRepository repository, IBatchGenreService batchService)
    : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await repository.GetAll());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var genre = await repository.Get(id);
        
        return genre == null
            ? NotFound()
            : Ok(genre);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] Genre genre)
    {
        var createdGenre = await repository.Create(genre);

        return CreatedAtAction(nameof(Get), new { id = createdGenre.Id }, createdGenre);
    }
    
    [HttpPost("batch")]
    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAll([FromBody] List<Genre> genres)
    {
        var response = await batchService.CreateGenres(genres);

        return CreatedAtAction(nameof(GetAll), new{}, response);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Genre genre)
    {
        var updatedGenre = await repository.Update(id, genre);

        return updatedGenre is null
            ? NotFound()
            :Ok(updatedGenre);
    }
    
    [HttpGet("from-query")]
    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFromQuery()
    {
        return Ok(await repository.GetAllFromQuery());
    }
    
    [HttpGet("names")]
    [ProducesResponseType(typeof(IEnumerable<GenreName>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNames()
    {
        return Ok(await repository.GetNames());
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var success = await repository.Delete(id);
        
        return success ? Ok() : NotFound();
    }
}