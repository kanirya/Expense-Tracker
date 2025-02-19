using System.ComponentModel.DataAnnotations;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Stripe;

namespace Expense_Tracker.Models
{
    public class hostelRoom
    {
        public Guid Id { get; set; }
        public int roomNo { get; set; }
        public string floor {  get; set; }
        public int seats {  get; set; }
        

    }
}


public class Floor
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}

public class Room
{
    [Key]

    public int FloorId { get; set; }
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Floor Floor { get; set; }
    public ICollection<Student> Students { get; set; } = new List<Student>();
}

public class Student
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Gender Gender { get; set; }
    public int? RoomId { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Room? Room { get; set; }
}


public class Booking
{
    public Guid Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Student Student { get; set; }
    public Room Room { get; set; }
}

public class Payment
{
    public Guid Id { get; set; }
    public int StudentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public Student Student { get; set; }
}
