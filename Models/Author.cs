using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models;

public class Author
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
