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
        public DbSet<DinarskiRacun> DinarskiRacuni { get; set; }

        public BankaKontekst (DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Modelovanje odnosa Korisnika i Dinarskog racuna (1:1 odnos u bazi)

            modelBuilder.Entity<Korisnik>()
                .HasOne(x => x.DinarskiRacun)
                .WithOne(x => x.Korisnik)
                .IsRequired(false);

            modelBuilder.Entity<DinarskiRacun>()
                .HasKey(x => x.IdKorisnika);

            // Modelovanje odnosa Korisnika i Deviznih racuna (1:n odnos u bazi)

            modelBuilder.Entity<Korisnik>()
                .HasMany(x => x.DevizniRacuni)
                .WithOne(x => x.Korisnik)
                .HasForeignKey(x => x.IdKorisnika)
                .IsRequired();


            modelBuilder.Entity<DevizniRacun>()
                .HasOne(x => x.Korisnik)
                .WithMany(x => x.DevizniRacuni)
                .IsRequired();
        }
    }
}
