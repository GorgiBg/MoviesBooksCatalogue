
using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.Services;
using Moq;
using MoviesBooksCatalogue.Data;

namespace UnitTests

{
    public class MovieServiceTests
    {
        [Fact]
        public async Task GetAllMovies_ReturnsAllMovies()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Movie>>();
            var expectedMovies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 },
                new Movie { Id = 2, Title = "Movie 2", Genre = "Drama", ReleaseDate = new DateTime(2021, 3, 15), Rating = 4.8 }
            };
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedMovies);
            var movieService = new MovieService(mockRepository.Object);

            // Act
            var result = await movieService.GetAllMovies();

            // Assert
            Assert.Equal(expectedMovies, result);
        }

        [Fact]
        public async Task GetMovieById_ReturnsMovieWithGivenId()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Movie>>();
            var expectedMovie = new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(expectedMovie);
            var movieService = new MovieService(mockRepository.Object);

            // Act
            var result = await movieService.GetMovieById(1);

            // Assert
            Assert.Equal(expectedMovie, result);
        }

        [Fact]
        public async Task AddMovie_AddsNewMovie()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Movie>>();
            var movieToAdd = new Movie { Title = "New Movie", Genre = "Comedy", ReleaseDate = new DateTime(2022, 5, 1), Rating = 4.2 };
            var movieService = new MovieService(mockRepository.Object);

            // Act
            await movieService.AddMovie(movieToAdd);

            // Assert
            mockRepository.Verify(repo => repo.Add(movieToAdd), Times.Once);
        }

        [Fact]
        public async Task UpdateMovie_UpdatesExistingMovie()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Movie>>();
            var existingMovie = new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingMovie);
            var movieToUpdate = new Movie { Id = 1, Title = "Updated Movie", Genre = "Comedy", ReleaseDate = new DateTime(2022, 5, 1), Rating = 4.2 };
            var movieService = new MovieService(mockRepository.Object);

            // Act
            await movieService.UpdateMovie(movieToUpdate);

            // Assert
            mockRepository.Verify(repo => repo.Update(movieToUpdate), Times.Once);
        }

        [Fact]
        public async Task DeleteMovie_DeletesMovieWithGivenId()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Movie>>();
            var existingMovie = new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = new DateTime(2020, 1, 1), Rating = 4.5 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingMovie);
            var movieService = new MovieService(mockRepository.Object);

            // Act
            await movieService.DeleteMovie(1);

            // Assert
            mockRepository.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}
