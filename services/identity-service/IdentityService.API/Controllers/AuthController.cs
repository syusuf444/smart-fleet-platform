using System.Security.Cryptography;
using System.Text;
using IdentityService.API.Data;
using IdentityService.API.DTOs;
using IdentityService.API.Models;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthDbContext _context;

    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(
        AuthDbContext context,
        IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequestDto request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email);

        if (existingUser != null)
        {
            return BadRequest(
                new AuthResponseDto
                {
                    Success = false,
                    Message = "User already exists"
                });
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            Role = request.Role
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(
            new AuthResponseDto
            {
                Success = true,
                Message = "User registered successfully"
            });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequestDto request)
    {
        var hashedPassword =
            HashPassword(request.Password);

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x =>
                    x.Email == request.Email &&
                    x.PasswordHash == hashedPassword);

        if (user == null)
        {
            return Unauthorized(
                new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid credentials"
                });
        }

        var token =
            _jwtTokenService.GenerateToken(user);

        return Ok(
            new AuthResponseDto
            {
                Success = true,
                Token = token,
                Message = "Login successful"
            });
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);

        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}