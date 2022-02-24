namespace UsedCar.Backend.UseCases.Users.Models
{
    public class UserReadResponse
    {
        public string? UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? DisplayName { get; set; }

        public string? MailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Zip { get; set;}

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Street1 { get; set; }

        public string? Street2 { get; set; }
    }
}
