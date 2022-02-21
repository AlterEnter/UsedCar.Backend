using Microsoft.EntityFrameworkCore;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore.Models;

namespace UsedCar.Backend.Infrastructures.EntityFrameworkCore
{
    public partial class UsedCarDBContext : DbContext
    {
        public UsedCarDBContext()
        {
        }

        public UsedCarDBContext(DbContextOptions<UsedCarDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IdaasInfo> IdaasInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Japanese_BIN2");

            modelBuilder.Entity<IdaasInfo>(entity =>
            {
                entity.HasKey(e => e.IdaasInfoNumber)
                    .HasName("PK__IdaasInf__A2EC98441AA88DA7");

                entity.ToTable("IdaasInfo", "UsedCar");

                entity.HasIndex(e => e.IdpUserId, "UQ_IdaasInfo_IdpUserId")
                    .IsUnique();

                entity.HasIndex(e => e.MailAddress, "UQ_IdaasInfo_MailAddress")
                    .IsUnique();

                entity.Property(e => e.DisplayName).HasMaxLength(30);

                entity.Property(e => e.IdpUserId).HasMaxLength(36);

                entity.Property(e => e.MailAddress).HasMaxLength(254);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
