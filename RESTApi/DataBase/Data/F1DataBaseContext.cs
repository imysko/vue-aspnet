using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Models;

namespace RESTApi.DataBase.Data
{
    public partial class F1DataBaseContext : DbContext
    {
        public F1DataBaseContext()
        {
        }

        public F1DataBaseContext(DbContextOptions<F1DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<UserRole> UsersRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                
                entity.Property(e => e.Username)
                    .HasColumnType("text")
                    .HasColumnName("username");
                
                entity.Property(e => e.PasswordHash)
                    .HasColumnType("text")
                    .HasColumnName("password_hash");
                
                entity.Property(e => e.PasswordKey)
                    .HasColumnType("text")
                    .HasColumnName("password_key");
            });
            
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                
                entity.Property(e => e.Name)
                    .HasColumnType("text")
                    .HasColumnName("role");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("Users_Roles");

                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity
                    .HasOne(e => e.User)
                    .WithMany(e => e.UsersRoles)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("user_id");
                
                entity
                    .HasOne(e => e.Role)
                    .WithMany(e => e.UsersRoles)
                    .HasForeignKey(e => e.RoleId)
                    .HasConstraintName("role_id");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.TeamId, "team_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(512)
                    .HasColumnName("image_url");

                entity.Property(e => e.Information)
                    .HasColumnType("text")
                    .HasColumnName("information");

                entity.Property(e => e.TeamId)
                    .HasColumnType("int(11)")
                    .HasColumnName("team_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .HasColumnName("title");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("Cars_team_id");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(70)
                    .HasColumnName("title");

                entity.Property(e => e.Url)
                    .HasMaxLength(30)
                    .HasColumnName("url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
