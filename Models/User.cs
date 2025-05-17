namespace DataPollingApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<UserJob> UserJobs { get; set; } = new List<UserJob>();
}
