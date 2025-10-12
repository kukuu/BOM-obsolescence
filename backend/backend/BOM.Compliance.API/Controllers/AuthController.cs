using Microsoft.AspNetCore.Mvc;
using BOM.Compliance.API.Services;
using BOM.Compliance.Domain.Entities;

namespace BOM.Compliance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        try
        {
            var user = await _authService.AuthenticateAsync(request.Email, request.Password);
            
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);
            
            return Ok(new LoginResponse
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Login failed: {ex.Message}");
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> Register(RegisterRequest request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);
            
            var token = _jwtService.GenerateToken(user);
            
            return Ok(new LoginResponse
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role
                }
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Registration failed: {ex.Message}");
        }
    }
}

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string Name, UserRole Role);
public record LoginResponse(string Token, UserDto User);
public record UserDto(Guid Id, string Email, string Name, UserRole Role);