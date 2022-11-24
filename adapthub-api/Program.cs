using adapthub_api;
using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.Services;
using adapthub_api.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IJobRequestRepository, JobRequestRepository>();
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IModeratorRepository, ModeratorRepository>();
builder.Services.AddTransient<IVacancyProcessService, VacancyProcessService>();

//builder.Services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=adapthub;MultipleActiveResultSets=true");
});

builder.Services.AddSingleton(new EmailConfiguration
{
    From = "testttt621@gmail.com",
    Password = "gcpqfioeurckanxw",
    SmtpServer = "smtp.gmail.com",
    Port = 465,
    UserName = "testttt621@gmail.com",
});

builder.Services.AddIdentity<Customer, IdentityRole<int>>()
                .AddEntityFrameworkStores<DataContext>();

builder.Services.AddIdentityCore<Customer>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Customer>>(TokenOptions.DefaultProvider);

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.SignIn.RequireConfirmedEmail = true;
    opts.User.AllowedUserNameCharacters = string.Empty;
    opts.User.RequireUniqueEmail = true;
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true,
};
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "origin",
                      builder => {
                          builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("origin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
