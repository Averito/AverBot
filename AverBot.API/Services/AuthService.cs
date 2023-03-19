using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AverBot.API.Constants;
using AverBot.API.Context;
using AverBot.API.DTO;
using AverBot.API.Models;
using AverBot.API.Structs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AverBot.API.Services;

public class AuthService
{
    public async Task<RegistrationResponseDTO> Registration(RegistrationDTO registrationDto)
    {
        try
        {
            await using var ctx = new AverBotContext();

            var user = await ctx.Users.FirstOrDefaultAsync(user => user.DiscordId == registrationDto.DiscordId);
            if (user != null) throw new BadHttpRequestException(ExceptionMessage.UserFound);

            var newUser = new User(registrationDto);
            var createdUser = await ctx.Users.AddAsync(newUser);
            await ctx.SaveChangesAsync();

            var token = GenerateSecurityToken(registrationDto.ExpiresIn, createdUser.Entity.Id);

            return new RegistrationResponseDTO(createdUser.Entity, token);
        }
        catch (Exception exception)
        {
            throw new BadHttpRequestException(exception.Message);
        }
    }
    public async Task<LoginResponseDTO> Login(LoginDTO loginDto)
    {
        try
        {
            await using var ctx = new AverBotContext();

            var user = await ctx.Users.FirstOrDefaultAsync(user => user.Id == loginDto.Id);
            if (user == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

            var token = GenerateSecurityToken(loginDto.ExpiresIn, loginDto.Id);

            return new LoginResponseDTO(loginDto.Id, user.DiscordId, token);
        }
        catch (Exception exception)
        {
            throw new BadHttpRequestException(exception.Message);
        }
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
    public static void ConfigureAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    public static void ConfigureJwtBearerOptions(JwtBearerOptions options)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (jwtSecret == null) return;

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.Headers.Append("Content-Type", "application/json");
                var jsonError = JsonConvert.SerializeObject(new UnauthorizedError(401, "You are not authorized"));
                await context.Response.WriteAsync(jsonError);
            },
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/Hubs/Warn"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    }
}