using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Punto
{
    public string NomePunto { get; set; } = null!;

    public string PosLatitudine { get; set; }

    public string PosLongitudine { get; set; }

    public string IdSettore { get; set; } = null!;

    public virtual Settore IdSettoreNavigation { get; set; } = null!;

    public virtual ICollection<Percorrenza> Percorrenzas { get; set; } = new List<Percorrenza>();

    public virtual ICollection<Stimati> Stimatis { get; set; } = new List<Stimati>();
}
