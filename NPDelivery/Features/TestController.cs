using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NPDelivery.Auth;

namespace NPDelivery.Features;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, username)
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthenticationTypes.Password));
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
        return Ok();
    }
}
