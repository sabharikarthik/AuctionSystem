using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuctionSystemCore.AuctionSystemCore.Models
{
    public partial class auction_systemContext : DbContext
    {
        public auction_systemContext()
        {
        }

        public auction_systemContext(DbContextOptions<auction_systemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bids> Bids { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<SoldItems> SoldItems { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=auction_system;Persist Security Info=False;User ID=sa;Password=R00tqu0tient;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bids>(entity =>
            {
                entity.ToTable("bids");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IncrementAmount).HasColumnName("increment_amount");

                entity.Property(e => e.MaxBid).HasColumnName("max_bid");

                entity.Property(e => e.SoldItemId).HasColumnName("sold_item_id");

                entity.Property(e => e.StartBid).HasColumnName("start_bid");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.SoldItem)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.SoldItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bids__sold_item___2C3393D0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__bids__user_id__2B3F6F97");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SoldItems>(entity =>
            {
                entity.ToTable("sold_items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BasePrice).HasColumnName("base_price");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.IncrementLimit).HasColumnName("increment_limit");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.SoldItems)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__sold_item__event__286302EC");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
