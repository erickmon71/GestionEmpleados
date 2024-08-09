using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.Models;

public partial class GestionEmpleadosDbContext : DbContext
{
    public GestionEmpleadosDbContext()
    {
    }

    public GestionEmpleadosDbContext(DbContextOptions<GestionEmpleadosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<PerformanceReview> PerformanceReviews { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Responsibility> Responsibilities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1124710DBC");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasMany(d => d.Roles).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeeR__RoleI__619B8048"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeeR__Emplo__60A75C0F"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "RoleId").HasName("PK__Employee__C27FE3F07BDCEB93");
                        j.ToTable("EmployeeRoles");
                    });
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId).HasName("PK__Payrolls__99DFC67263865893");

            entity.Property(e => e.Bonuses).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Deductions).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NetSalary).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.Payrolls)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Payrolls__Employ__6A30C649");
        });

        modelBuilder.Entity<PerformanceReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Performa__74BC79CE70C74253");

            entity.HasOne(d => d.Employee).WithMany(p => p.PerformanceReviewEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Performan__Emplo__6D0D32F4");

            entity.HasOne(d => d.Reviewer).WithMany(p => p.PerformanceReviewReviewers)
                .HasForeignKey(d => d.ReviewerId)
                .HasConstraintName("FK__Performan__Revie__6E01572D");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2F9E742E78");

            entity.Property(e => e.PermissionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A79F3E4754F");

            entity.Property(e => e.PositionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Responsibility>(entity =>
        {
            entity.HasKey(e => e.ResponsibilityId).HasName("PK__Responsi__87A0F9B5ED0C1742");

            entity.Property(e => e.ResponsibilityName).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AA5010487");

            entity.Property(e => e.RoleName).HasMaxLength(50);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePermi__Permi__18EBB532"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolePermi__RoleI__17F790F9"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__RolePerm__6400A1A85C2672FE");
                        j.ToTable("RolePermissions");
                    });
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B494F93DB37");

            entity.HasOne(d => d.Employee).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Schedules__Emplo__66603565");

            entity.HasOne(d => d.Shift).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK__Schedules__Shift__6754599E");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__Shifts__C0A8388193F552FD");

            entity.Property(e => e.ShiftName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
