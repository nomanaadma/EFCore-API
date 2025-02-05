using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Models;

namespace Movies.Api.Repositories;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAll();
    Task<IEnumerable<Genre>> GetAll(IEnumerable<int> genreIds);
    Task<Genre?> Get(int id);
    Task<Genre> Create(Genre genre);
    Task<Genre?> Update(int id, Genre genre);
    Task<bool> Delete(int id);
    Task<IEnumerable<Genre>> GetAllFromQuery();
    Task<IEnumerable<GenreName>> GetNames();
}

public class GenreRepository: IGenreRepository
{
    private readonly MoviesContext _context;
    private readonly IUnitOfWorkManager _uowManager;

    public GenreRepository(MoviesContext context, IUnitOfWorkManager uowManager)
    {
        _context = context;
        _uowManager = uowManager;
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _context.Genres.ToListAsync();
    }
    
    public async Task<IEnumerable<Genre>> GetAll(IEnumerable<int> genreIds)
    {
        return await _context.Genres
            .Where(genre => genreIds.Contains(genre.Id))
            .ToListAsync();
    }

    public async Task<Genre?> Get(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<Genre> Create(Genre genre)
    {
        await _context.Genres.AddAsync(genre);

        if(!_uowManager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return genre;
    }

    public async Task<Genre?> Update(int id, Genre genre)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return null;

        existingGenre.Name = genre.Name;

        if(!_uowManager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return existingGenre;
    }

    public async Task<bool> Delete(int id)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return false;

        _context.Genres.Remove(existingGenre);

        if(!_uowManager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Genre>> GetAllFromQuery()
    {
        const int minimumId = 2;

        var genres = await _context.Genres
            .FromSql($"SELECT * FROM [dbo].[genres] WHERE ID >= {minimumId}")
            .Where(genre => genre.Name != "Comedy")
            .ToListAsync();

        return genres;

    }
    
    public async Task<IEnumerable<GenreName>> GetNames()
    {
        var names = await _context.Database
            .SqlQuery<GenreName>($@"SELECT Name FROM dbo.Genres")
            .ToListAsync();

        return names;
    }
    
    
}