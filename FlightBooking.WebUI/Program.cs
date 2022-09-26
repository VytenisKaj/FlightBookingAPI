using FlightBooking.Infrastructure;
using FlightBooking.Infrastructure.Clients;
using FlightBooking.WebUI.Controllers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var redisConnection = builder.Configuration.GetValue<string>("RedisConnection");
var redis = ConnectionMultiplexer.Connect(redisConnection);

var redisClient = new RedisClient(redisConnection, allowAdmin: true);
builder.Services.AddSingleton<IRedisClient>(redisClient);
builder.Services.AddScoped(s => redis.GetDatabase());

//DataSeeder.SeedAirplanes();

builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<RedisController>(_ => new RedisController(redisClient.Database, redisClient, new RedisService(redisClient.Database)));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
