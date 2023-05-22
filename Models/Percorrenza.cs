using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Percorrenza
{
    public string Callsign { get; set; } = null!;

    public DateTime Dof { get; set; }

    public string NomePunto { get; set; } = null!;

    public DateTime OrarioDiSorvolo { get; set; }

    public virtual Punto NomePuntoNavigation { get; set; } = null!;

    public virtual Pianodivolo Pianodivolo { get; set; } = null!;
}
