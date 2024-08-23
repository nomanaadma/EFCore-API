namespace CosmosModels;

public class Movie
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string Synopsis { get; set; }

    public required Genre Genre { get; set; }
    
    public List<Role> Characters { get; set; } = new();
}