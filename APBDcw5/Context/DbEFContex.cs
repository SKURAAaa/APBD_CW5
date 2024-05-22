using Microsoft.EntityFrameworkCore;
using APBD_CW5.Models;

namespace APBD_CW5.Context
{
    public partial class BazaDanychContext : DbContext
    {
        public BazaDanychContext(DbContextOptions<BazaDanychContext> opcje)
            : base(opcje)
        {
        }

        // DbSet dla każdej encji
        public DbSet<Klient> Klienci { get; set; } = null!;
        public DbSet<KlientWycieczka> WycieczkiKlientów { get; set; } = null!;
        public DbSet<Kraj> Kraje { get; set; } = null!;
        public DbSet<Wycieczka> Wycieczki { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja encji Klient
            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlient).HasName("Klient_pk");
                entity.ToTable("Klient", "wycieczka");
                entity.Property(e => e.IdKlient).ValueGeneratedNever();
                entity.Property(e => e.Email).HasMaxLength(120);
                entity.Property(e => e.Imie).HasMaxLength(120);
                entity.Property(e => e.Nazwisko).HasMaxLength(120);
                entity.Property(e => e.Pesel).HasMaxLength(120);
                entity.Property(e => e.Telefon).HasMaxLength(120);
            });

            // Konfiguracja encji KlientWycieczka
            modelBuilder.Entity<KlientWycieczka>(entity =>
            {
                entity.HasKey(e => new { e.IdKlient, e.IdWycieczka }).HasName("Klient_Wycieczka_pk");
                entity.ToTable("Klient_Wycieczka", "wycieczka");
                entity.Property(e => e.DataPlatnosci).HasColumnType("datetime");
                entity.Property(e => e.DataRejestracji).HasColumnType("datetime");
                entity.HasOne(d => d.KlientNawigacja)
                    .WithMany(p => p.WycieczkiKlientów)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Tabela_5_Klient");
                entity.HasOne(d => d.WycieczkaNawigacja)
                    .WithMany(p => p.WycieczkiKlientów)
                    .HasForeignKey(d => d.IdWycieczka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Tabela_5_Wycieczka");
            });

            // Konfiguracja encji Kraj
            modelBuilder.Entity<Kraj>(entity =>
            {
                entity.HasKey(e => e.IdKraj).HasName("Kraj_pk");
                entity.ToTable("Kraj", "wycieczka");
                entity.Property(e => e.IdKraj).ValueGeneratedNever();
                entity.Property(e => e.Nazwa).HasMaxLength(120);
                entity.HasMany(d => d.Wycieczki)
                    .WithMany(p => p.Kraje)
                    .UsingEntity<Dictionary<string, object>>(
                        "KrajWycieczka",
                        l => l.HasOne<Wycieczka>().WithMany().HasForeignKey("IdWycieczka").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Kraj_Wycieczka_Wycieczka"),
                        r => r.HasOne<Kraj>().WithMany().HasForeignKey("IdKraj").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Kraj_Wycieczka_Kraj"),
                        j =>
                        {
                            j.HasKey("IdKraj", "IdWycieczka").HasName("Kraj_Wycieczka_pk");
                            j.ToTable("Kraj_Wycieczka", "wycieczka");
                        });
            });

            // Konfiguracja encji Wycieczka
            modelBuilder.Entity<Wycieczka>(entity =>
            {
                entity.HasKey(e => e.IdWycieczka).HasName("Wycieczka_pk");
                entity.ToTable("Wycieczka", "wycieczka");
                entity.Property(e => e.IdWycieczka).ValueGeneratedNever();
                entity.Property(e => e.DataOd).HasColumnType("datetime");
                entity.Property(e => e.DataDo).HasColumnType("datetime");
                entity.Property(e => e.Opis).HasMaxLength(220);
                entity.Property(e => e.Nazwa).HasMaxLength(120);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
