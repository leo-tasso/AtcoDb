using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Abilitazione
{
    public int MatricolaAbilitazione { get; set; }

    public string IdControllore { get; set; } = null!;

    public virtual Controllore IdControlloreNavigation { get; set; } = null!;

    public virtual ICollection<Settore> IdSettores { get; set; } = new List<Settore>();
}
