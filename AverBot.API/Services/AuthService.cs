using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AverBot.API.Constants;
using AverBot.API.Context;
using AverBot.API.DTO;
using AverBot.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AverBot.API.Services;

public class AuthService
{
    public async Task<RegistrationResponseDTO> Registration(RegistrationDTO registrationDto)
    {
        await using var ctx = new AverBotContext();

        var user = await ctx.Users.FirstOrDefaultAsync(user => user.DiscordId == registrationDto.DiscordId);
        if (user != null) throw new BadHttpRequestException(ExceptionMessage.UserFound);

        var newUser = new User(registrationDto);
        var createdUser = await ctx.Users.AddAsync(newUser);

        var token = GenerateSecurityToken(registrationDto.ExpiresIn, createdUser.Entity.Id);

        await ctx.SaveChangesAsync();

        return new RegistrationResponseDTO(createdUser.Entity, token);
    }

    public async Task<LoginResponseDTO> Login(LoginDTO loginDto)
    {
        await using var ctx = new AverBotContext();

        var user = await ctx.Users.FirstOrDefaultAsync(user => user.Id == loginDto.Id);
        if (user == null) throw new BadHttpRequestException(ExceptionMessage.UserNotFound);

        var token = GenerateSecurityToken(loginDto.ExpiresIn, loginDto.Id);

        return new LoginResponseDTO(loginDto.Id, user.DiscordId, token);
    }

    private string? GenerateSecurityToken(DateTime expiresIn, int id)
    {
        var jwtSercet = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (jwtSercet == null) return null;
        
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        var key = Encoding.ASCII.GetBytes(jwtSercet);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new [] {
                new Claim("id", id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = expiresIn,
            Audience = audience,
            Issuer = issuer,
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}