﻿// using IdGenerator;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TimeFourthe.Configurations;
using TimeFourthe.Services;

var key = Encoding.UTF8.GetBytes("1234");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://timefourthe.vercel.app").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
    });
});

// MongoDB Settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<TimetableService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<PendingUserService>();

// Controllers
builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();