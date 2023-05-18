using System;

namespace DAL.Entities;

public class UserEntity : BaseEntity<Guid>
{
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime? BirthDate { get; set; }
}