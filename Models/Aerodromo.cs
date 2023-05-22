using System;
using System.Collections.Generic;

namespace AtcoDbPopulator.Models;

public partial class Aerodromo
{
    public string AdLatitudine { get; set; } = null!;

    public string AdLongitudine { get; set; } = null!;

    public string CodiceIcao { get; set; } = null!;

    public string CodiceIata { get; set; } = null!;

    public virtual ICollection<Pistum> Pista { get; set; } = new List<Pistum>();

    public virtual ICollection<Settore> Settores { get; set; } = new List<Settore>();
}
