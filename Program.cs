using api.Databases;
using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
  ContentRootPath = Directory.GetCurrentDirectory(),
  WebRootPath = AppDomain.CurrentDomain.BaseDirectory + "terus-content"
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
  opts.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "PMS",
    Version = "v1"
  });

  opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please Enter a valid Token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });

  opts.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        Array.Empty<string>()
      }
    });
});

// Allow CORs
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
  builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Connection Database
builder.Services
  .AddDbContext<ApplicationDbContext>(
    opts => opts.UseMySQL(
      builder.Configuration.GetConnectionString("mysql")!
    )
  );

// Identity Config
builder.Services
  .AddIdentity<AppUser, IdentityRole>(options =>
  {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    // options.Password.RequiredLength = 12;
  })
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

// Authentication Config
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme =
  options.DefaultChallengeScheme =
  options.DefaultForbidScheme =
  options.DefaultScheme =
  options.DefaultSignInScheme =
  options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
    ValidateAudience = true,
    ValidAudience = builder.Configuration["JWT:ValidAudience"],
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
  };
});

// Register Repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
  app.UseSwagger();
  app.UseSwaggerUI();

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();