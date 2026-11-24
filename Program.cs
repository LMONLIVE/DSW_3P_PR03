using Microsoft.EntityFrameworkCore;
using DSW_3P_PR03.Repositories;
using DSW_3P_PR03.Services;
using DSW_3P_PR03.Data;

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

app.UseHttpsRedirection();

//Mapear Controllers
app.MapControllers();

app.Run();