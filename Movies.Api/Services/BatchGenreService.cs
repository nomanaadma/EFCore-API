using Movies.Api.Data;
using Movies.Api.Models;
using Movies.Api.Repositories;

namespace Movies.Api.Services;

public interface IBatchGenreService
{
    Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres);
    Task<IEnumerable<Genre>> UpdateGenres(IEnumerable<Genre> genres);
    
}

public class BatchGenreService(IGenreRepository repository, IUnitOfWorkManager uowManager) 
    : IBatchGenreService
{
    public async Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = [];
        
        uowManager.StartUnitOfWork();
        
        foreach (var genre in genres)
        {
            response.Add(await repository.Create(genre));
        }

        await uowManager.SaveChangesAsync();
        
        return response;
    }
    
    public async Task<IEnumerable<Genre>> UpdateGenres(IEnumerable<Genre> genres)
    {
        List<Genre> response = [];
        uowManager.StartUnitOfWork();
        
        // for performance optimization 
        await repository.GetAll(genres.Select(g => g.Id));
        
        foreach (var genre in genres)
        {
            var updatedGenre = await repository.Update(genre.Id, genre);
            if(updatedGenre is not null)
                response.Add(updatedGenre);
        }
        await uowManager.SaveChangesAsync();
        return response;
    }
}