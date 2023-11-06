using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Models;

public partial class BookManagementDbContext : DbContext
{
    public BookManagementDbContext()
    {
    }

    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookMst> BookMsts { get; set; }

    public virtual DbSet<CategoryMst> CategoryMsts { get; set; }

    public virtual DbSet<SubcategoryMst> SubcategoryMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=ARCHE-ITD440\\SQLEXPRESS;Database=BookManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookMst>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__BookMst__3DE0C2072F832DDB");

            entity.ToTable("BookMst");

            entity.Property(e => e.AuthorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BookName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CoverImagePath).HasMaxLength(200);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Edition)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PdfPath).HasMaxLength(200);
            entity.Property(e => e.Price).HasMaxLength(300);
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CategoryMst>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B83444E78");

            entity.ToTable("CategoryMst");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SubcategoryMst>(entity =>
        {
            entity.HasKey(e => e.SubcategoryId).HasName("PK__Subcateg__9C4E705D7A0E6786");

            entity.ToTable("SubcategoryMst");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SubcategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserMst__1788CC4CC86A9589");

            entity.ToTable("UserMst");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
