﻿using System;
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
        public DbSet<DevizniRacun> DevizniRacuni { get; set; }
        public DbSet<Valuta> Valute { get; set; }

        public BankaKontekst (DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Modelovanje odnosa Korisnika i Dinarskog racuna (1:1 odnos u bazi)

            modelBuilder.Entity<Korisnik>()
                .HasMany(x => x.DinarskiRacun)
                .WithOne(x => x.Korisnik)
                .HasForeignKey(x => x.IdKorisnika)
                .IsRequired();

            modelBuilder.Entity<DinarskiRacun>()
                .HasOne(x => x.Korisnik)
                .WithMany(x => x.DinarskiRacun)
                .IsRequired();

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

            // Modelovanje odnosa Deviznog racuna i valuta (1:n odnosu u bazi)

            modelBuilder.Entity<DevizniRacun>()
                .HasOne(x => x.Valuta)
                .WithMany(x => x.DevizniRacun)
                .HasForeignKey(x => x.IdValute)
                .IsRequired();

            modelBuilder.Entity<Valuta>()
                .HasMany(x => x.DevizniRacun)
                .WithOne(x => x.Valuta)
                .IsRequired();
        }
    }
}
