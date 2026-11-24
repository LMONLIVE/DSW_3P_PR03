using Microsoft.EntityFrameworkCore;
using DSW_3P_PR03.Repositories;
using DSW_3P_PR03.Services;
using DSW_3P_PR03.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

///Agregar Controladores
builder.Services.AddControllers();

//Agregar dependencias
builder.Services.AddScoped<IPersonajeRepository,PersonajeSQLRepository>();
builder.Services.AddScoped<IPersonajeService,PersonajeService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);

// Configuración de JWT
var jwt = builder.Configuration.GetSection("Jwt");
var key = Convert.FromBase64String(
    jwt["Key"] ?? throw new InvalidOperationException(
       "Falta configurar Jwt:Key"));
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwt["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(key)
            };
    });

// CONFIGURAR POLÍTICA DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CharacterHubFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",
                "http://127.0.0.1:5500"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Mapear Swagger
    app.UseSwaggerUI( options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "CharacterHub"
        );
    });
}

app.UseCors("CharacterHubFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//Mapear Controllers
app.MapControllers();

app.Run();