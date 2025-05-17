using System;
using System.Collections.Generic;

namespace DataPollingApi.Models;

// TODO define a name for the Job and Status
public partial class Job
{
    public int Id { get; set; }
    public string? XmlPath { get; set; }
    public string? Status { get; set; }
    public string? Name{ get; set; }
    public virtual ICollection<MachineJob> MachineJobs { get; set; } = new List<MachineJob>();
    public virtual ICollection<UserJob> UserJobs { get; set; } = new List<UserJob>();
}
