using System.Text.Json.Serialization;

public class UserDTO
{
    public int Id { get; set; }
    public string? Username { get; set; }

   [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public List<JobDTO>? UserJobs { get; set; }
}