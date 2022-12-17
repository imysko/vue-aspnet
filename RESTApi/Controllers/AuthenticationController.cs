using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using RESTApi.DataBase.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;

namespace RESTApi.Controllers;

[Route("api/authentication")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly F1DataBaseContext _context;

    public AuthenticationController(F1DataBaseContext context)
    {
        _context = context;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> Register(UserViewModel request)
    {
        if (_context.Users.Any(u => u.Username == request.Username))
        {
            return Conflict("Username already exist");
        }
        
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordKey);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordKey = passwordKey
        };
        
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
        var userRole = new UserRole
        {
            User = user,
            Role = role!
        };

        _context.Users.Add(user);
        _context.UsersRoles.Add(userRole);
        
        await _context.SaveChangesAsync();

        return StatusCode(401, "User was registered");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login(UserViewModel request)
    {
        var user = await _context.Users
            .Include(u => u.UsersRoles)
            .ThenInclude(e => e.Role)
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordKey))
        {
            return Unauthorized("Wrong username or password");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username)
        };
        claims.AddRange(user.UsersRoles
            .Select(usersRole => new Claim(ClaimsIdentity.DefaultRoleClaimType, usersRole.Role.Name)));

        var id = new ClaimsIdentity(
            claims, 
            "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType
        );
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        return Ok("User was authorized");
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok("User was deauthorized");
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("me"), Authorize]
    public Task<IActionResult> GetMe()
    {
        return Task.FromResult<IActionResult>(Ok(new
        {
            username = User.FindFirst(c => c.Type == ClaimsIdentity.DefaultNameClaimType)!.Value,
            roles = User.FindAll(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Select(c => c.Value)
        }));
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("check_authentication"), AllowAnonymous]
    public Task<IActionResult> CheckAuthentication()
    {
        return Task.FromResult<IActionResult>(
            Ok(User.FindFirst(c => c.Type == ClaimsIdentity.DefaultNameClaimType) != null));
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordKey)
    {
        using var hmac = new HMACSHA512();
        passwordKey = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordKey)
    {
        using var hmac = new HMACSHA512(passwordKey);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
    }
}