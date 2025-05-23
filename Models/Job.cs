﻿namespace DataPollingApi.Models;

public partial class Job
{
    public int Id { get; set; }
    public string? XmlPath { get; set; }
    public string? Status { get; set; }
    public string? Name{ get; set; }
    public virtual ICollection<MachineJob> MachineJobs { get; set; } = new List<MachineJob>();
    public virtual ICollection<UserJob> UserJobs { get; set; } = new List<UserJob>();
}
