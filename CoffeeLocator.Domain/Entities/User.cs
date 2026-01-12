using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeLocator.Domain.Common;
using CoffeeLocator.Domain.Enums;

namespace CoffeeLocator.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }


    /// <summary>
    /// Builder profesional para User
    /// </summary>
    /// <param name="email"></param>
    /// <param name="passwordHash"></param>
    /// <param name="fullName"></param>
    public User(string email, string passwordHash, string fullName)
    {
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
        Role = UserRole.StandardUser;
    }

    /// <summary>
    /// Metod for updating the user's role.
    /// </summary>
    /// <param name="newRole"></param>
    public void UpdateRole(UserRole newRole)
    {
        Role = newRole;
    }
}
