﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Centro
{
    public string NomeCentro { get; set; } = null!;

    public virtual ICollection<Controllore> Controllores { get; set; } = new List<Controllore>();

    public virtual ICollection<Postazione> Postaziones { get; set; } = new List<Postazione>();

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
