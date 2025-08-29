using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dbfirst.Models;

public partial class DbfirstDotnetContext : DbContext
{
    public DbfirstDotnetContext()
    {
    }

    public DbfirstDotnetContext(DbContextOptions<DbfirstDotnetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.Email, "employees_email_key").IsUnique();

            entity.Property(e => e.Employeeid).HasColumnName("employeeid");
            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Departmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_departmentid_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("students_pkey");

            entity.ToTable("students");

            entity.HasIndex(e => e.Email, "students_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "Studentcourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("Courseid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("studentcourses_courseid_fkey"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("Studentid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("studentcourses_studentid_fkey"),
                    j =>
                    {
                        j.HasKey("Studentid", "Courseid").HasName("studentcourses_pkey");
                        j.ToTable("studentcourses");
                        j.IndexerProperty<int>("Studentid").HasColumnName("studentid");
                        j.IndexerProperty<int>("Courseid").HasColumnName("courseid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
