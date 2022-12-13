namespace RESTApi.DataBase.Models;

public partial class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public byte[] PasswordKey { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    
    public virtual ICollection<UserRole> UsersRoles { get; set; }
}