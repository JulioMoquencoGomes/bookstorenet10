using System.Net;

namespace BookStore.Domain.Entities;

public class Book: TrackableEntity
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String Author { get; set; }
    public string Urlimg { get; set; }
    
    public Book(Guid id, String name, String author, String urlimg)
    {
        this.Id = id;
        this.Name = name;
        this.Author = author;
        this.Urlimg = urlimg;
    }
}
