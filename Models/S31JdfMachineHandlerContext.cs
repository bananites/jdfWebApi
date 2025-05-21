using Microsoft.EntityFrameworkCore;

namespace DataPollingApi.Models;

public partial class S31JdfMachineHandlerContext : DbContext
{
    public S31JdfMachineHandlerContext()
    {
    }

    public S31JdfMachineHandlerContext(DbContextOptions<S31JdfMachineHandlerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<MachineJob> MachineJobs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserJob> UserJobs { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//       => optionsBuilder.UseMySql("server=mainnode.fwsites.de;user=u31_dCwfbwFlMd;password=bmgUPWHnDuWvAA.oRVBSKaB4;database=s31_jdf_machine_handler", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.21-mariadb"));
    //  => optionsBuilder.UseMySql(, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.21-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job");

            
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.XmlPath)
                .HasMaxLength(200)
                .HasColumnName("xmlPath");
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("machine");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .HasColumnName("type");
            entity.Property(e => e.YearBuilt)
                .HasColumnType("int(11)")
                .HasColumnName("yearBuilt");
        });

        modelBuilder.Entity<MachineJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("machine_jobs");

            entity.HasIndex(e => e.JobId, "jobId");

            entity.HasIndex(e => e.MachineId, "machineId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("jobId");
            entity.Property(e => e.MachineId)
                .HasColumnType("int(11)")
                .HasColumnName("machineId");

            entity.HasOne(d => d.Job).WithMany(p => p.MachineJobs)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("machine_jobs_ibfk_2");

            entity.HasOne(d => d.Machine).WithMany(p => p.MachineJobs)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("machine_jobs_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_job");

            entity.HasIndex(e => e.JobId, "jobId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.JobId)
                .HasColumnType("int(11)")
                .HasColumnName("jobId");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.Job).WithMany(p => p.UserJobs)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("user_job_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.UserJobs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_job_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
