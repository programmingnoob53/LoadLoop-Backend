using System;
using System.Collections.Generic;
using LoadLoop.api.Models;
using Microsoft.EntityFrameworkCore;

namespace LoadLoop.api.Data;

public partial class LoadLoopContext : DbContext
{
    public LoadLoopContext(DbContextOptions<LoadLoopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminTable> AdminTables { get; set; }

    public virtual DbSet<DriverTable> DriverTables { get; set; }

    public virtual DbSet<TruckTable> TruckTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminTable>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__ADMIN_TA__B5B611FB47FAA4F3");

            entity.ToTable("ADMIN_TABLE");

            entity.Property(e => e.AdId).HasColumnName("AD_ID");
            entity.Property(e => e.AdSalary)
                .HasColumnType("money")
                .HasColumnName("AD_SALARY");
            entity.Property(e => e.UsId).HasColumnName("US_ID");

            entity.HasOne(d => d.Us).WithMany(p => p.AdminTables)
                .HasForeignKey(d => d.UsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ADMIN_TAB__US_ID__3587F3E0");
        });

        modelBuilder.Entity<DriverTable>(entity =>
        {
            entity.HasKey(e => e.DriId).HasName("PK__DRIVER_T__8AE39018DDA4B67E");

            entity.ToTable("DRIVER_TABLE");

            entity.Property(e => e.DriId).HasColumnName("DRI_ID");
            entity.Property(e => e.DriSalary)
                .HasColumnType("money")
                .HasColumnName("DRI_SALARY");
            entity.Property(e => e.IsAvailable).HasColumnName("IS_AVAILABLE");
            entity.Property(e => e.UsId).HasColumnName("US_ID");

            entity.HasOne(d => d.Us).WithMany(p => p.DriverTables)
                .HasForeignKey(d => d.UsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DRIVER_TA__US_ID__31B762FC");
        });

        modelBuilder.Entity<TruckTable>(entity =>
        {
            entity.HasKey(e => e.TruId).HasName("PK__TRUCK_TA__EEB2B73024A574ED");

            entity.ToTable("TRUCK_TABLE");

            entity.HasIndex(e => e.TruLicensePlate, "UQ__TRUCK_TA__54085E288DB57489").IsUnique();

            entity.Property(e => e.TruId).HasColumnName("TRU_ID");
            entity.Property(e => e.DriId).HasColumnName("DRI_ID");
            entity.Property(e => e.TruImage)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TRU_IMAGE");
            entity.Property(e => e.TruLicensePlate)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TRU_LICENSE_PLATE");
            entity.Property(e => e.TruMaxWeight).HasColumnName("TRU_MAX_WEIGHT");
            entity.Property(e => e.TruType)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("TRU_TYPE");

            entity.HasOne(d => d.Dri).WithMany(p => p.TruckTables)
                .HasForeignKey(d => d.DriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TRUCK_TAB__DRI_I__3A4CA8FD");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UsId).HasName("PK__USER_TAB__DE473D818F8C65FA");

            entity.ToTable("USER_TABLE");

            entity.HasIndex(e => e.UsEmail, "UQ__USER_TAB__BA0A3866A4E3C0A0").IsUnique();

            entity.HasIndex(e => new { e.UsIdNumber, e.UsName }, "UQ__USER_TAB__D2255197F0687426").IsUnique();

            entity.HasIndex(e => e.UsPhone, "UQ__USER_TAB__EECBD74424EBCD5C").IsUnique();

            entity.Property(e => e.UsId).HasColumnName("US_ID");
            entity.Property(e => e.UsEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("US_EMAIL");
            entity.Property(e => e.UsIdNumber)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("US_ID_NUMBER");
            entity.Property(e => e.UsName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("US_NAME");
            entity.Property(e => e.UsPasswordHash)
                .IsUnicode(false)
                .HasColumnName("US_PASSWORD_HASH");
            entity.Property(e => e.UsPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("US_PHONE");
            entity.Property(e => e.UsRole)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("U")
                .IsFixedLength()
                .HasColumnName("US_ROLE");
            entity.Property(e => e.UsSurname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("US_SURNAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
