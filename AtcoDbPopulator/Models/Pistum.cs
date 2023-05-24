using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Pistum
{
    public string CodAd { get; set; } = null!;

    public string Orientamento { get; set; } = null!;

    public int Lunghezza { get; set; }

    public virtual Aerodromo CodAdNavigation { get; set; } = null!;

    public virtual ICollection<Pianodivolo> PianodivoloPista { get; set; } = new List<Pianodivolo>();

    public virtual ICollection<Pianodivolo> PianodivoloPistumNavigations { get; set; } = new List<Pianodivolo>();
}
