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

        public virtual DbSet<CommentRating> CommentRating { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Parking> Parking { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserProperty> UserProperty { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-CUU4AQ5;Database=ParkNGo;Trusted_Connection=True;ConnectRetryCount=0;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentRating>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.Attachments)
                    .HasColumnName("attachments")
                    .IsUnicode(false);

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasColumnName("from")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasColumnName("to")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parking>(entity =>
            {
                entity.Property(e => e.ParkingId).HasColumnName("parking_id");

                entity.Property(e => e.Additional)
                    .HasColumnName("additional")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CommentsId).HasColumnName("comments_id");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("image");

                entity.Property(e => e.IntersectionFrom)
                    .HasColumnName("intersection_from")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntersectionTo)
                    .HasColumnName("intersection_to")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.LatTo).HasColumnName("lat_to");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.LongTo).HasColumnName("long_to");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Ccv)
                    .IsRequired()
                    .HasColumnName("ccv")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Expiration)
                    .IsRequired()
                    .HasColumnName("expiration")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.PropertyOwner)
                    .IsRequired()
                    .HasColumnName("property_owner")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaserId)
                    .IsRequired()
                    .HasColumnName("purchaser_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TimeSlot)
                    .IsRequired()
                    .HasColumnName("time_slot")
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate)
                    .HasColumnName("transaction_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

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
            });

            modelBuilder.Entity<UserProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.AvailabilityId)
                    .HasColumnName("availability_id")
                    .IsUnicode(false);

                entity.Property(e => e.CommentsId).HasColumnName("comments_id");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("image");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.PropAddress)
                    .IsRequired()
                    .HasColumnName("prop_address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PropCity)
                    .IsRequired()
                    .HasColumnName("prop_city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PropPostalCode)
                    .IsRequired()
                    .HasColumnName("prop_postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PropProvinceId)
                    .IsRequired()
                    .HasColumnName("prop_province_id")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
