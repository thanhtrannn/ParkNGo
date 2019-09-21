using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ParkNGo.Models
{
    public partial class ParkNGoContext : DbContext
    {
        public ParkNGoContext()
        {
        }

        public ParkNGoContext(DbContextOptions<ParkNGoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:user-thanh.database.windows.net,1433;Initial Catalog=ParkNGo;Persist Security Info=False;User ID=ThanhTran;Password=Asdfasdf2.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayUrl)
                    .IsRequired()
                    .HasColumnName("display_url")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.ProvinceId)
                    .IsRequired()
                    .HasColumnName("province_id")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecretAnswer1)
                    .IsRequired()
                    .HasColumnName("secret_answer1")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecretAnswer2)
                    .IsRequired()
                    .HasColumnName("secret_answer2")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecretQuestion1)
                    .IsRequired()
                    .HasColumnName("secret_question1")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SecretQuestion2)
                    .IsRequired()
                    .HasColumnName("secret_question2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
