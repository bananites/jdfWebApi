public class UserDTO
{
    public int Id { get; set; }
    public string? Username { get; set; }
    
    public List<JobDTO>? UserJobs { get; set; }
}