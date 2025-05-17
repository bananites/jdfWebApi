using DataPollingApi.Models;

public class MachineJobDTO
{
    public int Id { get; set; }
    public int? MachineId { get; set; }
    public int? JobId { get; set; }

    public Job? Job { get; set; }
}