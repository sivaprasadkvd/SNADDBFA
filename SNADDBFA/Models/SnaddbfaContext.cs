using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SNADDBFA.Models;

public partial class SnaddbfaContext : DbContext
{
    public SnaddbfaContext()
    {
    }

    public SnaddbfaContext(DbContextOptions<SnaddbfaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RegistrationTable> RegistrationTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=REALMESIVALAPPY;Database=snaddbfa;user id=sa;password=kvd1676sp;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RegistrationTable>(entity =>
        {
            entity.HasKey(e => e.Sno);

            entity.ToTable("registrationTable");

            entity.Property(e => e.Sno).HasColumnName("sno");
            entity.Property(e => e.About)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("about");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userId");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
