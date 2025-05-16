using System;
using System.Collections.Generic;

namespace DataPollingApi.Models;

public partial class Machine
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? YearBuilt { get; set; }

    public virtual ICollection<MachineJob> MachineJobs { get; set; } = new List<MachineJob>();
}
