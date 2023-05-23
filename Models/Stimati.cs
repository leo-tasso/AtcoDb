﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Stimati
{
    public string Callsign { get; set; } = null!;

    public DateTime Dof { get; set; }

    public string NomePunto { get; set; } = null!;

    public DateTime OrarioStimato { get; set; }

    public virtual Punto NomePuntoNavigation { get; set; } = null!;

    public virtual Pianodivolo Pianodivolo { get; set; } = null!;
}