using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookLibraryAPI.Data;
using BookLibraryAPI.DTOs;
using BookLibraryAPI.Models;

namespace BookLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AuthorsController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto dto)
    {
        var author = new Author { Name = dto.Name };
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAuthorBooks), new { id = author.Id }, author);
    }

    [HttpGet("{id}/books")]
    public async Task<IActionResult> GetAuthorBooks(int id)
    {
        var author = await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        if (author == null) return NotFound();
        return Ok(author.Books);
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _db.Authors.ToListAsync();
        return Ok(authors);
    }
}
