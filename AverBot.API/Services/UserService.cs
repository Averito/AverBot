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
        
        var user = await ctx.Users.Include(user => user.Servers).FirstOrDefaultAsync(user => user.Id == id);
        if (user == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);
        
        return user;
    }

    public async Task<User> EditUser(User currentUser, EditUserDTO editUserDto)
    {
        await using var ctx = new AverBotContext();
        
        currentUser.EditUsername(editUserDto.Username);
        currentUser.EditDiscriminator(editUserDto.Discriminator);
        currentUser.EditAvatar(editUserDto.Avatar);

        var editedUser = ctx.Users.Update(currentUser);
        await ctx.SaveChangesAsync();
        
        return editedUser.Entity;
    }

    public int GetUserIdFromHttpContext(HttpContext httpContext)
    {
        var userIdString = httpContext?.User?.Claims.ToList()[0].Value;
        int userId;

        var canConvert = int.TryParse(userIdString, out userId);
        if (!canConvert) throw new BadHttpRequestException(ExceptionMessage.UserIdNotValid);

        return userId;
    }
    
    public async Task<User> Me(HttpContext httpContext)
    {
        var userId = GetUserIdFromHttpContext(httpContext);

        await using var ctx = new AverBotContext();

        var user = ctx.Users.Include(user => user.Servers).FirstOrDefault(user => user.Id == userId);
        if (user == null) throw new BadHttpRequestException(ExceptionMessage.NotFound);

        return user;
    }
}