namespace BookLibraryAPI.DTOs;

public record BookDto(string Title, string? Description, int AuthorId, DateTime? PublishedAt);
