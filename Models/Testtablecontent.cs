using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models;

public partial class Testtablecontent
{
    public Guid ContentId { get; set; }

    public string Content { get; set; } = null!;

    public Guid Userid { get; set; }
    [JsonIgnore]

    public virtual Testtableuser User { get; set; } = null!;
}
