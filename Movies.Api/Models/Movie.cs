namespace Movies.Api.Models;

public class Movie
{
    public int Identifier { get; init; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Synopsis { get; set; }
}

public class MovieTitle
{
    public int Id { get; init; }
    
    public string? Title { get; init; }
}