using MariaDB.Data;
using MariaDB.Models;
using Microsoft.EntityFrameworkCore;

namespace MariaDB.Repositories;

public interface IGenreRepository
{
    Task<IEnumerable<Genre>> GetAll();
    Task<IEnumerable<Genre>> GetAll(IEnumerable<int> genreIds);
    Task<Genre?> Get(int id);
    Task<Genre> Create(Genre genre);
    Task<Genre?> Update(int id, Genre genre);
    Task<bool> Delete(int id);
    Task<IEnumerable<Genre>> GetAllFromQuery();
}

public class GenreRepository: IGenreRepository
{
    private readonly IMoviesContextManager _manager;
    private readonly MoviesContext _context;

    public GenreRepository(IMoviesContextManager manager)
    {
        _manager = manager;
        _context = _manager.GetContext();
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
        
        if(!_manager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return genre;
    }

    public async Task<Genre?> Update(int id, Genre genre)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return null;

        existingGenre.Description = genre.Description;

        if(!_manager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();

        return existingGenre;
    }

    public async Task<bool> Delete(int id)
    {
        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre is null)
            return false;

        _context.Genres.Remove(existingGenre);
        
        if(!_manager.IsUnitOfWorkStarted)
            await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<Genre>> GetAllFromQuery()
    {
        var minimumGenreId = 2;

        var genres = await _context.Genres
            .FromSql($"SELECT * FROM [dbo].[Genres] WHERE ID >= {minimumGenreId}")
            .Where(genre => genre.Name != "Comedy")
            .ToListAsync();

        return genres;
    }
}