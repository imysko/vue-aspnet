using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using RESTApi.DataBase.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RESTApi.DataBase.Data;

namespace RESTApi.Controllers;

[Route("api/authentication")]
[ApiController]
[Produces("application/json")]
public class AuthenticationController : BaseController
{
    public AuthenticationController(F1DataBaseContext context) : base(context)
    {
    }

    [HttpPost("register")]
    public async Task<JsonResult> Register(UserViewModel request)
    {
        if (_context.Users.Any(u => u.Username == request.Username))
        {
            return new JsonResult(new BaseResponse(false, "username-exist"));
        }
        
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordKey);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordKey = passwordKey
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userRole = new UserRole
        {
            UserId = _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username).Result!.Id,
            RoleId = 1
        };
        
        _context.UsersRoles.Add(userRole);
        await _context.SaveChangesAsync();
        
        return new JsonResult(new BaseResponse(true, "registration-completed"));
    }

    [HttpPost("login")]
    public async Task<JsonResult> Login(UserViewModel request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        
        if (user == null)
        {
            return new JsonResult(new BaseResponse(false, "username-not-found"));
        }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordKey))
        {
            return new JsonResult(new BaseResponse(false, "wrong-password"));
        }
        
        var sessionId = (long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        var session = new Session
        {
            Id = sessionId,
            UserId = user.Id
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
            
        HttpContext.Response.Cookies.Append("session_id", sessionId.ToString(), new CookieOptions()
        {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.AddMonths(1)
        });

        return new JsonResult(new BaseResponse(true, "authorization-completed"));
    }

    [HttpPost("logout")]
    public async Task<JsonResult> Logout()
    {
        var sessionId = GetSession();

        if (sessionId != null)
        {
            var sessionToRemove = await _context.Sessions.FirstOrDefaultAsync(session => session.Id == sessionId);
            
            if (sessionToRemove != null)
            {
                HttpContext.Response.Cookies.Delete("session_id");

                _context.Sessions.Remove(sessionToRemove);
                await _context.SaveChangesAsync();
                
                return new JsonResult(new BaseResponse(true, "session-finish"));
            }
        }
        
        return new JsonResult(new BaseResponse(false, "session-not-found"));
    }

    [HttpGet("check_authentication")]
    public async Task<bool> CheckAuthentication()
    {
        return CheckSession();
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