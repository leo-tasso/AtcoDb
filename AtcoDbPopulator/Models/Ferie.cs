﻿// <auto-generated/>
#pragma warning disable 1591
using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Ferie
{
    public string IdControllore { get; set; } = null!;

    public DateTime Inizio { get; set; }

    public DateTime Fine { get; set; }

    public virtual Controllore IdControlloreNavigation { get; set; } = null!;
}
