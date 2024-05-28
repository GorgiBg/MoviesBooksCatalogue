using MoviesBooksCatalogue.Models;
using MoviesBooksCatalogue.MoviesBooksCatalogue;
using MoviesBooksCatalogue.Services;

namespace MoviesBooksCatalogue
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter 'web' to start the web application or 'console' to start the console application:");
            var mode = Console.ReadLine()?.Trim().ToLower();

            if (mode == "web")
            {
                CreateHostBuilder(args).Build().Run();
            }
            else if (mode == "console")
            {
                await RunConsoleApp(args);
            }
            else
            {
                Console.WriteLine("Invalid option. Starting the console application by default.");
                await RunConsoleApp(args);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task RunConsoleApp(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var bookService = services.GetRequiredService<IBookService>();
            var movieService = services.GetRequiredService<IMovieService>();

            Console.WriteLine("Welcome to the Movies and Books Catalogue!");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. List all books");
                Console.WriteLine("2. Add a new book");
                Console.WriteLine("3. Update a book");
                Console.WriteLine("4. Delete a book");
                Console.WriteLine("5. List all movies");
                Console.WriteLine("6. Add a new movie");
                Console.WriteLine("7. Update a movie");
                Console.WriteLine("8. Delete a movie");
                Console.WriteLine("9. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var books = await bookService.GetAllBooks();
                        foreach (var book in books)
                        {
                            Console.WriteLine($"Id: {book.Id}, Title: {book.Title}, Genre: {book.Genre}, Release Date: {book.ReleaseDate}, Rating: {book.Rating}");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter book title:");
                        var title = Console.ReadLine();
                        Console.WriteLine("Enter book genre:");
                        var genre = Console.ReadLine();
                        Console.WriteLine("Enter book release date (YYYY-MM-DD):");
                        var releaseDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter book rating:");
                        var rating = double.Parse(Console.ReadLine());

                        var newBook = new Book { Title = title, Genre = genre, ReleaseDate = releaseDate, Rating = rating };
                        await bookService.AddBook(newBook);
                        Console.WriteLine("Book added successfully.");
                        break;

                    case "3":
                        Console.WriteLine("Enter book id to update:");
                        var updateBookId = int.Parse(Console.ReadLine());
                        var bookToUpdate = await bookService.GetBookById(updateBookId);
                        if (bookToUpdate != null)
                        {
                            Console.WriteLine("Enter new title:");
                            bookToUpdate.Title = Console.ReadLine();
                            Console.WriteLine("Enter new genre:");
                            bookToUpdate.Genre = Console.ReadLine();
                            Console.WriteLine("Enter new release date (YYYY-MM-DD):");
                            bookToUpdate.ReleaseDate = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter new rating:");
                            bookToUpdate.Rating = double.Parse(Console.ReadLine());

                            await bookService.UpdateBook(bookToUpdate);
                            Console.WriteLine("Book updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Enter book id to delete:");
                        var deleteBookId = int.Parse(Console.ReadLine());
                        await bookService.DeleteBook(deleteBookId);
                        Console.WriteLine("Book deleted successfully.");
                        break;

                    case "5":
                        var movies = await movieService.GetAllMovies();
                        foreach (var movie in movies)
                        {
                            Console.WriteLine($"Id: {movie.Id}, Title: {movie.Title}, Genre: {movie.Genre}, Release Date: {movie.ReleaseDate}, Rating: {movie.Rating}");
                        }
                        break;

                    case "6":
                        Console.WriteLine("Enter movie title:");
                        var movieTitle = Console.ReadLine();
                        Console.WriteLine("Enter movie genre:");
                        var movieGenre = Console.ReadLine();
                        Console.WriteLine("Enter movie release date (YYYY-MM-DD):");
                        var movieReleaseDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter movie rating:");
                        var movieRating = double.Parse(Console.ReadLine());

                        var newMovie = new Movie { Title = movieTitle, Genre = movieGenre, ReleaseDate = movieReleaseDate, Rating = movieRating };
                        await movieService.AddMovie(newMovie);
                        Console.WriteLine("Movie added successfully.");
                        break;

                    case "7":
                        Console.WriteLine("Enter movie id to update:");
                        var updateMovieId = int.Parse(Console.ReadLine());
                        var movieToUpdate = await movieService.GetMovieById(updateMovieId);
                        if (movieToUpdate != null)
                        {
                            Console.WriteLine("Enter new title:");
                            movieToUpdate.Title = Console.ReadLine();
                            Console.WriteLine("Enter new genre:");
                            movieToUpdate.Genre = Console.ReadLine();
                            Console.WriteLine("Enter new release date (YYYY-MM-DD):");
                            movieToUpdate.ReleaseDate = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter new rating:");
                            movieToUpdate.Rating = double.Parse(Console.ReadLine());

                            await movieService.UpdateMovie(movieToUpdate);
                            Console.WriteLine("Movie updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Movie not found.");
                        }
                        break;

                    case "8":
                        Console.WriteLine("Enter movie id to delete:");
                        var deleteMovieId = int.Parse(Console.ReadLine());
                        await movieService.DeleteMovie(deleteMovieId);
                        Console.WriteLine("Movie deleted successfully.");
                        break;

                    case "9":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
