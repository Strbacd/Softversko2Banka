using System;
using System.Collections.Generic;
using System.Text;
using Banka.Data.Entiteti;
using Microsoft.EntityFrameworkCore;

namespace Banka.Data
{
    public class BankaKontekst : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Valuta> Valute { get; set; }
        public DbSet<Placanje> Placanja { get; set; }

        public BankaKontekst (DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Modelovanje kompozitnog kljuca Racun tabele
            modelBuilder.Entity<Racun>()
                .HasKey(x => new { x.IdValute, x.IdKorisnika });

            // Modelovanje odnosa Korisnika i Racuna (1:n odnos u bazi)

            modelBuilder.Entity<Korisnik>()
                .HasMany(x => x.Racuni)
                .WithOne(x => x.Korisnik)
                .HasForeignKey(x => x.IdKorisnika)
                .IsRequired();


            modelBuilder.Entity<Racun>()
                .HasOne(x => x.Korisnik)
                .WithMany(x => x.Racuni)
                .IsRequired();

            // Modelovanje odnosa Racuna i valuta (1:n odnosu u bazi)

            modelBuilder.Entity<Racun>()
                .HasOne(x => x.Valuta)
                .WithMany(x => x.Racuni)
                .HasForeignKey(x => x.IdValute)
                .IsRequired();

            modelBuilder.Entity<Valuta>()
                .HasMany(x => x.Racuni)
                .WithOne(x => x.Valuta)
                .IsRequired();

            // Modelovanje odnosa Racuna i Placanja (1:n odnos u bazi)

            modelBuilder.Entity<Placanje>()
                .HasOne(x => x.Racun)
                .WithMany(x => x.Placanja)
                .HasForeignKey(x => x.IdRacuna)
                .IsRequired();

            modelBuilder.Entity<Racun>()
                .HasMany(x => x.Placanja)
                .WithOne(x => x.Racun)
                .IsRequired();
        }
    }
}
