using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;
using Moq;
using MoviesBooksCatalogue.Data;

namespace UnitTests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Book>>();
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 },
                new Book { Id = 2, Title = "Book 2", Genre = "Non-Fiction", ReleaseDate = new DateTime(2021, 3, 15), Rating = 4.8 }
            };
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedBooks);
            var bookService = new BookService(mockRepository.Object);

            // Act
            var result = await bookService.GetAllBooks();

            // Assert
            Assert.Equal(expectedBooks, result);
        }

        [Fact]
        public async Task GetBookById_ReturnsBookWithGivenId()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Book>>();
            var expectedBook = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(expectedBook);
            var bookService = new BookService(mockRepository.Object);

            // Act
            var result = await bookService.GetBookById(1);

            // Assert
            Assert.Equal(expectedBook, result);
        }

        [Fact]
        public async Task AddBook_AddsNewBook()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Book>>();
            var bookToAdd = new Book { Title = "New Book", Genre = "Sci-Fi", ReleaseDate = new DateTime(2022, 5, 1), Rating = 4.2 };
            var bookService = new BookService(mockRepository.Object);

            // Act
            await bookService.AddBook(bookToAdd);

            // Assert
            mockRepository.Verify(repo => repo.Add(bookToAdd), Times.Once);
        }

        [Fact]
        public async Task UpdateBook_UpdatesExistingBook()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Book>>();
            var existingBook = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingBook);
            var bookToUpdate = new Book { Id = 1, Title = "Updated Book", Genre = "Sci-Fi", ReleaseDate = new DateTime(2022, 5, 1), Rating = 4.2 };
            var bookService = new BookService(mockRepository.Object);

            // Act
            await bookService.UpdateBook(bookToUpdate);

            // Assert
            mockRepository.Verify(repo => repo.Update(bookToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteBook_DeletesBookWithGivenId()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Book>>();
            var existingBook = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingBook);
            var bookService = new BookService(mockRepository.Object);

            // Act
            await bookService.DeleteBook(1);

            // Assert
            mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
