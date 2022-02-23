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
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Japanese_BIN2");

            modelBuilder.Entity<IdaasInfo>(entity =>
            {
                entity.HasKey(e => e.IdaasInfoNumber)
                    .HasName("PK__IdaasInf__A2EC9844411AFE0B");

                entity.ToTable("IdaasInfo", "UsedCar");

                entity.HasIndex(e => e.IdpUserId, "UQ_IdaasInfo_IdpUserId")
                    .IsUnique();

                entity.HasIndex(e => e.MailAddress, "UQ_IdaasInfo_MailAddress")
                    .IsUnique();

                entity.Property(e => e.DisplayName).HasMaxLength(30);

                entity.Property(e => e.IdpUserId).HasMaxLength(36);

                entity.Property(e => e.MailAddress).HasMaxLength(254);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserNumber)
                    .HasName("PK__User__578B7EF7F0FE2B34");

                entity.ToTable("User", "UsedCar");

                entity.HasIndex(e => e.UserId, "UQ_User_UserId")
                    .IsUnique();

                entity.Property(e => e.City).HasMaxLength(10);

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.IdpUserId).HasMaxLength(36);

                entity.Property(e => e.LastName).HasMaxLength(15);

                entity.Property(e => e.PhoneNumber).HasMaxLength(15);

                entity.Property(e => e.State).HasMaxLength(10);

                entity.Property(e => e.Street1).HasMaxLength(30);

                entity.Property(e => e.Street2).HasMaxLength(30);

                entity.Property(e => e.Zip).HasMaxLength(10);

                entity.HasOne(d => d.IdpUser)
                    .WithMany(p => p.Users)
                    .HasPrincipalKey(p => p.IdpUserId)
                    .HasForeignKey(d => d.IdpUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_IdpUserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
