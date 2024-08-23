using System.Text.Json.Serialization;

namespace Movies.Api.Models;

public class Genre
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	
	[JsonIgnore]
	public byte[] ConcurrencyToken { get; set; } = [];

	// [JsonIgnore]
	// public DateTime CreatedDate { get; set; }

	[JsonIgnore]
	public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
}