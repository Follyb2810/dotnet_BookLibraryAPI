using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibraryAPI.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
}
