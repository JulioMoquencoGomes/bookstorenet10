using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities;

public class Lend: TrackableEntity
{
    public Guid Id { get; set; }

    [Required]
    public int BookId { get; set; }
    public Book? Book { get; set; }

    [Required]
    public int ReaderId { get; set; }
    public Reader? Reader { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public DateTime? DeliveryDate { get; set; }
    
    public Lend(Guid id, int bookId, int readerId, 
        DateTime startDate, 
        DateTime endDate,
        DateTime? deliveryDate = null
    )
    {
        this.Id = id;
        this.BookId = bookId;
        this.ReaderId = readerId;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.DeliveryDate = deliveryDate;
    }
}
