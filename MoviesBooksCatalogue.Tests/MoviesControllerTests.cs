using Microsoft.AspNetCore.Mvc;
using MoviesBooksCatalogue.Controllers;
using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;
using Moq;

namespace UnitTests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Get_ReturnsAllMovies()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var expectedMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 },
                new Movie { Id = 2, Title = "Movie 2", Genre = "Drama", ReleaseDate = new DateTime(2021, 3, 15), Rating = 4.8 }
            };
            mockMovieService.Setup(service => service.GetAllMovies()).ReturnsAsync(expectedMovies);
            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.Get() as ObjectResult;
            var movies = result.Value as IEnumerable<Movie>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedMovies, movies);
        }

        [Fact]
        public async Task Get_ReturnsMovieById()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var expectedMovie = new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockMovieService.Setup(service => service.GetMovieById(1)).ReturnsAsync(expectedMovie);
            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.Get(1) as ObjectResult;
            var movie = result.Value as Movie;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedMovie, movie);
        }

        [Fact]
        public async Task Post_CreatesNewMovie()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var newMovie = new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.Post(newMovie) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("Get", result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);
            Assert.Equal(newMovie, result.Value);
        }

        [Fact]
        public async Task Put_UpdatesMovie()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var updatedMovie = new Movie { Id = 1, Title = "Updated Movie", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.Put(1, updatedMovie) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task Delete_RemovesMovie()
        {
            // Arrange
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MoviesController(mockMovieService.Object);

            // Act
            var result = await controller.Delete(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }
    }
}
