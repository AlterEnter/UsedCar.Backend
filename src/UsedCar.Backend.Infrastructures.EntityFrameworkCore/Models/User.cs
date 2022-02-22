namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models
{
    public partial class User
    {
        public long UserNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public string IdpUserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Zip { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street1 { get; set; } = null!;
        public string? Street2 { get; set; }

        public virtual IdaasInfo IdpUser { get; set; } = null!;
    }
}
