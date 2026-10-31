using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadLoop.api.Models;

public partial class LoadLoopDbContext : DbContext
{
    public LoadLoopDbContext()
    {
    }

    public LoadLoopDbContext(DbContextOptions<LoadLoopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.TruckId).HasName("PK__TRUCK__6693F6337C29A10C");

            entity.ToTable("TRUCK");

            entity.Property(e => e.TruckId).HasColumnName("TRUCK_ID");
            entity.Property(e => e.TruckImage)
                .IsUnicode(false)
                .HasColumnName("TRUCK_IMAGE");
            entity.Property(e => e.TruckLicensePlate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TRUCK_LICENSE_PLATE");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TRUCK__USER_ID__14270015");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USER_TAB__F3BEEBFF2339A1A1");

            entity.ToTable("USER_TABLE");

            entity.HasIndex(e => new { e.UserEmail, e.UserRole }, "UQ_USER_EMAIL_ROLE").IsUnique();

            entity.HasIndex(e => e.UserEmail, "UQ__USER_TAB__43CA31682C25787F").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.UserAge).HasColumnName("USER_AGE");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_EMAIL");
            entity.Property(e => e.UserPasswordHash)
                .IsUnicode(false)
                .HasColumnName("USER_PASSWORD_HASH");
            entity.Property(e => e.UserRole)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("USER_ROLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
