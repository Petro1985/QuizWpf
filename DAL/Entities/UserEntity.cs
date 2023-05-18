using System;

namespace DAL.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime? BirthDate { get; set; }
}