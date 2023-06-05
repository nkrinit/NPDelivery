using System.Security.Cryptography;
using System.Text;

using Isopoh.Cryptography.Argon2;

namespace NPDelivery.Helpers;

public static class PasswordHasher
{
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public static string HashPassword(string password)
    {
        var salt = new byte[24];
        _rng.GetBytes(salt);
        var config = new Argon2Config()
        {
            Password = Encoding.UTF8.GetBytes(password),
            Salt = salt,
            HashLength = 32,
        };

        using var argon2 = new Argon2(config);
        using var hash = argon2.Hash();

        return config.EncodeString(hash.Buffer);
    }

    public static bool Verify(string passwordHash, string password)
    {
        return Argon2.Verify(passwordHash, password);
    }
}

// Initial implementation:
//public static class PasswordHasher
//{
//    private static readonly RandomNumberGenerator Rng =
//        RandomNumberGenerator.Create();

//    public static string HashPassword(string password, string salt)
//    {
//        var config = new Argon2Config
//        {
//            Salt = Convert.FromBase64String(salt),
//            Password = Encoding.UTF8.GetBytes(password),
//            HashLength = 64
//        };

//        using var argon2 = new Argon2(config);
//        using var hash = argon2.Hash();
//        return config.EncodeString(hash.Buffer);
//    }

//    public static string GetSalt()
//    {
//        byte[] salt = new byte[24];
//        Rng.GetBytes(salt);
//        return Convert.ToBase64String(salt);
//    }

//    // Initial implementation:
//    //public static string HashPassword(string password)
//    //{
//    //    return Argon2.Hash(password, hashLength: 64);
//    //}
//}
