using System;
using System.Collections.Generic;

namespace DataPollingApi.Models;

public partial class MachineJob
{
    public int Id { get; set; }

    public int? MachineId { get; set; }

    public int? JobId { get; set; }

    public virtual Job? Job { get; set; }

    public virtual Machine? Machine { get; set; }
}
