using AverBot.API.Main.Context;
using AverBot.API.Main.Services;

var builder = WebApplication.CreateBuilder(args);

var environmentsService = new EnvironmentService(@"D:\Averito\CSharp\AverBot\AverBot.API.Main\.env");
environmentsService.EnvironmentsLoad();

builder.Services.AddSingleton<UserService>();

builder.Services.AddDbContext<AverBotContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();