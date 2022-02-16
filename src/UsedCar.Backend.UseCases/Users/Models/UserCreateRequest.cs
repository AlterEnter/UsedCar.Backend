namespace UsedCar.Backend.UseCases.Users.Models
{
    public class UserCreateRequest
    {
        public string? Address { get; set; } 
        public string? City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? DisplayName { get; set; }
        public string? IDassId { get; set; }
        public string? MailAddress { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? State { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public Guid  UserId { get; set; }
        public string? Zip { get; set; }
    }
}
