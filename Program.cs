using FlightBookingSystem.Data;
using FlightBookingSystem.Repositories;
using FlightBookingSystem.Services.FlightBookingSystem.Services;
using FlightBookingSystem.Services;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using FlightBookingSystem.Interfaces.IServices;
using FlightBookingSystem.Interfaces.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter()); // Global authorization filter ekleyin
});

//Add AbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<IFlightRepository, FlightRepository>(); // scope kayýt iþlemi IoC containere kayýt etme iþlemidir. Transient, Scoped ve singleton olarak 3 farklý türü vardýr burda scoped olarak kayýt IoC container'e kayýt iþlemi gerçekleþmiþtir. Yani Her istek baþýna yeni bir örnek oluþturulur. 
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();


// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Services
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IUserService, UserService>();


// Add JwtService
builder.Services.AddScoped<JwtService>();

// JWT Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightBookingSystem", Version = "v1" });

    // Bearer token için güvenlik tanýmý ekleniyor
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen JWT token giriniz. (Bearer Token)  þekilinde giriniz. Yani tokenden önce Bearer yaz yoksa çalýþmaz.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Hata ayýklama sayfasý
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Authentication middleware'i
app.UseAuthorization();  // Authorization middleware'i


app.MapControllers();

app.Run();
