﻿// <auto-generated/>
#pragma warning disable 1591
using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Pianodivolo
{
    public DateTime? OrarioAtterraggio { get; set; }

    public DateTime? OrarioDecollo { get; set; }

    public string Callsign { get; set; } = null!;

    public DateTime Dof { get; set; }

    public string NumeroDiCoda { get; set; } = null!;

    public string CodAdDecollo { get; set; } = null!;

    public string OrientamentoPistaDecollo { get; set; } = null!;

    public string CodAdAtterraggio { get; set; } = null!;

    public string OrientamentoPistaAtterraggio { get; set; } = null!;

    public virtual Aereomobile NumeroDiCodaNavigation { get; set; } = null!;

    public virtual ICollection<Percorrenza> Percorrenzas { get; set; } = new List<Percorrenza>();

    public virtual Pistum Pistum { get; set; } = null!;

    public virtual Pistum PistumNavigation { get; set; } = null!;

    public virtual ICollection<Stimati> Stimatis { get; set; } = new List<Stimati>();
}
