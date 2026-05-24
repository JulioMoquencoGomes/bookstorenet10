using System.Net;

namespace BookStore.Domain.Entities;

public class Reader: TrackableEntity
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public DateTime? Birthday { get; set; }
    public string Urlimg { get; set; }
    
    public Reader(Guid id, String name, DateTime? birthday, String urlimg)
    {
        this.Id = id;
        this.Name = name;
        this.Birthday = birthday;
        this.Urlimg = urlimg;
    }
}
