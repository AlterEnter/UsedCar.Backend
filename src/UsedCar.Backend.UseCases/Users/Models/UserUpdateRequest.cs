namespace UsedCar.Backend.UseCases.Users.Models
{
    public class UserUpdateRequest
    {
        public string IdaasId { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set;} = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MailAddress { get; set;} = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Street1 { get; set; } = string.Empty;
        public string? Street2 { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }
}
