using DSW_3P_PR03.Repositories;
using DSW_3P_PR03.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

///Agregar Controladores
builder.Services.AddControllers();

//Agregar dependencias
builder.Services.AddScoped<IPersonajeRepository,PersonajeJSONRepository>();
builder.Services.AddScoped<IPersonajeService,PersonajeService>();

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