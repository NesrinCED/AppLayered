using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using static iTextSharp.text.pdf.events.IndexEvents;

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

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Templatehistory> Templatehistories { get; set; }
    public virtual DbSet<ProjectAuthorization> ProjectAuthorizations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAP-TUN-DBHZWT2\\SQLEXPRESS;Initial Catalog=AppLayeredDB;trusted_connection=true;Trust Server Certificate = true;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Templatehistory>().ToTable(tb => tb.HasTrigger("TemplatehistoryTrigger"));

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__781134A1F35AFE84");

            entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Employees)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_employee_role");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__project__1CB92E03D490C625");

            entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");
        });
       
        /*modelBuilder.Entity<Template>()
        .Ignore(t => t.Templatehistories)
        .Ignore(t => t.Content)
        .Ignore(t => t.TemplateId);*/

        modelBuilder.Entity<Template>(entity =>
        {

            
            entity.HasKey(e => e.TemplateId).HasName("PK__template__E7FB8F2156A9F4F2");

            entity.Property(e => e.TemplateId).HasDefaultValueSql("(newid())");

            entity.Property(e => e.Content).HasDefaultValueSql("('<ul><li><p style=\"text-align: center;\"><strong>Hello Its my content</strong></p></li></ul>')");

            entity.HasOne(d => d.TemplateCreatedBy).WithMany(p => p.TemplateCreatedBy)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__template__Employ__3F466844");

            entity.HasOne(d => d.TemplateModifiedBy).WithMany(p => p.TemplateModifiedBy).HasConstraintName("FK_Modified_By");

            entity.HasOne(d => d.Project).WithMany(p => p.Templates).HasConstraintName("FK__template__Projec__3E52440B");

        });

        modelBuilder.Entity<Templatehistory>(entity =>
        {
            entity.HasKey(e => e.TemplateHistoryId).HasName("PK__template__38AC53C855901A84");

            entity.Property(e => e.TemplateHistoryId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Template).WithMany(p => p.Templatehistories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_templatehistory_template");

        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__D80AB4BB97A968CE");

            entity.Property(e => e.RoleId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<ProjectAuthorization>(entity =>
        {
            entity.HasKey(e => e.ProjectAuthorizationId).HasName("PK__ProjectA__66F5361F0C2227CE");

            entity.Property(e => e.ProjectAuthorizationId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectAuthorizations)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_ProjectAuthorization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
