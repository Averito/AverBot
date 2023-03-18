using AverBot.API.Constants;
using AverBot.API.Context;
using AverBot.API.DTO;
using AverBot.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AverBot.API.Services;

public class UserService
{
    public async Task<User> GetUserById(int id)
    {
        await using var ctx = new AverBotContext();
        var user = ctx.Users.Find(id);

        if (user == null) throw new BadHttpRequestException(ExceptionMessage.UserNotFound);
        
        return user;
    }

    public async Task<User> EditUser(User user)
    {
        await using var ctx = new AverBotContext();

        var editedUser = ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
        
        return editedUser.Entity;
    }
}