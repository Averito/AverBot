using AverBot.API.Context;
using AverBot.API.Hubs;
using AverBot.API.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var environmentsService = new EnvironmentService(@"D:\Averito\CSharp\AverBot\AverBot.API\.env");
environmentsService.EnvironmentsLoad();

builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<ServerService>();
builder.Services.AddSingleton<GuildUserService>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(host => true)
        .AllowCredentials();
}));
builder.Services.AddSignalR();
builder.Services.AddDbContext<AverBotContext>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(AuthService.ConfigureAuthenticationOptions)
    .AddJwtBearer(AuthService.ConfigureJwtBearerOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.MapHub<WarnsHub>("Hubs/Warns");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();