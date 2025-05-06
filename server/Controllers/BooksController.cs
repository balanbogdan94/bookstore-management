using BookstoreApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", Price = 12.99M, Year = 1925, Genre = "Classic" },
        new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", Price = 14.99M, Year = 1960, Genre = "Fiction" },
        new Book { Id = 3, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", Price = 11.99M, Year = 1949, Genre = "Dystopian" }
    };
    private static int _nextId = 4;

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks()
    {
        return _books;
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public ActionResult<Book> CreateBook(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var existingBook = _books.FirstOrDefault(b => b.Id == id);
        if (existingBook == null)
        {
            return NotFound();
        }

        var index = _books.IndexOf(existingBook);
        _books[index] = book;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        _books.Remove(book);
        return NoContent();
    }
}