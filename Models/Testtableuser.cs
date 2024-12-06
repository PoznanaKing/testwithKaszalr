using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Testtableuser
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Testtablecontent> Testtablecontents { get; set; } = new List<Testtablecontent>();
}
