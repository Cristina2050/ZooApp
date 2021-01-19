using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ZooServices.Model;

namespace ZooServices
{
    public partial class DbContextZoo:DbContext
    {
        public DbContextZoo()
        {
        }

        public DbContextZoo(DbContextOptions<DbContextZoo> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Breed> Breed { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breed>(entity =>
            {
                entity.HasKey(e => e.BreedIdPk);

                entity.ToTable("BREED");

                entity.HasComment("TABLA QUE ALMACENA LOS TIPO DE RAZAS DE ANIMALES");

                entity.Property(e => e.BreedIdPk)
                    .HasColumnName("BREED_ID_PK")
                    .HasComment("Clave principal de identificación del registro");


                entity.Property(e => e.BreedName)
                            .IsRequired()
                            .HasColumnName("BREED_NAME")
                            .HasMaxLength(64)
                            .IsUnicode(false)
                            .HasComment("Corresponde al nombre de la raza de un animal.");                
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.AnimIdPk);

                entity.ToTable("ANIMAL");

                entity.HasComment(@"Tabla que almacena los animales del zoológico.");

                entity.HasIndex(e => e.BreedIdPk)
                    .HasName("BREED_ANIM_INDX");

                entity.HasIndex(e => e.AnimIdPk)
                    .HasName("ANIM_BREED_INDX");

                entity.Property(e => e.AnimIdPk)
                    .HasColumnName("ANIM_ID_PK")
                    .HasComment("Clave principal de identificación del registro");

                entity.Property(e => e.BreedIdPk)
                    .HasColumnName("BREED_ID_PK")
                    .HasComment("Corresponde a la clave foránea de razas de animales.");

                entity.Property(e => e.AnimName)
                    .IsRequired()
                    .HasColumnName("ANIM_NAME")
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasComment("Corresponde al nombre del animal");

                entity.Property(e => e.AnimAge)
                    .IsRequired()
                    .HasColumnName("ANIM_AGE")
                    .HasComment("Edad del animal.");

                entity.Property(e => e.AnimWeight)
                   .IsRequired()
                   .HasColumnName("ANIM_WEIGHT")
                   .HasColumnType("decimal(12,2)")
                   .HasComment("Peso del animal.");

                entity.Property(e => e.AnimCharacteristics)
                    .HasColumnName("ANIM_CHARACTERISTIC")
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasComment("Corresponde a las características del animal");

                entity.Property(e => e.AnimUserId)
                    .IsRequired()
                    .HasColumnName("ANIM_USER")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("Corresponde al usuario que transacciona con el registro. ");

                entity.Property(e => e.AnimCreateDate)
                    .HasColumnName("ANIM_CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasComment("Corresponde a la fecha de creación del registro");

                entity.Property(e => e.AnimUpdateDate)
                    .HasColumnName("ANIM_UPDATE_DATE")
                    .HasColumnType("datetime")
                    .HasComment("Corresponde a la fecha de actualización del registro");
             

                entity.HasOne(d => d.Breeds)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.BreedIdPk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ANIMAL_BREED");
            });

        }           
    }
}
