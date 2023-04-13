using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataContext;

public partial class AppLayeredDBDbContext : DbContext
{
    public AppLayeredDBDbContext()
    {
    }

    public AppLayeredDBDbContext(DbContextOptions<AppLayeredDBDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9NT4DKI\\SQLEXPRESS;database=AppLayeredDB;trusted_connection=true;Trust Server Certificate = true;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__781134A1F35AFE84");

            entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__project__1CB92E03D490C625");

            entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__template__E7FB8F2156A9F4F2");

            entity.Property(e => e.TemplateId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.TemplateCreatedBy).WithMany(p => p.TemplateCreatedBy)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__template__Employ__3F466844");

            entity.HasOne(d => d.TemplateModifiedBy).WithMany(p => p.TemplateModifiedBy).HasConstraintName("FK_Modified_By");

            entity.HasOne(d => d.Project).WithMany(p => p.Templates).HasConstraintName("FK__template__Projec__3E52440B");
        });
        /*    [ForeignKey("EmployeId")]
    [InverseProperty("TemplateEmployes")]
    public virtual Employee? Employe { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("TemplateModifiedByNavigations")]
    public virtual Employee? ModifiedByNavigation { get; set; }*/
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
