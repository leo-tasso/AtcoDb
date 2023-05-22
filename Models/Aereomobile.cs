﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Aereomobile
{
    public string Tipo { get; set; } = null!;

    public string NumeroDiCoda { get; set; } = null!;

    public virtual ICollection<Pianodivolo> Pianodivolos { get; set; } = new List<Pianodivolo>();
}
