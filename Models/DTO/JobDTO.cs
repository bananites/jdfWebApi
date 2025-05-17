public class JobDTO
{
    public int Id { get; set; }
    public string? XmlPath { get; set; }
    public string? Name { get; set; }
    public string? Status { get; set; }
    public int? MachineId { get; set; }
    public List<UserDTO>? Users { get; set;}

}