namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models
{
    public partial class IdaasInfo
    {
        public IdaasInfo()
        {
            Users = new HashSet<User>();
        }

        public long IdaasInfoNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdpUserId { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string MailAddress { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
