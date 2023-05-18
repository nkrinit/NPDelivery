namespace NPDelivery.Auth;

public static class AuthenticationTypes
{
    public const string Password = "Password";
}

public static class PolicyNames
{
    public const string Customer = "Customer";
    public const string Courier = "Courier";
    public const string StoreKeeper = "StoreKeeper";
    public const string Admin = "Admin";
    public const string Support = "Support";
}

public static class CustomClaimTypes
{
    public const string UserId = "userId";
    public const string Email = "email";
    public const string Name = "name";
    public const string Role = "role";
}

public enum ApplicationRole
{
    Customer,
    Courier,
    StoreKeeper,
    Admin,
    Support
}
