﻿using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Turno
{
    public string IdControllore { get; set; } = null!;

    public int Retribuzione { get; set; }

    public DateTime Data { get; set; }

    public int Slot { get; set; }

    public string? IdPostazione { get; set; }

    public string? CentroStandBy { get; set; }

    public virtual Centro? CentroStandByNavigation { get; set; }

    public virtual Controllore IdControlloreNavigation { get; set; } = null!;

    public virtual Postazione? IdPostazioneNavigation { get; set; }
}
