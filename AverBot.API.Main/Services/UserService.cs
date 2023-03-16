using AverBot.API.Main.Context;
using AverBot.API.Main.DTO;
using AverBot.API.Main.Models;
using Microsoft.EntityFrameworkCore;

namespace AverBot.API.Main.Services;

public class UserService
{
    public async Task<List<User>> GetUsers()
    {
        await using var ctx = new AverBotContext();
        return await ctx.Users.ToListAsync();
    }

    public async Task<User> CreateUser(CreateUserDTO createUserDto)
    {
        await using var ctx = new AverBotContext();
        
        var user = new User(createUserDto);
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();

        return user;
    }
}