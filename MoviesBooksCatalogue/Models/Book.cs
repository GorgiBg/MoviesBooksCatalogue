using System.ComponentModel.DataAnnotations;

namespace MoviesBooksCatalogue.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Genre is required.")]
    public string Genre { get; set; }

    [Required(ErrorMessage = "Release date is required.")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Rating is required.")]
    public double Rating { get; set; }
}