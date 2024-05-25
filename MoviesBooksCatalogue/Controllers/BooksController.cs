using Microsoft.AspNetCore.Mvc;
using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;

namespace MoviesBooksCatalogue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _bookService.GetAllBooks());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Book book)
    {
        await _bookService.AddBook(book);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        await _bookService.UpdateBook(book);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteBook(id);
        return NoContent();
    }
}