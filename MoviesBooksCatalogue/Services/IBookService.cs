using MoviesBooksCatalogue.Data;
using MoviesBooksCatalogue.Models;

namespace MoviesBooksCatalogue.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
    }

    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task AddBook(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.Update(book);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.Delete(id);
        }
    }
}