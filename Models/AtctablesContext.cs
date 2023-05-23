﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AtcoDbPopulator.Models;

public partial class AtctablesContext : DbContext
{
    public AtctablesContext()
    {
    }

    public AtctablesContext(DbContextOptions<AtctablesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abilitazione> Abilitaziones { get; set; }

    public virtual DbSet<Aereomobile> Aereomobiles { get; set; }

    public virtual DbSet<Aerodromo> Aerodromos { get; set; }

    public virtual DbSet<Centro> Centros { get; set; }

    public virtual DbSet<Controllore> Controllores { get; set; }

    public virtual DbSet<Ferie> Feries { get; set; }

    public virtual DbSet<Percorrenza> Percorrenzas { get; set; }

    public virtual DbSet<Pianodivolo> Pianodivolos { get; set; }

    public virtual DbSet<Pistum> Pista { get; set; }

    public virtual DbSet<Postazione> Postaziones { get; set; }

    public virtual DbSet<Punto> Puntos { get; set; }

    public virtual DbSet<Settore> Settores { get; set; }

    public virtual DbSet<Stimati> Stimatis { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=atctables;Uid=root;Pwd=rasberry;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abilitazione>(entity =>
        {
            entity.HasKey(e => e.MatricolaAbilitazione).HasName("PRIMARY");

            entity.ToTable("abilitazione");

            entity.HasIndex(e => e.IdControllore, "FKPossedimento");

            entity.Property(e => e.IdControllore)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdControlloreNavigation).WithMany(p => p.Abilitaziones)
                .HasForeignKey(d => d.IdControllore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPossedimento");

            entity.HasMany(d => d.IdSettores).WithMany(p => p.MatricolaAbilitaziones)
                .UsingEntity<Dictionary<string, object>>(
                    "Abilitazionesettori",
                    r => r.HasOne<Settore>().WithMany()
                        .HasForeignKey("IdSettore")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKIdSettore"),
                    l => l.HasOne<Abilitazione>().WithMany()
                        .HasForeignKey("MatricolaAbilitazione")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKMatricolaAbilitazione"),
                    j =>
                    {
                        j.HasKey("MatricolaAbilitazione", "IdSettore").HasName("PRIMARY");
                        j.ToTable("abilitazionesettori");
                        j.HasIndex(new[] { "IdSettore" }, "FKIdSettore");
                        j.IndexerProperty<string>("IdSettore")
                            .HasMaxLength(50)
                            .IsFixedLength();
                    });
        });

        modelBuilder.Entity<Aereomobile>(entity =>
        {
            entity.HasKey(e => e.NumeroDiCoda).HasName("PRIMARY");

            entity.ToTable("aereomobile");

            entity.Property(e => e.NumeroDiCoda)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Tipo)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<Aerodromo>(entity =>
        {
            entity.HasKey(e => e.CodiceIcao).HasName("PRIMARY");

            entity.ToTable("aerodromo");

            entity.Property(e => e.CodiceIcao)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.CodiceIata)
                .HasMaxLength(3)
                .IsFixedLength();
        });

        modelBuilder.Entity<Centro>(entity =>
        {
            entity.HasKey(e => e.NomeCentro).HasName("PRIMARY");

            entity.ToTable("centro");

            entity.Property(e => e.NomeCentro).HasMaxLength(40);
        });

        modelBuilder.Entity<Controllore>(entity =>
        {
            entity.HasKey(e => e.IdControllore).HasName("PRIMARY");

            entity.ToTable("controllore");

            entity.HasIndex(e => e.NomeCentro, "FKAssegnazione");

            entity.Property(e => e.IdControllore)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Cognome).HasMaxLength(20);
            entity.Property(e => e.Nome).HasMaxLength(20);
            entity.Property(e => e.NomeCentro).HasMaxLength(40);

            entity.HasOne(d => d.NomeCentroNavigation).WithMany(p => p.Controllores)
                .HasForeignKey(d => d.NomeCentro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAssegnazione");
        });

        modelBuilder.Entity<Ferie>(entity =>
        {
            entity.HasKey(e => new { e.IdControllore, e.Inizio }).HasName("PRIMARY");

            entity.ToTable("ferie");

            entity.Property(e => e.IdControllore)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Inizio).HasColumnType("date");
            entity.Property(e => e.Fine).HasColumnType("date");

            entity.HasOne(d => d.IdControlloreNavigation).WithMany(p => p.Feries)
                .HasForeignKey(d => d.IdControllore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSvolgimento");
        });

        modelBuilder.Entity<Percorrenza>(entity =>
        {
            entity.HasKey(e => new { e.Callsign, e.Dof, e.NomePunto }).HasName("PRIMARY");

            entity.ToTable("percorrenza");

            entity.HasIndex(e => e.NomePunto, "FKPer_Pun");

            entity.Property(e => e.Callsign).HasMaxLength(30);
            entity.Property(e => e.Dof).HasColumnType("date");
            entity.Property(e => e.NomePunto)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.OrarioDiSorvolo).HasColumnType("datetime");

            entity.HasOne(d => d.NomePuntoNavigation).WithMany(p => p.Percorrenzas)
                .HasForeignKey(d => d.NomePunto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPer_Pun");

            entity.HasOne(d => d.Pianodivolo).WithMany(p => p.Percorrenzas)
                .HasForeignKey(d => new { d.Callsign, d.Dof })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPer_Pia");
        });

        modelBuilder.Entity<Pianodivolo>(entity =>
        {
            entity.HasKey(e => new { e.Callsign, e.Dof }).HasName("PRIMARY");

            entity.ToTable("pianodivolo");

            entity.HasIndex(e => new { e.CodAdAtterraggio, e.OrientamentoPistaAtterraggio }, "FKPistaAtterraggio");

            entity.HasIndex(e => new { e.CodAdDecollo, e.OrientamentoPistaDecollo }, "FKPistaDecollo");

            entity.HasIndex(e => e.NumeroDiCoda, "FKPraticamento");

            entity.Property(e => e.Callsign).HasMaxLength(30);
            entity.Property(e => e.Dof).HasColumnType("date");
            entity.Property(e => e.CodAdAtterraggio)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.CodAdDecollo)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.NumeroDiCoda)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.OrarioAtterraggio).HasColumnType("datetime");
            entity.Property(e => e.OrarioDecollo).HasColumnType("datetime");
            entity.Property(e => e.OrientamentoPistaAtterraggio)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.OrientamentoPistaDecollo)
                .HasMaxLength(3)
                .IsFixedLength();

