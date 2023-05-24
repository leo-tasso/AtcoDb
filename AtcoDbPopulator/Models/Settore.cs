using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Settore
{
    public string IdSettore { get; set; } = null!;

    public string? CodAd { get; set; }

    public virtual Aerodromo? CodAdNavigation { get; set; }

    public virtual ICollection<Punto> Puntos { get; set; } = new List<Punto>();

    public virtual ICollection<Postazione> IdPostaziones { get; set; } = new List<Postazione>();

    public virtual ICollection<Abilitazione> MatricolaAbilitaziones { get; set; } = new List<Abilitazione>();
}
