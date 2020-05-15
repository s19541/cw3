using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cwiczenia3.ModelsF
{
    public partial class s19541Context : DbContext
    {
        public s19541Context()
        {
        }

        public s19541Context(DbContextOptions<s19541Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Procedure> Procedure { get; set; }
        public virtual DbSet<ProcedureAnimal> ProcedureAnimal { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }
        public virtual DbSet<Tabela1> Tabela1 { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TeamMember> TeamMember { get; set; }

        // Unable to generate entity type for table 'dbo.TOWAR'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.DOSTAWCA'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.MIASTO'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.DZIAL'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRACOWNIK'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.budzet'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.EMP'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.DEPT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SALGRADE'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tabela'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tabela2'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tabela3'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRZEDMIOT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.NAUCZYCIEL'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.KLASA'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.UCZEN'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.OCENA'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.KLIENT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.KATEGORIA'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRODUKT'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.SPRZEDAZ'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.IdAnimal);

                entity.Property(e => e.AdmissionDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Animal_Owner");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment);

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.HasKey(e => e.IdProcedure);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProcedureAnimal>(entity =>
            {
                entity.HasKey(e => new { e.ProcedureIdProcedure, e.AnimalIdAnimal, e.Date });

                entity.ToTable("Procedure_Animal");

                entity.Property(e => e.ProcedureIdProcedure).HasColumnName("Procedure_IdProcedure");

                entity.Property(e => e.AnimalIdAnimal).HasColumnName("Animal_IdAnimal");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.AnimalIdAnimalNavigation)
                    .WithMany(p => p.ProcedureAnimal)
                    .HasForeignKey(d => d.AnimalIdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_3_Animal");

                entity.HasOne(d => d.ProcedureIdProcedureNavigation)
                    .WithMany(p => p.ProcedureAnimal)
                    .HasForeignKey(d => d.ProcedureIdProcedure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Table_3_Procedure");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject);

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber);

                entity.Property(e => e.IndexNumber)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy);

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tabela1>(entity =>
            {
                entity.ToTable("tabela1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask);

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdAssignedToNavigation)
                    .WithMany(p => p.TaskIdAssignedToNavigation)
                    .HasForeignKey(d => d.IdAssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember2");

                entity.HasOne(d => d.IdCreatorNavigation)
                    .WithMany(p => p.TaskIdCreatorNavigation)
                    .HasForeignKey(d => d.IdCreator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TeamMember1");

                entity.HasOne(d => d.IdProjectNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Project");

                entity.HasOne(d => d.IdTaskTypeNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.IdTaskType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_TaskType");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.HasKey(e => e.IdTaskType);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasKey(e => e.IdTeamMember);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
