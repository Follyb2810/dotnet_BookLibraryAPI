using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;
    public BooksController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDto dto)
    {
        var author = await _db.Authors.FindAsync(dto.AuthorId);
        if (author == null) return BadRequest("Author not found");

        var book = new Book
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = dto.AuthorId,
            PublishedAt = dto.PublishedAt ?? DateTime.UtcNow
        };

        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? author)
    {
        var query = _db.Books.Include(b => b.Author).AsQueryable();

        if (!string.IsNullOrWhiteSpace(author))
            query = query.Where(b => b.Author.Name.Contains(author));

        var books = await query.ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) return NotFound();
        return Ok(book);
    }
}
