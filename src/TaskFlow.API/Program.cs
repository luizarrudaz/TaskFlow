using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Interfaces;
using TaskFlow.API.Services;
using TaskFlow.API.UseCases.Task.GetAllTasksAsync;
using TaskFlow.API.Repositories;
using TaskFlow.API.UseCases.Task.GetTaskByIdAsync;
using TaskFlow.API.UseCases.Task.AddTaskAsync;
using TaskFlow.API.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskFlow.API.UseCases.User.AuthenticateUser;
using Microsoft.OpenApi.Models;
using TaskFlow.API.UseCases.Task.DeleteTaskAsync;
using TaskFlow.API.UseCases.Task.UpdateTaskAsync;
using TaskFlow.API.UseCases.Task.GetTaskByNameAsync;
using TaskFlow.API.Validators;
using FluentValidation;
using TaskFlow.API.DTO.TaskCreateDTO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskFlow.API", Version = "v1" });

    // Add JWT configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
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

// JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Definir como true em produção
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = false,
        //ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Database
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
    .EnableSensitiveDataLogging() // Ativar apenas em desenvolvimento
    .LogTo(Console.WriteLine));

// Dependencies
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<GetAllTasksUseCase>();
builder.Services.AddScoped<GetTaskByIdUseCase>();
builder.Services.AddScoped<AddTaskUseCase>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticateUserUseCase>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeleteTaskUseCase>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<GetTaskByNameUseCase>();
builder.Services.AddScoped<IValidator<TaskCreateAndUpdateDTO>, TaskCreateAndUpdateValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();