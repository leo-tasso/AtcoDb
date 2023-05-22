﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Postazione
{
    public string IdPostazione { get; set; } = null!;

    public string NomeCentro { get; set; } = null!;

    public virtual Centro NomeCentroNavigation { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    public virtual ICollection<Settore> IdSettores { get; set; } = new List<Settore>();
}
