using Azure;

using NPDelivery.Auth;

namespace NPDelivery.Domain;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    // N.B. No need to store salt, rely on Argon2 implementation
    //public string Salt { get; set; }
    public List<ApplicationRole> Roles { get; set; }  

}

public class Role
{
    public string Value { get; set; }
    public static implicit operator string(Role role) => role.Value;
    public static implicit operator Role(string role) => new() { Value = role };
    public static implicit operator Role(ApplicationRole role) => new() { Value = role.ToString() };
}