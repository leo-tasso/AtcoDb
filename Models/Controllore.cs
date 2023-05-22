﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Controllore
{
    public string IdControllore { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string NomeCentro { get; set; } = null!;

    public virtual ICollection<Abilitazione> Abilitaziones { get; set; } = new List<Abilitazione>();

    public virtual ICollection<Ferie> Feries { get; set; } = new List<Ferie>();

    public virtual Centro NomeCentroNavigation { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
