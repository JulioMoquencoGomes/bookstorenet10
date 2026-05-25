using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public class Reader: TrackableEntity
{
    public Guid Id { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = "ColumnName cannot be empty.")]
    [RegularExpression(@"^\S+$")]
    public String Name { get; set; }

    public DateTime? Birthday { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "ColumnName cannot be empty.")]
    [RegularExpression(@"^\S+$")]
    public String Urlimg { get; set; }
    
    public Reader(Guid id, String name, DateTime? birthday, String urlimg)
    {
        this.Id = id;
        this.Name = name;
        this.Birthday = birthday;
        this.Urlimg = urlimg;
    }
}
