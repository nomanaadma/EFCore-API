using Movies.Api.Data;
using Movies.Api.Models;
using Movies.Api.Repositories;

namespace Movies.Api.Services;

public interface IBatchGenreService
{
    Task<IEnumerable<Genre>> CreateGenres(IEnumerable<Genre> genres);
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
}