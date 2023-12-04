using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealStateCompanyApplication.Configurations;
using RealStateCompanyApplication.Context;
using RealStateCompanyApplication.Services;
using RealStateCompanyApplication.Services.Contracts;
using RealStateCompanyApplication.Utilities;
using RealStateCompanyApplication.Utilities.Contracts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/// Changes in the swagger configuration to be able to test the access token right here
builder.Services.AddSwaggerGen(x=>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please Enter Jwt Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                }
            },
            new string[] { }
        }
    });
});

/// Configuration of the new interfaces created
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IJwtTokenUtility, JwtTokenUtility>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

/// Connection to the database to generate a session with the DbContext
builder.Services.AddDbContext<RealStateContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("GamesConnectionString")));

/// Configuration of the mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

/// Configuration of the JwtToken
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
