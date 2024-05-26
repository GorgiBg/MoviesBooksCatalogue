using Microsoft.AspNetCore.Mvc;
using MoviesBooksCatalogue.Controllers;
using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;
using Moq;

namespace UnitTests

{
    public class BooksControllerTests
    {
        [Fact]
        public async Task Get_ReturnsAllBooks()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 },
                new Book { Id = 2, Title = "Book 2", Genre = "Non-Fiction", ReleaseDate = new DateTime(2021, 3, 15), Rating = 4.8 }
            };
            mockBookService.Setup(service => service.GetAllBooks()).ReturnsAsync(expectedBooks);
            var controller = new BooksController(mockBookService.Object);

            // Act
            var result = await controller.Get() as ObjectResult;
            var books = result.Value as IEnumerable<Book>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedBooks, books);
        }

        [Fact]
        public async Task Get_ReturnsBookById()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var expectedBook = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockBookService.Setup(service => service.GetBookById(1)).ReturnsAsync(expectedBook);
            var controller = new BooksController(mockBookService.Object);

            // Act
            var result = await controller.Get(1) as ObjectResult;
            var book = result.Value as Book;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedBook, book);
        }

        [Fact]
        public async Task Post_CreatesNewBook()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var newBook = new Book { Id = 1, Title = "Book 1", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            var controller = new BooksController(mockBookService.Object);

            // Act
            var result = await controller.Post(newBook) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("Get", result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);
            Assert.Equal(newBook, result.Value);
        }

        [Fact]
        public async Task Put_UpdatesBook()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var updatedBook = new Book { Id = 1, Title = "Updated Book", Genre = "Fiction", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            var controller = new BooksController(mockBookService.Object);

            // Act
            var result = await controller.Put(1, updatedBook) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task Delete_RemovesBook()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var controller = new BooksController(mockBookService.Object);

            // Act
            var result = await controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }
    }
}
