using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Primer_Parcial.Models;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasCita> CategoriasCitas { get; set; }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Diagnostico> Diagnosticos { get; set; }

    public virtual DbSet<Doctore> Doctores { get; set; }

    public virtual DbSet<Enfermera> Enfermeras { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasCita>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaCita).HasName("PK__Categori__114A2E92A651349A");

            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(35);
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Citas__394B0202326FDFC1");

            entity.Property(e => e.Descripción).HasMaxLength(200);

            entity.HasOne(d => d.IdCategoriaCitaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCategoriaCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdCategor__3C69FB99");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdDoctor__3A81B327");

            entity.HasOne(d => d.IdEnfermeraNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEnfermera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdEnferme__3B75D760");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__IdPacient__398D8EEE");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D539991D3");

            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(80);
            entity.Property(e => e.Telefono).HasMaxLength(10);
            entity.Property(e => e.Ubicación).HasMaxLength(50);
        });

        modelBuilder.Entity<Diagnostico>(entity =>
        {
            entity.HasKey(e => e.IdDiagnostico).HasName("PK__Diagnost__BD16DB691D10418D");

            entity.Property(e => e.Descripcion).HasColumnType("text");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Diagnosticos)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosti__IdDoc__403A8C7D");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Diagnosticos)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diagnosti__IdPac__3F466844");
        });

        modelBuilder.Entity<Doctore>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__Doctores__F838DB3E706836D6");

            entity.Property(e => e.Cedula).HasMaxLength(11);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(60);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Doctores)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctores__IdDepa__31EC6D26");
        });

        modelBuilder.Entity<Enfermera>(entity =>
        {
            entity.HasKey(e => e.IdEnfermera).HasName("PK__Enfermer__56277F23CD120B8E");

            entity.Property(e => e.Cedula).HasMaxLength(11);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(60);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(10);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Enfermeras)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enfermera__IdDep__34C8D9D1");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__693FA0AF70A82E6A");

            entity.Property(e => e.Descripcion).HasMaxLength(150);
            entity.Property(e => e.Nombre).HasMaxLength(35);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estados__FBB0EDC1662ECE55");

            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF9010102AE43");

            entity.Property(e => e.Numero).HasMaxLength(5);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49B253BFD35");

            entity.Property(e => e.Cedula).HasMaxLength(11);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(60);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(10);
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PK__Tratamie__5CB7E7530EAF222C");

            entity.Property(e => e.Descripcion).HasColumnType("text");

            entity.HasOne(d => d.IdDiagnosticoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.IdDiagnostico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tratamien__IdDia__4316F928");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tratamien__IdDoc__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
