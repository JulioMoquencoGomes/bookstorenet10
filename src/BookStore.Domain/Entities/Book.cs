using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public class Book: TrackableEntity
{
    public Guid Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field cannot be empty.")]
    [MinLength(1)]
    public String Name { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "The field cannot be empty.")]
    [MinLength(1)]
    public String Author { get; set; } = string.Empty;
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "The field cannot be empty.")]
    [MinLength(1)]
    public String Urlimg { get; set; } = string.Empty;
    
    public Book(Guid id, String name, String author, String urlimg)
    {
        this.Id = id;
        this.Name = name;
        this.Author = author;
        this.Urlimg = urlimg;
    }
}
