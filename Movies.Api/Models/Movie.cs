namespace Movies.Api.Models;

public class Movie
{
    public int Id { get; init; }
    public string? Title { get; init; }    
    public DateTime ReleaseDate { get; init; }
    public string? Synopsis { get; init; }
}