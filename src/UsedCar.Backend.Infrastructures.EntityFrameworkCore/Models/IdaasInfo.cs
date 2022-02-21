using System;
using System.Collections.Generic;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models
{
    public partial class IdaasInfo
    {
        public long IdaasInfoNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdpUserId { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string MailAddress { get; set; } = null!;
    }
}
