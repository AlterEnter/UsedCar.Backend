namespace UsedCar.Backend.UseCases.Users.Models
{
    public class UserCreateRequest
    {
        public string IdaasId { get; set; } = Guid.NewGuid().ToString();
        public string MailAddress { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
