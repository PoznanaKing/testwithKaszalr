using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class Test2Context : DbContext
{
    public Test2Context()
    {
    }

    public Test2Context(DbContextOptions<Test2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Testtablecontent> Testtablecontents { get; set; }

    public virtual DbSet<Testtableuser> Testtableusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=test2;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Testtablecontent>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PRIMARY");

            entity.ToTable("testtablecontent");

            entity.HasIndex(e => e.Userid, "userid");

            entity.Property(e => e.ContentId).HasColumnName("contentId");
            entity.Property(e => e.Content)
                .HasMaxLength(200)
                .HasColumnName("content");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Testtablecontents)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("testtablecontent_ibfk_1");
        });

        modelBuilder.Entity<Testtableuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("testtableuser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
