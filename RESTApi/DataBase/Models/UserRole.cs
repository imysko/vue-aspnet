﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RESTApi.DataBase.Models;

public partial class UserRole
{
    [Column("user_id")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    public Role Role { get; set; }
}