            entity.HasOne(d => d.NumeroDiCodaNavigation).WithMany(p => p.Pianodivolos)
                .HasForeignKey(d => d.NumeroDiCoda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPraticamento");

            entity.HasOne(d => d.Pistum).WithMany(p => p.PianodivoloPista)
                .HasForeignKey(d => new { d.CodAdAtterraggio, d.OrientamentoPistaAtterraggio })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPistaAtterraggio");

            entity.HasOne(d => d.PistumNavigation).WithMany(p => p.PianodivoloPistumNavigations)
                .HasForeignKey(d => new { d.CodAdDecollo, d.OrientamentoPistaDecollo })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPistaDecollo");
        });

        modelBuilder.Entity<Pistum>(entity =>
        {
            entity.HasKey(e => new { e.CodAd, e.Orientamento }).HasName("PRIMARY");

            entity.ToTable("pista");

            entity.Property(e => e.CodAd)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Orientamento)
                .HasMaxLength(3)
                .IsFixedLength();

            entity.HasOne(d => d.CodAdNavigation).WithMany(p => p.Pista)
                .HasForeignKey(d => d.CodAd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKComposizione");
        });

        modelBuilder.Entity<Postazione>(entity =>
        {
            entity.HasKey(e => e.IdPostazione).HasName("PRIMARY");

            entity.ToTable("postazione");

            entity.HasIndex(e => e.NomeCentro, "FKUbicazione");

            entity.Property(e => e.IdPostazione).HasMaxLength(50);
            entity.Property(e => e.NomeCentro).HasMaxLength(40);

            entity.HasOne(d => d.NomeCentroNavigation).WithMany(p => p.Postaziones)
                .HasForeignKey(d => d.NomeCentro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUbicazione");

            entity.HasMany(d => d.IdSettores).WithMany(p => p.IdPostaziones)
                .UsingEntity<Dictionary<string, object>>(
                    "Composizionesettori",
                    r => r.HasOne<Settore>().WithMany()
                        .HasForeignKey("IdSettore")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKCom_Set"),
                    l => l.HasOne<Postazione>().WithMany()
                        .HasForeignKey("IdPostazione")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKCom_Pos"),
                    j =>
                    {
                        j.HasKey("IdPostazione", "IdSettore").HasName("PRIMARY");
                        j.ToTable("composizionesettori");
                        j.HasIndex(new[] { "IdSettore" }, "FKCom_Set");
                        j.IndexerProperty<string>("IdPostazione").HasMaxLength(50);
                        j.IndexerProperty<string>("IdSettore")
                            .HasMaxLength(50)
                            .IsFixedLength();
                    });
        });

        modelBuilder.Entity<Punto>(entity =>
        {
            entity.HasKey(e => e.NomePunto).HasName("PRIMARY");

            entity.ToTable("punto");

            entity.HasIndex(e => e.IdSettore, "FKR");

            entity.Property(e => e.NomePunto)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.IdSettore)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdSettoreNavigation).WithMany(p => p.Puntos)
                .HasForeignKey(d => d.IdSettore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKR");
        });

        modelBuilder.Entity<Settore>(entity =>
        {
            entity.HasKey(e => e.IdSettore).HasName("PRIMARY");

            entity.ToTable("settore");

            entity.HasIndex(e => e.CodAd, "FKAppartenenza");

            entity.Property(e => e.IdSettore)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.CodAd)
                .HasMaxLength(4)
                .IsFixedLength();

            entity.HasOne(d => d.CodAdNavigation).WithMany(p => p.Settores)
                .HasForeignKey(d => d.CodAd)
                .HasConstraintName("FKAppartenenza");
        });

        modelBuilder.Entity<Stimati>(entity =>
        {
            entity.HasKey(e => new { e.Callsign, e.Dof, e.NomePunto }).HasName("PRIMARY");

            entity.ToTable("stimati");

            entity.HasIndex(e => e.NomePunto, "FKSti_Pun");

            entity.Property(e => e.Callsign).HasMaxLength(30);
            entity.Property(e => e.Dof).HasColumnType("date");
            entity.Property(e => e.NomePunto)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.OrarioStimato).HasColumnType("datetime");

            entity.HasOne(d => d.NomePuntoNavigation).WithMany(p => p.Stimatis)
                .HasForeignKey(d => d.NomePunto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSti_Pun");

            entity.HasOne(d => d.Pianodivolo).WithMany(p => p.Stimatis)
                .HasForeignKey(d => new { d.Callsign, d.Dof })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSti_Pia");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => new { e.IdControllore, e.Data, e.Slot }).HasName("PRIMARY");

            entity.ToTable("turno");

            entity.HasIndex(e => e.IdPostazione, "FKTurnoLavorativo");

            entity.HasIndex(e => e.CentroStandBy, "FKTurnoStandBy");

            entity.Property(e => e.IdControllore)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Data).HasColumnType("date");
            entity.Property(e => e.CentroStandBy).HasMaxLength(10);
            entity.Property(e => e.IdPostazione).HasMaxLength(50);

            entity.HasOne(d => d.CentroStandByNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.CentroStandBy)
                .HasConstraintName("FKTurnoStandBy");

            entity.HasOne(d => d.IdControlloreNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdControllore)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKLavora");

            entity.HasOne(d => d.IdPostazioneNavigation).WithMany(p => p.Turnos)
                .HasForeignKey(d => d.IdPostazione)
                .HasConstraintName("FKTurnoLavorativo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
