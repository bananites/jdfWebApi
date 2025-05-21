
public class MachineDTO
{

    public int Id { get; set; }
    public string? Type { get; set; }
    public int? YearBuilt { get; set; }

    public List<JobDTO>? MachineJobs { get; set; } 
}