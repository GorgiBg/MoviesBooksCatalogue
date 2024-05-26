using MoviesBooksCatalogue.Data;
using MoviesBooksCatalogue.Models;

namespace MoviesBooksCatalogue.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);
        Task AddMovie(Movie movie);
        Task UpdateMovie(Movie movie);
        Task DeleteMovie(int id);
    }
    
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _movieRepository.GetAll();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _movieRepository.GetById(id);
        }

        public async Task AddMovie(Movie movie)
        {
            await _movieRepository.Add(movie);
        }

        public async Task UpdateMovie(Movie movie)
        {
            await _movieRepository.Update(movie);
        }

        public async Task DeleteMovie(int id)
        {
            await _movieRepository.Delete(id);
        }
    }
}