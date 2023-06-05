using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NPDelivery.Auth;
using NPDelivery.Data;
using NPDelivery.Domain;
using NPDelivery.Helpers;

namespace NPDelivery.Features;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly DataContext _dataContext;

    public TestController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = _dataContext.Users.FirstOrDefault(x => x.Email == username);
        if(user == null || !PasswordHasher.Verify(user.PasswordHash, password))
        {
            return Problem(detail: "Username or password is incorrect", title: "Bad Request Error", statusCode: 400);
        }

        var claims = new List<Claim>()
        {
            new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
            new Claim(CustomClaimTypes.Name, user.Name),
            new Claim(CustomClaimTypes.Email, user.Email)
        };
        foreach(var role in user.Roles)
        {
            claims.Add(new Claim(CustomClaimTypes.Role, role.ToString()));
        }

        var claimIdentity = new ClaimsIdentity(claims, AuthenticationTypes.Password);
        var authUser = new ClaimsPrincipal(claimIdentity);
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authUser);
        return Ok();
    }

    [Route("CustomerRegister")]
    [HttpPost]
    public async Task<IActionResult> RegisterAsCustomer(RegisterUserDto registerUser)
    {
        var user = new User
        {
            Email = registerUser.Email,
            Name = registerUser.Name,
            PasswordHash = PasswordHasher.HashPassword(registerUser.Password),
            Roles = new List<ApplicationRole>() { ApplicationRole.Customer }
        };

        await _dataContext.AddAsync(user);

        await _dataContext.SaveChangesAsync();

        return Ok(user);

    }

    // N.B. Should be safe to prevent accessing by brute forcing. Send security token?
    [Route("CourierRegister")]
    [HttpPost]
    public async Task<IActionResult> RegisterAsCourier(RegisterUserDto registerUser)
    {
        var user = new User
        {
            Email = registerUser.Email,
            Name = registerUser.Name,
            PasswordHash = PasswordHasher.HashPassword(registerUser.Password),
            Roles = new List<ApplicationRole>() { ApplicationRole.Courier }
        };

        await _dataContext.AddAsync(user);

        await _dataContext.SaveChangesAsync();

        return Ok(user);

    }

    // N.B. Should be safe to prevent accessing by brute forcing. Send security token?
    [Route("StoreOwnerRegister")]
    [HttpPost]
    public async Task<IActionResult> RegisterAsStoreOwner(RegisterUserDto registerUser)
    {
        var user = new User
        {
            Email = registerUser.Email,
            Name = registerUser.Name,
            PasswordHash = PasswordHasher.HashPassword(registerUser.Password),
            Roles = new List<ApplicationRole>() { ApplicationRole.StoreKeeper }
        };

        await _dataContext.AddAsync(user);

        await _dataContext.SaveChangesAsync();

        return Ok(user);
    }
    
    // N.B. Also should be safe
    [Route("SupportRegister")]
    [HttpPost]
    public async Task<IActionResult> RegisterAsSupport(RegisterUserDto registerUser)
    {
        var user = new User
        {
            Email = registerUser.Email,
            Name = registerUser.Name,
            PasswordHash = PasswordHasher.HashPassword(registerUser.Password),
            Roles = new List<ApplicationRole>() { ApplicationRole.Support }
        };

        await _dataContext.AddAsync(user);

        await _dataContext.SaveChangesAsync();

        return Ok(user);
    }

    [Route("Logout")]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

}

public record RegisterUserDto(string Email, string Name, string Password);